using Medicaly.Application.Communs;
using Medicaly.Application.Entensions;
using Medicaly.Application.Transients;
using Medicaly.Domain.Administradores;
using Medicaly.Domain.Administradores.Dtos;
using Medicaly.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Medicaly.Application.Administradores;

public interface IAdministradorService
{
    public Task<AdministradorOutput?> Get(Guid aministradorId);
    public Task<AdministradorOutput?> GetByEmail(string administradorEmail);
    public Task<PagedResult<AdministradorOutput>> GetList(PagedFilteredInput input);
    public Task<Guid?> Create(AdministradorInput input);
    public Task<bool> Update(Guid administradorId, AdministradorInput input);
    public Task<bool> Delete(Guid administradorId);
}

public class AdministradorService: IAdministradorService, IAutoTransient
{
    private readonly ILogger<AdministradorService> _logger;
    private readonly MedicalyDbContext _db;
    private DbSet<Administrador> _administradors => _db.Administradores;

    public AdministradorService(MedicalyDbContext db, ILogger<AdministradorService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<AdministradorOutput?> Get(Guid aministradorId)
    {
        var Administrador = await _administradors.AsNoTracking().FirstOrDefaultAsync(Administrador => Administrador.Id == aministradorId);
        return Administrador != null ? new AdministradorOutput(Administrador) : null;
    }

    public async Task<AdministradorOutput?> GetByCpf(string AdministradorCpf)
    {
        var Administrador = await _administradors.AsNoTracking().FirstOrDefaultAsync(Administrador => Administrador.Cpf == AdministradorCpf);
        return Administrador != null ? new AdministradorOutput(Administrador) : null;
    }

    public async Task<AdministradorOutput?> GetByEmail(string administradorEmail)
    {
        var administrador = await _administradors.AsNoTracking().FirstOrDefaultAsync(administrador => administrador.Email == administradorEmail);
        return administrador != null ? new AdministradorOutput(administrador) : null;
    }

    public async Task<PagedResult<AdministradorOutput>> GetList(PagedFilteredInput input)
    {
        var query = _administradors
            .AsNoTracking()
            .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), administrador =>
                administrador.Nome.ToLower().Contains(input.Filter.ToLower())
                || administrador.Sobrenome.ToLower().Contains(input.Filter.ToLower())
            )
            .Select(administrador => new AdministradorOutput(administrador));

        return await query.ToPagedResult(input);
    }

    public async Task<Guid?> Create(AdministradorInput input)
    {
        try
        {
            var administrador = new Administrador(input);
            await _administradors.AddAsync(administrador);
            await _db.SaveChangesAsync();
            return administrador.Id;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public async Task<bool> Update(Guid administradorId, AdministradorInput input)
    {
        var administrador = await _administradors.FirstOrDefaultAsync(administrador => administrador.Id == administradorId);
        if (administrador == null) return false;
        administrador.Update(input);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(Guid administradorId)
    {
        var administrador = await _administradors.FirstOrDefaultAsync(administrador => administrador.Id == administradorId);
        if (administrador == null) return false;
        _administradors.Remove(administrador);
        await _db.SaveChangesAsync();
        return true;
    }
}