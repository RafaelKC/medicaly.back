using Medicaly.Domain.Administradores;
using Medicaly.Domain.Agendamentos;
using Medicaly.Domain.Anexos;
using Medicaly.Domain.Procedimentos;
using Medicaly.Domain.Enderecos;
using Medicaly.Domain.Pacientes;
using Medicaly.Domain.Profissionais;
using Medicaly.Domain.UnidadeAtendimento;
using Microsoft.EntityFrameworkCore;

namespace Medicaly.Infrastructure.Database;

public class MedicalyDbContext: DbContext
{
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Administrador> Administradores { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Profissional> Profissionais { get; set; }
    public DbSet<Procedimento> Procedimentos { get; set; }
    public DbSet<UnidadeAtendimento> UnidadeAtendimentos { get; set; }
    public DbSet<Anexo> Anexos { get; set; }

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

        modelBuilder.Entity<Administrador>(administradorModel =>
        {
            administradorModel.HasIndex(administrador => administrador.Email).IsUnique();
            administradorModel.HasIndex(administrador => administrador.Cpf).IsUnique();

            administradorModel
                .HasOne<Endereco>()
                .WithMany()
                .HasForeignKey(administrador => administrador.EnderecoId);
        });

        modelBuilder.Entity<Paciente>(pacienteModel =>
        {
            pacienteModel.HasIndex(paciente => paciente.Email).IsUnique();
            pacienteModel.HasIndex(paciente => paciente.Cpf).IsUnique();

            pacienteModel
                .HasOne(paciente => paciente.Endereco)
                .WithMany()
                .HasForeignKey(paciente => paciente.EnderecoId);
        });

        modelBuilder.Entity<Profissional>(profissionalModel =>
        {
            profissionalModel.HasIndex(profissional => profissional.Email).IsUnique();
            profissionalModel.HasIndex(profissional => profissional.Cpf).IsUnique();
            profissionalModel.HasIndex(profissional => profissional.CredencialDeSaude).IsUnique();

            profissionalModel
                .HasOne<Endereco>()
                .WithMany()
                .HasForeignKey(profissional => profissional.EnderecoId);
        });

        modelBuilder.Entity<Procedimento>(procedimentoModel =>
            {
                procedimentoModel.HasOne(a => a.Paciente).WithMany()
                    .HasForeignKey(a => a.IdPaciente);

                procedimentoModel.HasOne(a => a.Profissional).WithMany()
                    .HasForeignKey(a => a.IdProfissional);

                procedimentoModel.HasOne<UnidadeAtendimento>().WithMany()
                    .HasForeignKey(a => a.IdUnidadeAtendimento);


            }
        );
        
        modelBuilder.Entity<UnidadeAtendimento>(UnidadeAtendimentoModel =>
            {
                UnidadeAtendimentoModel.HasOne(a => a.Endereco).WithMany()
                    .HasForeignKey(a => a.EnderecoId);

            }
        );
        
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseNpgsql();
        }
    }
}