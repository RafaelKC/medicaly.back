using Medicaly.Application.Communs;
using Medicaly.Application.Transients;
using Medicaly.Domain.ResultadoAnexos;
using Medicaly.Domain.ResultadoAnexos.Dtos;
using Medicaly.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Medicaly.Application.ResultadoAnexos;

public interface IResultadoAnexoService
{ 
    public Task<Guid?> Create(ResultadoAnexoInput input);
}
public class ResultadoAnexoService : IResultadoAnexoService, IAutoTransient
{
    private readonly ILogger<ResultadoAnexoService> _logger;
    private readonly MedicalyDbContext _db;
    private DbSet<ResultadoAnexo> _resultadoAnexo => _db.ResultadoAnexos;
    
    public ResultadoAnexoService (MedicalyDbContext db, ILogger<ResultadoAnexoService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<Guid?> Create(ResultadoAnexoInput input)
    {
        try
        {
            var resultadoAnexo = new ResultadoAnexo(input);
            await _resultadoAnexo.AddAsync(resultadoAnexo);
            await _db.SaveChangesAsync();
            return resultadoAnexo.ProcedimentoId;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}