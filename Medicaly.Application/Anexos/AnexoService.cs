using Medicaly.Application.Communs;
using Medicaly.Application.Entensions;
using Medicaly.Application.Transients;
using Medicaly.Domain.Anexos;
using Medicaly.Domain.Anexos.Dtos;
using Medicaly.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Medicaly.Application.Anexos;

public interface IAnexoService
{
    public Task<AnexoOutput?> Get(Guid id);
    public Task<PagedResult<AnexoOutput>> GetList(PagedFilteredInput input);
    public Task<AnexoOutput?> Create(AnexoInput input);
    public Task<bool> Delete(Guid id);
}

public class AnexoService: IAnexoService, IAutoTransient
{
    private readonly MedicalyDbContext _db;
    private readonly ILogger<AnexoService> _logger;

    private DbSet<Anexo> _anexos => _db.Anexos;

    public AnexoService(MedicalyDbContext db, ILogger<AnexoService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<AnexoOutput?> Get(Guid id)
    {
        var anexo = await _anexos.AsNoTracking().FirstOrDefaultAsync(anexo => anexo.Id == id);
        return anexo != null ? new AnexoOutput(anexo) : null;
    }

    public async Task<PagedResult<AnexoOutput>> GetList(PagedFilteredInput input)
    {
        return await _anexos
            .AsNoTracking()
            .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), a => a.Nome.ToLower().Contains(input.Filter))
            .Select(a => new AnexoOutput(a))
            .ToPagedResult(input);
    }

    public async Task<AnexoOutput?> Create(AnexoInput input)
    {
        try
        {
            var anexo = new Anexo(input);
            await _anexos.AddAsync(anexo);
            await _db.SaveChangesAsync();
            return new AnexoOutput(anexo);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public async Task<bool> Delete(Guid id)
    {
        var anexo = await _anexos.FindAsync(id);
        if (anexo != null)
        {
            _anexos.Remove(anexo);
            await _db.SaveChangesAsync();
            return true;
        }
        return false;
    }
}