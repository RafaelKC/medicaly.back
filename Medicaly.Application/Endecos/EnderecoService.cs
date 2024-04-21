using Medicaly.Application.Communs;
using Medicaly.Application.Entensions;
using Medicaly.Application.Transients;
using Medicaly.Domain.Enderecos;
using Medicaly.Domain.Enderecos.Dtos;
using Medicaly.Domain.Pacientes.Dtos;
using Medicaly.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Medicaly.Application.Enrecos;

public interface IEnderecoService
{
    public Task<EnderecoOutput?> Get(Guid enderecoId);
    public Task<PagedResult<EnderecoOutput>> GetList(PagedFilteredInput input);
    public Task<Guid?> Create(EnderecoInput input);
    public Task<bool> Update(Guid enderecoId, EnderecoInput input);
    public Task<bool> Delete(Guid enderecoId);
}

public class EnderecoService : IEnderecoService, IAutoTransient
{
    private readonly ILogger<EnderecoService> _logger;
    private readonly MedicalyDbContext _db;

    private DbSet<Endereco> _enderecos => _db.Enderecos;

    public EnderecoService(MedicalyDbContext db, ILogger<EnderecoService> logger)
    {
        _db = db;
        _logger = logger;
    }


    public async Task<EnderecoOutput?> Get(Guid enderecoId)
    {
        var endereco = await _enderecos.AsNoTracking().FirstOrDefaultAsync(endereco => endereco.Id == enderecoId);
        return endereco != null ? new EnderecoOutput(endereco) : null;
    }

    public async Task<PagedResult<EnderecoOutput>> GetList(PagedFilteredInput input)
    {
        var query = _enderecos
            .AsNoTracking()
            .Select(endereco => new EnderecoOutput(endereco));

        return await query.ToPagedResult(input);
    }

    public async Task<Guid?> Create(EnderecoInput input)
    {
        try
        {
            var endereco = new Endereco(input);
            await _enderecos.AddAsync(endereco);
            await _db.SaveChangesAsync();
            return endereco.Id;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public async Task<bool> Update(Guid enderecoId, EnderecoInput input)
    {
        var endereco = await _enderecos
            .FirstOrDefaultAsync(endereco => endereco.Id == enderecoId);
        if (endereco == null) return false;
        endereco.Update(input);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(Guid enderecoId)
    {
        var endereco = await _enderecos.FirstOrDefaultAsync(endereco => endereco.Id == enderecoId);
        if (endereco == null) return false;
        _enderecos.Remove(endereco);
        await _db.SaveChangesAsync();
        return true;
    }
}