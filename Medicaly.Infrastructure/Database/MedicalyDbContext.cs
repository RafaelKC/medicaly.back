using Medicaly.Domain.Administradores;
using Medicaly.Domain.Enderecos;
using Medicaly.Domain.Pacientes;
using Medicaly.Domain.Profissionais;
using Microsoft.EntityFrameworkCore;

namespace Medicaly.Infrastructure.Database;

public class MedicalyDbContext: DbContext
{
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Administrador> Administradores { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Profissional> Profissionais { get; set; }

    public MedicalyDbContext()
    {
    }
    
    public MedicalyDbContext(
        DbContextOptions<MedicalyDbContext> options): base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("public");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseNpgsql();
        }
    }
}