﻿using System.ComponentModel.DataAnnotations;
using Medicaly.Domain.Communs;
using Medicaly.Domain.Enderecos;
using Medicaly.Domain.UnidadeAtendimento.Dtos;
using Medicaly.Domain.UnidadeAtendimento.Enums;
using Newtonsoft.Json;

namespace Medicaly.Domain.UnidadeAtendimento;

public class UnidadeAtendimento : Entity
{
    [Required]
    [MinLength(10)]
    [MaxLength(50)]
    public string Nome { get; set; }

    [Required] public TipoUnidade TipoUnidade { get; set; }
    
    [Required] public Guid EnderecoId { get; set; }
    
    public Endereco Endereco { get; set; }

    public UnidadeAtendimento()
    {
        
    }
    public UnidadeAtendimento(UnidadeAtendimentoInput input)
    {
        Id = input.Id;
        Nome = input.Nome;
        TipoUnidade = input.TipoUnidade;
        EnderecoId = input.EnderecoId;
    }

    public void Update(UnidadeAtendimentoInput input)
    {
        Nome = input.Nome;
        TipoUnidade = input.TipoUnidade;
        EnderecoId = input.EnderecoId;
    }
    
}