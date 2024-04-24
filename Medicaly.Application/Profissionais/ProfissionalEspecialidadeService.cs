using Medicaly.Application.Transients;
using Medicaly.Domain.Profissionais;
using Medicaly.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Medicaly.Application.Profissionais;

public interface IProfissionalEspecialidadeService
{
    public Task UpdateEspecialidades(Guid idProfissional, List<Guid> idsEspecialidades);
}

public class ProfissionalEspecialidadeService: IProfissionalEspecialidadeService, IAutoTransient
{
    private readonly MedicalyDbContext _db;
    private DbSet<ProfissionalEspecialidade> _profissionalEspecialidades => _db.ProfissionalEspecialidades;

    public ProfissionalEspecialidadeService(MedicalyDbContext db)
    {
        _db = db;
    }


    public async Task UpdateEspecialidades(Guid idProfissional, List<Guid> idsEspecialidades)
    {
        var query = _db.ProfissionalEspecialidades
            .Where(pm => pm.IdProsissional == idProfissional);
        _profissionalEspecialidades.RemoveRange(query);

        var novosEspecialidades = idsEspecialidades.Select(id => new ProfissionalEspecialidade()
        {
            IdProsissional = idProfissional,
            IdEspecialidade = id
        }).ToList();

        await _profissionalEspecialidades.AddRangeAsync(novosEspecialidades);
        await _db.SaveChangesAsync();
    }
}