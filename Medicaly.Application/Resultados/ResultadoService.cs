using Medicaly.Application.Communs;
using Medicaly.Application.Entensions;
using Medicaly.Application.Procedimentos.Dtos;
using Medicaly.Application.Resultados.Dtos;
using Medicaly.Application.Transients;
using Medicaly.Domain.Anexos.Dtos;
using Medicaly.Domain.Resultados;
using Medicaly.Domain.Resultados.Dtos;
using Medicaly.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Medicaly.Application.Resultados;
public interface IResultadoService
{
    public Task<ResultadoOutput> Get(Guid resultadoId);
    public Task<PagedResult<ResultadoOutput>> GetList(GetListResultadoInput input);
    public Task<Guid?> Create(ResultadoInput input);
    public Task<bool> Update(Guid resultadoId, ResultadoInput input);
    public Task<bool> Delete(Guid resultadoId);
}

public class ResultadoService : IResultadoService, IAutoTransient
{
    private readonly ILogger<ResultadoService> _logger;
    private readonly MedicalyDbContext _db;

    private DbSet<Resultado> _resultado => _db.Resultados;


   
    
    public ResultadoService(MedicalyDbContext db, ILogger<ResultadoService> logger)
    {
        _db = db;
        _logger = logger;
    }

    
    public async Task<ResultadoOutput?> Get(Guid resultadoId)
    {
        var resultado = await _resultado.AsNoTracking()
                .Include(a => a.ResultadoAnexo)
                
                .Select(resultado => new ResultadoOutput()
                {
                    ProcedimentoId = resultado.ProcedimentoId,
                    AnexoId = resultado.ResultadoAnexo.AnexoId,
                    Observacoes = resultado.Observacoes
            
                })
                .FirstOrDefaultAsync(resultado => resultado.ProcedimentoId == resultadoId);
        return resultado;
    }

    public async Task<PagedResult<ResultadoOutput>> GetList(GetListResultadoInput input)
    {
        var query = _resultado.AsNoTracking()
            .WhereIf(input.ProcedimentoId.HasValue, a => input.ProcedimentoId.Value==a.ProcedimentoId)
            .Include(a => a.ResultadoAnexo)
            .ThenInclude(a => a.Anexo)
            .Select(resultado => new ResultadoOutput()
        {
            ProcedimentoId = resultado.ProcedimentoId,
            AnexoId = resultado.ResultadoAnexo.AnexoId,
            Observacoes = resultado.Observacoes
            
        });
        return await query.ToPagedResult(input);
    }

    public async Task<Guid?> Create(ResultadoInput input)
    {
        try
        {
            var resultado = new Resultado(input);
            await _resultado.AddAsync(resultado);
            await _db.SaveChangesAsync();
            return resultado.ProcedimentoId;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> Update(Guid resultadoId, ResultadoInput input)
    {
        var resultado = await _resultado.FirstOrDefaultAsync(resultado => resultado.ProcedimentoId == resultadoId);
        if (resultadoId == null)
        {
            return false;
        }
        resultado.Update(input);
        _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(Guid resultadoId)
    {
        var resultado = await _resultado.FirstOrDefaultAsync(resultado => resultado.ProcedimentoId == resultadoId);
         _resultado.Remove(resultado);
         await _db.SaveChangesAsync();
         return true;

    }
}