using System.ComponentModel.DataAnnotations;
using Medicaly.Domain.Communs;
using Medicaly.Domain.Agendamentos.Dtos;
using Medicaly.Domain.Agendamentos.Status;
using Medicaly.Domain.Agendamentos.TipoProcedimento;
using Medicaly.Domain.Profissionais;
using Medicaly.Domain.Pacientes;

namespace Medicaly.Domain.Agendamentos;

public class Agendamento: Entity
{
    public TipoProcedimento TipoProcedimento { get; set; }

    public Status Status { get; set; }

    [Required]
    public string CodigoTuss { get; set; }

    [Required]
    public DateTime Data { get; set; }

    
    [Required]
    public Guid IdProfissional { get; set; }

    public Profissional Profissional { get; set }

    [Required]
    public Guid IdPaciente { get; set; }
    
    public Paciente Paciente { get; set; }


    public Guid IdUnicadeAtendimento { get; set; }



    public Agendamento() {

    }

    public Agendamento(AgendamentoInput input)
    {
        Id = input.Id != Guid.Empty ? input.Id : Guid.NewGuid();
        TipoProcedimento = input.TipoProcedimento;
        Status = input.Status;
        CodigoTuss = input.CodigoTuss;
        Data = input.Data;
        IdPaciente = input.IdPaciente;
        IdProfissional = input.IdProfissional;
    }


    public void Update(AgendamentoInput input)
    {
        TipoProcedimento = input.TipoProcedimento;
        Status = input.Status;
        CodigoTuss = input.CodigoTuss;
        Data = input.Data;
        IdPaciente = input.IdPaciente;
        IdProfissional = input.IdProfissional;

    }
}