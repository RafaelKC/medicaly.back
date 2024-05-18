using Medicaly.Application.Communs;
using Medicaly.Application.Entensions;
using Medicaly.Application.Procedimentos.Dtos;
using Medicaly.Application.Transients;
using Medicaly.Domain.Agendamentos;
using Medicaly.Domain.Agendamentos.Dtos;
using Medicaly.Domain.Procedimentos.Dtos;
using Medicaly.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Medicaly.Application.Procedimentos;

public interface IProcedimentoService
{
    public Task<ProcedimentoOutput?> Get(Guid procedimentoId);
    public Task<PagedResult<ProcedimentoOutput>> GetList(GetListProcedimentoInput input);
    public Task<Guid?> Create(ProcedimentoInput input);
    public Task<bool> Update(Guid procedimentoId, ProcedimentoInput input);
    public Task<bool> Delete(Guid procedimentoId);
}

public class ProcedimentoService: IProcedimentoService, IAutoTransient
{
    private readonly ILogger<ProcedimentoService> _logger;
    private readonly MedicalyDbContext _db;

    private DbSet<Procedimento> _procedimento => _db.Procedimentos;

    public ProcedimentoService(MedicalyDbContext db, ILogger<ProcedimentoService> logger)
    {
        _db = db;
        _logger = logger;
    }


    public async Task<ProcedimentoOutput?> Get(Guid procedimentoId)
    {
        var procedimento = await _procedimento.AsNoTracking().FirstOrDefaultAsync(procedimento => procedimento.Id == procedimentoId);
        return procedimento != null ? new ProcedimentoOutput(procedimento) : null;
    }

    public async Task<PagedResult<ProcedimentoOutput>> GetList(GetListProcedimentoInput input)
    {
        var query = _procedimento
            .AsNoTracking()
            .WhereIf(input.ProfissionalId.HasValue, a => input.ProfissionalId.Value==a.IdProfissional)
            .Include(procedimento => procedimento.Profissional)
            .Include(procedimento => procedimento.Paciente)
            .Select(procedimento => new ProcedimentoOutput
            {
                Id = procedimento.Id,
                TipoProcedimento = procedimento.TipoProcedimento,
                CodigoTuss = procedimento.CodigoTuss,
                Status = procedimento.Status,
                Data = procedimento.Data,
                IdPaciente = procedimento.IdPaciente,
                IdProfissional = procedimento.IdProfissional,
                IdUnidadeAtendimento = procedimento.IdUnidadeAtendimento,
                Profissional = procedimento.Profissional,
                Paciente = procedimento.Paciente,
            });

        return await query.ToPagedResult(input);
    }

    public async Task<Guid?> Create(ProcedimentoInput input)
    {
        try
        {
            var procedimento = new Procedimento(input);
            await _procedimento.AddAsync(procedimento);
            await _db.SaveChangesAsync();
            return procedimento.Id;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public async Task<bool> Update(Guid procedimentoId, ProcedimentoInput input)
    {
        var procedimento = await _procedimento
            .FirstOrDefaultAsync(procedimento => procedimento.Id == procedimentoId);
        if (procedimento == null) return false;
        procedimento.Update(input);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(Guid procedimentoId)
    {
        var procedimento = await _procedimento.FirstOrDefaultAsync(procedimento => procedimento.Id == procedimentoId);
        if (procedimento == null) return false;
        _procedimento.Remove(procedimento);
        await _db.SaveChangesAsync();
        return true;
    }
}