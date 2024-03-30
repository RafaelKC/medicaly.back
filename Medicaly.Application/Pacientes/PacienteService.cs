using Medicaly.Application.Communs;
using Medicaly.Application.Entensions;
using Medicaly.Application.Transients;
using Medicaly.Domain.Pacientes;
using Medicaly.Domain.Pacientes.Dtos;
using Medicaly.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Medicaly.Application.Pacientes;

public interface IPacienteService
{
    public Task<PacienteOutput?> Get(Guid pacienteId);
    public Task<PacienteOutput?> GetByCpf(string pacienteCpf);
    public Task<PacienteOutput?> GetByEmail(string pacienteEmail);
    public Task<PagedResult<PacienteOutput>> GetList(PagedFilteredInput input);
    public Task<Guid?> Create(PacienteInput input);
    public Task<bool> Update(Guid pacienteId, PacienteInput input);
    public Task<bool> Delete(Guid pacienteId);
}

public class PacienteService: IPacienteService, IAutoTransient
{
    private readonly ILogger<PacienteService> _logger;
    private readonly MedicalyDbContext _db;
    private DbSet<Paciente> _pacientes => _db.Pacientes;

    public PacienteService(MedicalyDbContext db, ILogger<PacienteService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<PacienteOutput?> Get(Guid pacienteId)
    {
        var paciente = await _pacientes.AsNoTracking().FirstOrDefaultAsync(paciente => paciente.Id == pacienteId);
        return paciente != null ? new PacienteOutput(paciente) : null;
    }

    public async Task<PacienteOutput?> GetByCpf(string pacienteCpf)
    {
        var paciente = await _pacientes.AsNoTracking().FirstOrDefaultAsync(paciente => paciente.Cpf == pacienteCpf);
        return paciente != null ? new PacienteOutput(paciente) : null;
    }

    public async Task<PacienteOutput?> GetByEmail(string pacienteEmail)
    {
        var paciente = await _pacientes.AsNoTracking().FirstOrDefaultAsync(paciente => paciente.Email == pacienteEmail);
        return paciente != null ? new PacienteOutput(paciente) : null;
    }

    public async Task<PagedResult<PacienteOutput>> GetList(PagedFilteredInput input)
    {
        var query = _pacientes
            .AsNoTracking()
            .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), paciente =>
                paciente.Nome.ToLower().Contains(input.Filter.ToLower())
                || paciente.Sobrenome.ToLower().Contains(input.Filter.ToLower())
            )
            .Select(paciente => new PacienteOutput(paciente));

        return await query.ToPagedResult(input);
    }

    public async Task<Guid?> Create(PacienteInput input)
    {
        try
        {
            var paciente = new Paciente(input);
            await _pacientes.AddAsync(paciente);
            await _db.SaveChangesAsync();
            return paciente.Id;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public async Task<bool> Update(Guid pacienteId, PacienteInput input)
    {
        var paciente = await _pacientes.FirstOrDefaultAsync(paciente => paciente.Id == pacienteId);
        if (paciente == null) return false;
        paciente.Update(input);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(Guid pacienteId)
    {
        var paciente = await _pacientes.FirstOrDefaultAsync(paciente => paciente.Id == pacienteId);
        if (paciente == null) return false;
        _pacientes.Remove(paciente);
        await _db.SaveChangesAsync();
        return true;
    }
}