﻿// <auto-generated />
using System;
using Medicaly.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Medicaly.Infrastructure.Migrations
{
    [DbContext(typeof(MedicalyDbContext))]
    [Migration("20240424234620_AddEspecialidades")]
    partial class AddEspecialidades
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Medicaly.Domain.Administradores.Administrador", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("EnderecoId")
                        .HasColumnType("uuid");

                    b.Property<int>("Genero")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.HasKey("Id");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("EnderecoId");

                    b.ToTable("Administradores", "public");
                });

            modelBuilder.Entity("Medicaly.Domain.Agendamentos.Procedimento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CodigoTuss")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Data")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("IdPaciente")
                        .HasColumnType("uuid");

                    b.Property<Guid>("IdProfissional")
                        .HasColumnType("uuid");

                    b.Property<Guid>("IdUnidadeAtendimento")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("TipoProcedimento")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("IdPaciente");

                    b.HasIndex("IdProfissional");

                    b.HasIndex("IdUnidadeAtendimento");

                    b.ToTable("Procedimentos", "public");
                });

            modelBuilder.Entity("Medicaly.Domain.Anexos.Anexo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BucketEndereco")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("BucketPrefix")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DataUltimaModificacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Extencao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Anexos", "public");
                });

            modelBuilder.Entity("Medicaly.Domain.Enderecos.Endereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("character varying(8)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CodigoIbgeCidade")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Complemento")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("character varying(2)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Numero")
                        .HasColumnType("integer");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Enderecos", "public");
                });

            modelBuilder.Entity("Medicaly.Domain.Especialidades.Especialidade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Especialidades", "public");
                });

            modelBuilder.Entity("Medicaly.Domain.Pacientes.Paciente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("EnderecoId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<int>("Genero")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.HasKey("Id");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("EnderecoId");

                    b.ToTable("Pacientes", "public");
                });

            modelBuilder.Entity("Medicaly.Domain.Profissionais.Profissional", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Atuacoes")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.Property<string>("CredencialDeSaude")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int[]>("DiasAtendidos")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("EnderecoId")
                        .HasColumnType("uuid");

                    b.Property<TimeSpan>("FimExpediente")
                        .HasColumnType("interval");

                    b.Property<int>("Genero")
                        .HasColumnType("integer");

                    b.Property<TimeSpan>("InicioExpediente")
                        .HasColumnType("interval");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.Property<int>("Tipo")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.HasIndex("CredencialDeSaude")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("EnderecoId");

                    b.ToTable("Profissionais", "public");
                });

            modelBuilder.Entity("Medicaly.Domain.Profissionais.ProfissionalEspecialidade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("IdEspecialidade")
                        .HasColumnType("uuid");

                    b.Property<Guid>("IdProsissional")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("IdProsissional");

                    b.HasIndex("IdEspecialidade", "IdProsissional")
                        .IsUnique();

                    b.ToTable("ProfissionalEspecialidades", "public");
                });

            modelBuilder.Entity("Medicaly.Domain.UnidadeAtendimento.UnidadeAtendimento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("EnderecoId")
                        .HasColumnType("uuid");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("TipoUnidade")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId");

                    b.ToTable("UnidadeAtendimentos", "public");
                });

            modelBuilder.Entity("Medicaly.Domain.Administradores.Administrador", b =>
                {
                    b.HasOne("Medicaly.Domain.Enderecos.Endereco", null)
                        .WithMany()
                        .HasForeignKey("EnderecoId");
                });

            modelBuilder.Entity("Medicaly.Domain.Agendamentos.Procedimento", b =>
                {
                    b.HasOne("Medicaly.Domain.Pacientes.Paciente", "Paciente")
                        .WithMany()
                        .HasForeignKey("IdPaciente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Medicaly.Domain.Profissionais.Profissional", "Profissional")
                        .WithMany()
                        .HasForeignKey("IdProfissional")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Medicaly.Domain.UnidadeAtendimento.UnidadeAtendimento", null)
                        .WithMany()
                        .HasForeignKey("IdUnidadeAtendimento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");

                    b.Navigation("Profissional");
                });

            modelBuilder.Entity("Medicaly.Domain.Pacientes.Paciente", b =>
                {
                    b.HasOne("Medicaly.Domain.Enderecos.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("Medicaly.Domain.Profissionais.Profissional", b =>
                {
                    b.HasOne("Medicaly.Domain.Enderecos.Endereco", null)
                        .WithMany()
                        .HasForeignKey("EnderecoId");
                });

            modelBuilder.Entity("Medicaly.Domain.Profissionais.ProfissionalEspecialidade", b =>
                {
                    b.HasOne("Medicaly.Domain.Especialidades.Especialidade", "Especialidade")
                        .WithMany("ProfissionalEspecialidades")
                        .HasForeignKey("IdEspecialidade")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Medicaly.Domain.Profissionais.Profissional", "Profissional")
                        .WithMany("ProfissionalEspecialidades")
                        .HasForeignKey("IdProsissional")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Especialidade");

                    b.Navigation("Profissional");
                });

            modelBuilder.Entity("Medicaly.Domain.UnidadeAtendimento.UnidadeAtendimento", b =>
                {
                    b.HasOne("Medicaly.Domain.Enderecos.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("Medicaly.Domain.Especialidades.Especialidade", b =>
                {
                    b.Navigation("ProfissionalEspecialidades");
                });

            modelBuilder.Entity("Medicaly.Domain.Profissionais.Profissional", b =>
                {
                    b.Navigation("ProfissionalEspecialidades");
                });
#pragma warning restore 612, 618
        }
    }
}
