﻿using System.ComponentModel.DataAnnotations;
using Medicaly.Domain.Communs;
using Medicaly.Domain.Profissionais.Enums;
using Medicaly.Domain.Users;
using Medicaly.Domain.Users.Enums;

namespace Medicaly.Domain.Profissionais.Dtos;

public class ProfissionalInput: EntityDto, IUser
{
    [Required]
    public string Nome { get; set; }

    [Required]
    public string Sobrenome { get; set; }

    [MaxLength(11)]
    [MinLength(11)]
    public string Cpf { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [MaxLength(11)]
    [MinLength(10)]
    public string Telefone { get; set; }

    public DateTime DataNascimento { get; set; }

    public Guid? EnderecoId { get; set; }

    [Required]
    public Genero Genero { get; set; }

    [Required]
    public string CredencialDeSaude { get; set; }

    public List<Guid> AtuacoesIds { get; set; }

    public List<Guid> EspecialidadesIds { get; set; }

    [Required]
    public TipoProfissional Tipo { get; set; }

    [Required]
    public double InicioExpediente { get; set; }

    [Required]
    public double FimExpediente { get; set; }

    [Required]
    public DayOfWeek[] DiasAtendidos { get; set; }

    [Required]
    public Guid UnidadeId { get; set; }
}