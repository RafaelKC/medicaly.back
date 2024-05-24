using Medicaly.Application.Communs;
using Medicaly.Application.Entensions;
using Medicaly.Application.Transients;
using Medicaly.Domain.Profissionais;
using Medicaly.Domain.Profissionais.Dtos;
using Medicaly.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Medicaly.Application.Profissionais;

public interface IProfissionalService
{
    public Task<ProfissionalOutput?> Get(Guid ProfissionalId);
    public Task<ProfissionalOutput?> GetByEmail(string ProfissionalEmail);
    public Task<PagedResult<ProfissionalOutput>> GetList(PagedFilteredInput input);
    public Task<Guid?> Create(ProfissionalInput input);
    public Task<bool> Update(Guid ProfissionalId, ProfissionalInput input);
    public Task<bool> Delete(Guid ProfissionalId);
}

public class ProfissionalService: IProfissionalService, IAutoTransient
{
    private readonly ILogger<ProfissionalService> _logger;
    private readonly IProfissionalEspecialidadeService _profissionalEspecialidadeService;
    private readonly MedicalyDbContext _db;
    private DbSet<Profissional> _Profissionals => _db.Profissionais;

    public ProfissionalService(MedicalyDbContext db, ILogger<ProfissionalService> logger, IProfissionalEspecialidadeService profissionalEspecialidadeService)
    {
        _db = db;
        _logger = logger;
        _profissionalEspecialidadeService = profissionalEspecialidadeService;
    }

    public async Task<ProfissionalOutput?> Get(Guid ProfissionalId)
    {
        var Profissional = await _Profissionals
            .AsNoTracking()
            .Include(p => p.Especialidades)
            .Include(p => p.Atuacoes)
            .Include(p => p.Unidade)
            .Select(Profissional => new ProfissionalOutput
            {
                Id = Profissional.Id,
                Nome = Profissional.Nome,
                Sobrenome = Profissional.Sobrenome,
                Cpf = Profissional.Cpf,
                Email = Profissional.Email,
                Telefone = Profissional.Telefone,
                DataNascimento = Profissional.DataNascimento,
                EnderecoId = Profissional.EnderecoId,
                Genero = Profissional.Genero,
                CredencialDeSaude = Profissional.CredencialDeSaude,
                Tipo = Profissional.Tipo,
                InicioExpediente = Profissional.InicioExpediente.TotalMilliseconds,
                FimExpediente = Profissional.FimExpediente.TotalMilliseconds,
                DiasAtendidos = Profissional.DiasAtendidos,
                UnidadeId = Profissional.UnidadeId,
                Unidade = Profissional.Unidade,
                Especialidades = Profissional.Especialidades.ToList(),
                Atuacoes = Profissional.Atuacoes.ToList(),
            })
            .FirstOrDefaultAsync(Profissional => Profissional.Id == ProfissionalId);
        return Profissional ?? null;
    }

    public async Task<ProfissionalOutput?> GetByCpf(string ProfissionalCpf)
    {
        var Profissional = await _Profissionals.AsNoTracking().FirstOrDefaultAsync(Profissional => Profissional.Cpf == ProfissionalCpf);
        return Profissional != null ? new ProfissionalOutput(Profissional) : null;
    }

    public async Task<ProfissionalOutput?> GetByEmail(string ProfissionalEmail)
    {
        var Profissional = await _Profissionals
            .AsNoTracking()
            .Include(p => p.Especialidades)
            .Include(p => p.Atuacoes)
            .Select(Profissional => new ProfissionalOutput
                {
                    Id = Profissional.Id,
                    Nome = Profissional.Nome,
                    Sobrenome = Profissional.Sobrenome,
                    Cpf = Profissional.Cpf,
                    Email = Profissional.Email,
                    Telefone = Profissional.Telefone,
                    DataNascimento = Profissional.DataNascimento,
                    EnderecoId = Profissional.EnderecoId,
                    Genero = Profissional.Genero,
                    CredencialDeSaude = Profissional.CredencialDeSaude,
                    Tipo = Profissional.Tipo,
                    InicioExpediente = Profissional.InicioExpediente.TotalMilliseconds,
                    FimExpediente = Profissional.FimExpediente.TotalMilliseconds,
                    DiasAtendidos = Profissional.DiasAtendidos,
                    Especialidades = Profissional.Especialidades.ToList(),
                    Atuacoes = Profissional.Atuacoes.ToList(),
                })
            .FirstOrDefaultAsync(Profissional => Profissional.Email == ProfissionalEmail);

        return Profissional ?? null;
    }

    public async Task<PagedResult<ProfissionalOutput>> GetList(PagedFilteredInput input)
    {
        var query = _Profissionals
            .AsNoTracking()
            .Include(p => p.Especialidades)
            .Include(p => p.Atuacoes)
            .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), Profissional =>
                Profissional.Nome.ToLower().Contains(input.Filter.ToLower())
                || Profissional.Sobrenome.ToLower().Contains(input.Filter.ToLower())
            )
            .Select(Profissional => new ProfissionalOutput
            {
                Id = Profissional.Id,
                Nome = Profissional.Nome,
                Sobrenome = Profissional.Sobrenome,
                Cpf = Profissional.Cpf,
                Email = Profissional.Email,
                Telefone = Profissional.Telefone,
                DataNascimento = Profissional.DataNascimento,
                EnderecoId = Profissional.EnderecoId,
                Genero = Profissional.Genero,
                CredencialDeSaude = Profissional.CredencialDeSaude,
                Tipo = Profissional.Tipo,
                InicioExpediente = Profissional.InicioExpediente.TotalMilliseconds,
                FimExpediente = Profissional.FimExpediente.TotalMilliseconds,
                DiasAtendidos = Profissional.DiasAtendidos,
                Especialidades = Profissional.Especialidades.ToList(),
                Atuacoes = Profissional.Atuacoes.ToList(),
            });

        return await query.ToPagedResult(input);
    }

    public async Task<Guid?> Create(ProfissionalInput input)
    {
        try
        {
            var Profissional = new Profissional(input);
            await _Profissionals.AddAsync(Profissional);
            await _db.SaveChangesAsync();

            await _profissionalEspecialidadeService.UpdateEspecialidadesEAtuacoes(input.Id, input.EspecialidadesIds, input.AtuacoesIds);

            return Profissional.Id;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public async Task<bool> Update(Guid ProfissionalId, ProfissionalInput input)
    {
        var Profissional = await _Profissionals.FirstOrDefaultAsync(Profissional => Profissional.Id == ProfissionalId);
        if (Profissional == null) return false;
        Profissional.Update(input);
        await _db.SaveChangesAsync();

        await _profissionalEspecialidadeService.UpdateEspecialidadesEAtuacoes(input.Id, input.EspecialidadesIds, input.AtuacoesIds);

        return true;
    }

    public async Task<bool> Delete(Guid ProfissionalId)
    {
        var Profissional = await _Profissionals.FirstOrDefaultAsync(Profissional => Profissional.Id == ProfissionalId);
        if (Profissional == null) return false;
        _Profissionals.Remove(Profissional);
        await _db.SaveChangesAsync();
        return true;
    }
}