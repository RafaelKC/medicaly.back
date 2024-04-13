using Medicaly.Application.Communs;
using Medicaly.Application.Entensions;
using Medicaly.Application.Transients;
using Medicaly.Domain.UnidadeAtendimento;
using Medicaly.Domain.UnidadeAtendimento.Dtos;
using Medicaly.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Medicaly.Application.UnidadesAtendimento;

public interface IUnidadeAtendimentoService
{
    public Task<UnidadeAtendimentoOutput?> Get(Guid unidadeAtendimentoId);
    public Task<PagedResult<UnidadeAtendimentoOutput>> GetList(PagedFilteredInput input);
    public Task<Guid?> Create(UnidadeAtendimentoInput input);
    public Task<bool> Update(Guid unidadeAtendimentoId, UnidadeAtendimentoInput input);
    public Task<bool> Delete(Guid unidadeAtendimentoId);
}

public class UnidadeAtendimentoService: IUnidadeAtendimentoService, IAutoTransient
{
    private readonly ILogger<UnidadeAtendimentoService> _logger;
    private readonly MedicalyDbContext _db;

    private DbSet<UnidadeAtendimento> _unidadeAtendimento => _db.UnidadeAtendimentos;

    public UnidadeAtendimentoService(MedicalyDbContext db, ILogger<UnidadeAtendimentoService> logger)
    {
        _db = db;
        _logger = logger;
    }


    public async Task<UnidadeAtendimentoOutput?> Get(Guid unidadeAtendimentoId)
    {
        var unidadeAtendimento = await _unidadeAtendimento.AsNoTracking().FirstOrDefaultAsync(unidadeAtendimento => unidadeAtendimento.Id == unidadeAtendimentoId);
        return unidadeAtendimento != null ? new UnidadeAtendimentoOutput(unidadeAtendimento) : null;
    }

    public async Task<PagedResult<UnidadeAtendimentoOutput>> GetList(PagedFilteredInput input)
    {
        var query = _unidadeAtendimento
            .AsNoTracking()
            .Select(unidadeAtendimento => new UnidadeAtendimentoOutput(unidadeAtendimento));

        return await query.ToPagedResult(input);
    }

    public async Task<Guid?> Create(UnidadeAtendimentoInput input)
    {
        try
        {
            var unidadeAtendimento = new UnidadeAtendimento(input);
            await _unidadeAtendimento.AddAsync(unidadeAtendimento);
            await _db.SaveChangesAsync();
            return unidadeAtendimento.Id;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public async Task<bool> Update(Guid unidadeAtendimentoId, UnidadeAtendimentoInput input)
    {
        var unidadeAtendimento = await _unidadeAtendimento
            .FirstOrDefaultAsync(unidadeAtendimento => unidadeAtendimento.Id == unidadeAtendimentoId);
        if (unidadeAtendimento == null) return false;
        unidadeAtendimento.Update(input);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(Guid unidadeAtendimentoId)
    {
        var unidadeAtendimento = await _unidadeAtendimento.FirstOrDefaultAsync(unidadeAtendimento => unidadeAtendimento.Id == unidadeAtendimentoId);
        if (unidadeAtendimento == null) return false;
        _unidadeAtendimento.Remove(unidadeAtendimento);
        await _db.SaveChangesAsync();
        return true;
    }
}