using System.ComponentModel.DataAnnotations;
using Medicaly.Domain.Agendamento.Enums;
using Medicaly.Domain.Agendamentos.Dtos;
using Medicaly.Domain.Communs;
using Medicaly.Domain.Pacientes;
using Medicaly.Domain.Profissionais;
using Medicaly.Domain.ResultadoAnexos;
using Medicaly.Domain.UnidadesAtendimentos;

namespace Medicaly.Domain.Agendamentos;

public class Procedimento: Entity {
    public TipoProcedimento TipoProcedimento { get; set; }

    public Status Status { get; set; }

    [Required]
    public string CodigoTuss { get; set; }

    [Required]
    public DateTime Data { get; set; }

    
    [Required]
    public Guid IdProfissional { get; set; }

    public Profissional Profissional { get; set; }

    [Required]
    public Guid IdPaciente { get; set; }
    
    public Paciente Paciente { get; set; }


    public Guid IdUnidadeAtendimento { get; set; }

    public UnidadeAtendimento UnidadeAtendimento { get; set; }
    


    public Procedimento() {

    }

    public Procedimento(ProcedimentoInput input)
    {
        Id = input.Id != Guid.Empty ? input.Id : Guid.NewGuid();
        TipoProcedimento = input.TipoProcedimento;
        Status = input.Status;
        CodigoTuss = input.CodigoTuss;
        Data = input.Data;
        IdPaciente = input.IdPaciente;
        IdProfissional = input.IdProfissional;
        IdUnidadeAtendimento = input.IdUnidadeAtendimento;
    }


    public void Update(ProcedimentoInput input)
    {
        TipoProcedimento = input.TipoProcedimento;
        Status = input.Status;
        CodigoTuss = input.CodigoTuss;
        Data = input.Data;
        IdPaciente = input.IdPaciente;
        IdProfissional = input.IdProfissional;

    }
}