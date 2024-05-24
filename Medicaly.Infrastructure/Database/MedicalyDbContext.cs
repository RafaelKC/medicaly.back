using Medicaly.Domain.Administradores;
using Medicaly.Domain.Agendamentos;
using Medicaly.Domain.Anexos;
using Medicaly.Domain.Procedimentos;
using Medicaly.Domain.Enderecos;
using Medicaly.Domain.Especialidades;
using Medicaly.Domain.Pacientes;
using Medicaly.Domain.Profissionais;
using Medicaly.Domain.ResultadoAnexos;
using Medicaly.Domain.Resultados;
using Medicaly.Domain.Resultados.Dtos;
using Medicaly.Domain.UnidadesAtendimentos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
    public DbSet<Especialidade> Especialidades { get; set; }
    public DbSet<ProfissionalEspecialidade> ProfissionalEspecialidades { get; set; }
    public DbSet<ProfissionalAtuacao> ProfissionalAtuacaos { get; set; }
    
    public DbSet<Resultado> Resultados { get; set; }

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

        modelBuilder.Entity<ProfissionalEspecialidade>(pm =>
        {
            pm
                .HasIndex(p => new { p.IdEspecialidade, p.IdProsissional })
                .IsUnique();
        });

        modelBuilder.Entity<ProfissionalAtuacao>(pm =>
        {
            pm
                .HasIndex(p => new { p.IdEspecialidade, p.IdProsissional })
                .IsUnique();
        });

        modelBuilder.Entity<Especialidade>(pm =>
        {
            pm
                .HasIndex(p => new { p.Nome })
                .IsUnique();
        });

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

            profissionalModel
                .HasMany(e => e.Especialidades)
                .WithMany(e => e.Profissionais)
                .UsingEntity<ProfissionalEspecialidade>(
                    l => l
                        .HasOne(e => e.Especialidade)
                        .WithMany(e => e.ProfissionalEspecialidades)
                        .HasForeignKey(e => e.IdEspecialidade),
                    r => r
                        .HasOne(e => e.Profissional)
                        .WithMany(e => e.ProfissionalEspecialidades)
                        .HasForeignKey(e => e.IdProsissional));

            profissionalModel
                .HasMany(e => e.Atuacoes)
                .WithMany(e => e.ProfissionaisAtuacoes)
                .UsingEntity<ProfissionalAtuacao>(
                    l => l
                        .HasOne(e => e.Especialidade)
                        .WithMany(e => e.ProfissionalAtuacoes)
                        .HasForeignKey(e => e.IdEspecialidade),
                    r => r
                        .HasOne(e => e.Profissional)
                        .WithMany(e => e.ProfissionalAtuacoes)
                        .HasForeignKey(e => e.IdProsissional));

            profissionalModel
                .HasOne(e => e.Unidade)
                .WithMany()
                .HasForeignKey(e => e.UnidadeId);
        });

        modelBuilder.Entity<Procedimento>(procedimentoModel =>
            {
                procedimentoModel.HasOne(a => a.Paciente).WithMany()
                    .HasForeignKey(a => a.IdPaciente);

                procedimentoModel.HasOne(a => a.Profissional).WithMany()
                    .HasForeignKey(a => a.IdProfissional);

                procedimentoModel.HasOne(a => a.UnidadeAtendimento).WithMany()
                    .HasForeignKey(a => a.IdUnidadeAtendimento);
                


            }
        );
        
        modelBuilder.Entity<UnidadeAtendimento>(UnidadeAtendimentoModel =>
            {
                UnidadeAtendimentoModel.HasOne(a => a.Endereco).WithMany()
                    .HasForeignKey(a => a.EnderecoId);

            }
        );

        modelBuilder.Entity<Resultado>(ResultadoModel =>
        {
            ResultadoModel.HasKey(a => a.ProcedimentoId);
        });

        modelBuilder.Entity<ResultadoAnexo>(ResultadoAnexoModel =>
        {
            ResultadoAnexoModel.HasKey(a => new { a.AnexoId, a.ProcedimentoId });
            ResultadoAnexoModel.HasOne(a => a.Anexo)
                .WithOne(a => a.ResultadoAnexos).HasPrincipalKey<Anexo>(a => a.Id)
                .HasForeignKey<ResultadoAnexo>(a => a.AnexoId);
            ResultadoAnexoModel.HasOne(a => a.Resultado)
                .WithOne(a => a.ResultadoAnexo).HasPrincipalKey<Resultado>(a => a.ProcedimentoId)
                .HasForeignKey<ResultadoAnexo>(a => a.ProcedimentoId);

        });
    }
    
    

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseNpgsql();
        }
    }
}