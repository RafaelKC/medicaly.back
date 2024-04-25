using Medicaly.Application.Communs;
using Medicaly.Application.Entensions;
using Medicaly.Application.Transients;
using Medicaly.Domain.Especialidades;
using Medicaly.Domain.Especialidades.Dtos;
using Medicaly.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Medicaly.Application.Especialidades;

public interface IEspecialidadeService
{
    public Task<EspecialidadeModel?> Get(Guid especialidadeId);
    public Task<PagedResult<EspecialidadeModel>> GetList(PagedFilteredInput input);
    public Task<Guid?> Create(EspecialidadeModel input);
    public Task<bool> Update(Guid especialidadeId, EspecialidadeModel input);
    public Task<bool> Delete(Guid especialidadeId);
}

public class EspecialidadeService: IEspecialidadeService, IAutoTransient
{
    private readonly ILogger<EspecialidadeService> _logger;
    private readonly MedicalyDbContext _db;

    private DbSet<Especialidade> _especialidade => _db.Especialidades;

    public EspecialidadeService(MedicalyDbContext db, ILogger<EspecialidadeService> logger)
    {
        _db = db;
        _logger = logger;
    }


    public async Task<EspecialidadeModel?> Get(Guid especialidadeId)
    {
        var especialidade = await _especialidade.AsNoTracking().FirstOrDefaultAsync(especialidade => especialidade.Id == especialidadeId);
        return especialidade != null ? new EspecialidadeModel(especialidade) : null;
    }

    public async Task<PagedResult<EspecialidadeModel>> GetList(PagedFilteredInput input)
    {
        var query = _especialidade
            .AsNoTracking()
            .Select(especialidade => new EspecialidadeModel(especialidade));

        return await query.ToPagedResult(input);
    }

    public async Task<Guid?> Create(EspecialidadeModel input)
    {
        try
        {
            var especialidade = new Especialidade(input);
            await _especialidade.AddAsync(especialidade);
            await _db.SaveChangesAsync();
            return especialidade.Id;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public async Task<bool> Update(Guid especialidadeId, EspecialidadeModel input)
    {
        var especialidade = await _especialidade
            .FirstOrDefaultAsync(especialidade => especialidade.Id == especialidadeId);
        if (especialidade == null) return false;
        especialidade.Update(input);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(Guid especialidadeId)
    {
        var especialidade = await _especialidade.FirstOrDefaultAsync(especialidade => especialidade.Id == especialidadeId);
        if (especialidade == null) return false;
        _especialidade.Remove(especialidade);
        await _db.SaveChangesAsync();
        return true;
    }
}