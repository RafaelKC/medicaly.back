﻿using Medicaly.Application.Communs;
using Medicaly.Application.Entensions;
using Medicaly.Application.Procedimentos.Dtos;
using Medicaly.Application.Transients;
using Medicaly.Domain.Procedimentos;
using Medicaly.Domain.Procedimentos.Dtos;
using Medicaly.Domain.Procedimentos.Enums;
using Medicaly.Domain.Profissionais;
using Medicaly.Domain.Resultados.Dtos;
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
    private DbSet<Profissional> _profissionais => _db.Profissionais;

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
            .WhereIf(input.ProfissionalId.HasValue, a => input.ProfissionalId.Value == a.IdProfissional)
            .WhereIf(input.PacienteId.HasValue, a => input.PacienteId.Value == a.IdPaciente)
            .Include(procedimento => procedimento.Paciente)
            .Include(procedimento => procedimento.Profissional)
            .Include(procedimento => procedimento.UnidadeAtendimento)
            .Include(procedimento => procedimento.Resultado)
            .ThenInclude(resultado => resultado.Anexos);

        var result = await query.ToPagedResult(input);
        return new PagedResult<ProcedimentoOutput>
        {
            TotalCount = result.TotalCount,
            Items = result.Items.Select(procedimento => new ProcedimentoOutput(procedimento)
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
                UnidadeAtendimento = procedimento.UnidadeAtendimento,
                Resultado = procedimento.Resultado == null ? null : new ResultadoOutput
                {
                    ProcedimentoId = procedimento.Resultado.ProcedimentoId,
                    Observacoes = procedimento.Resultado.Observacoes,
                    TemAnexo = procedimento.Resultado.Anexos != null && procedimento.Resultado.Anexos.Any()
                }
            }).ToList()
        };
    }

    public async Task<Guid?> Create(ProcedimentoInput input)
    {
        input.Data = input.Data.ToLocalTime();

        var horarioAtendimento = input.Data - input.Data.Date;
        var finalAtendimento = horarioAtendimento.Add(TimeSpan.FromHours(1));

        var profissional = await _profissionais.AsNoTracking().FirstOrDefaultAsync(p => p.Id == input.IdProfissional);
        var profissionalAtendeNesseHorario = profissional != null && profissional.InicioExpediente <= horarioAtendimento &&
                                             profissional.FimExpediente >= finalAtendimento &&  (profissional.DiasAtendidos == null || profissional.DiasAtendidos.Contains(input.Data.DayOfWeek));

        var dataValida = input.Data > DateTime.Now;

        var inicioConflitoAtendimento = input.Data;
        inicioConflitoAtendimento = inicioConflitoAtendimento.AddHours(-1);
        var finalConflitoAtendimento = input.Data;
        finalConflitoAtendimento = finalConflitoAtendimento.AddHours(1);

        var temConflito = await _procedimento.AsNoTracking()
            .Where(p => p.IdProfissional == input.IdProfissional)
            .Where(p => p.Status == Status.Ativo || p.Status == Status.EmAndamento)
            .AnyAsync(p => inicioConflitoAtendimento < p.Data && p.Data < finalConflitoAtendimento);


        if (temConflito || !profissionalAtendeNesseHorario || !dataValida)
        {
            return null;
        }


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