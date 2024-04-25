using Medicaly.Application.Transients;
using Medicaly.Domain.Profissionais;
using Medicaly.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Medicaly.Application.Profissionais;

public interface IProfissionalEspecialidadeService
{
    public Task UpdateEspecialidadesEAtuacoes(Guid idProfissional, List<Guid> idsEspecialidades, List<Guid> idsAtuacoes);
}

public class ProfissionalEspecialidadeService: IProfissionalEspecialidadeService, IAutoTransient
{
    private readonly MedicalyDbContext _db;
    private DbSet<ProfissionalEspecialidade> _profissionalEspecialidades => _db.ProfissionalEspecialidades;
    private DbSet<ProfissionalAtuacao> _profissionalAtuacaos => _db.ProfissionalAtuacaos;

    public ProfissionalEspecialidadeService(MedicalyDbContext db)
    {
        _db = db;
    }


    public async Task UpdateEspecialidadesEAtuacoes(Guid idProfissional, List<Guid> idsEspecialidades, List<Guid> idsAtuacoes)
    {
        await UpdateEspecialidades(idProfissional, idsEspecialidades);
        await UpdateAtuacoes(idProfissional, idsAtuacoes);
        await _db.SaveChangesAsync();
    }

    private async Task UpdateEspecialidades(Guid idProfissional, List<Guid> idsEspecialidades)
    {
        var query = _profissionalEspecialidades
            .Where(pm => pm.IdProsissional == idProfissional);
        _profissionalEspecialidades.RemoveRange(query);

        var novosEspecialidades = idsEspecialidades
            .Distinct()
            .Select(id => new ProfissionalEspecialidade()
            {
                IdProsissional = idProfissional,
                IdEspecialidade = id
            }).ToList();

        await _profissionalEspecialidades.AddRangeAsync(novosEspecialidades);
    }

    private async Task UpdateAtuacoes(Guid idProfissional, List<Guid> idsAtuacoes)
    {
        var query = _profissionalAtuacaos
            .Where(pm => pm.IdProsissional == idProfissional);
        _profissionalAtuacaos.RemoveRange(query);

        var novasAtuacoes = idsAtuacoes
            .Distinct()
            .Select(id => new ProfissionalAtuacao
            {
                IdProsissional = idProfissional,
                IdEspecialidade = id
            }).ToList();

        await _profissionalAtuacaos.AddRangeAsync(novasAtuacoes);
    }
}