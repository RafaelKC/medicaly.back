using Medicaly.Domain.Agendamento.Enums;
using Medicaly.Domain.Agendamentos;
using Medicaly.Domain.Communs;

namespace Medicaly.Domain.Procedimentos.Dtos;

public class ProcedimentoOutput : EntityDto
{
    public TipoProcedimento TipoProcedimento { get; set; }
    public Status Status { get; set; }
    public string CodigoTuss { get; set; }
    public DateTime Data { get; set; }
    public Guid IdPaciente { get; set; }
    public Guid IdProfissional { get; set; }
    public Guid IdUnidadeAtendimento { get; set; }


    public ProcedimentoOutput(Procedimento input)
    {
        Id = input.Id;
        TipoProcedimento = input.TipoProcedimento;
        CodigoTuss = input.CodigoTuss;
        Status = input.Status;
        Data = input.Data;
        IdPaciente = input.IdPaciente;
        IdProfissional = input.IdProfissional;
        IdUnidadeAtendimento = input.IdUnidadeAtendimento;

    }
}