using System.ComponentModel.DataAnnotations;
using Medicaly.Domain.Communs;
using Medicaly.Domain.Agendamento.Status;
using Medicaly.Domain.Agendamento.TipoProcedimento;

namespace Medicaly.Domain.Agendamentos.Dtos;

public class AgendamentoInput: EntityDto
{
    public TipoProcedimento TipoProcedimento { get; set; }

    public Status Status { get; set; }

    [Required]
    public string CodigoTuss { get; set; }

    [Required]
    public DateTime Data { get; set; }

    [Required]
    public Guid IdProfissional { get; set; }

    [Required]
    public Guid IdPaciente { get; set; }


    public Guid IdUnicadeAtendimento { get; set; }



}