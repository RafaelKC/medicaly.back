using System.ComponentModel.DataAnnotations;
using Medicaly.Domain.Communs;
using Medicaly.Domain.Procedimentos.Enums;

namespace Medicaly.Domain.Procedimentos.Dtos;

public class ProcedimentoInput: EntityDto
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


    public Guid IdUnidadeAtendimento { get; set; }



}