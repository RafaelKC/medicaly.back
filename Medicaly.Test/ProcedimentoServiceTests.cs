using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medicaly.Application.Procedimentos;
using Medicaly.Domain.UnidadesAtendimentos;
using Medicaly.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;
using Medicaly.Application.Procedimentos.Dtos;
using Medicaly.Domain.Enderecos;
using Medicaly.Domain.Pacientes;
using Medicaly.Domain.Procedimentos.Dtos;
using Medicaly.Domain.Procedimentos.Enums;
using Medicaly.Domain.Profissionais;
using Medicaly.Domain.Profissionais.Enums;
using Medicaly.Domain.UnidadesAtendimentos.Enums;
using Medicaly.Domain.Users.Enums;


namespace Medicaly.Test;

public class ProcedimentoServiceTests: BaseTestWithDbContext
{
    private readonly Mock<ILogger<ProcedimentoService>> _loggerMock;
    private readonly ProcedimentoService _service;
    
    
    public ProcedimentoServiceTests()
    {
        _loggerMock = new Mock<ILogger<ProcedimentoService>>();
        _service = new ProcedimentoService(DbContext, _loggerMock.Object);
    }
    
    [Fact]
    public async Task TestCreate()
    {
        // Arrange
        var endereco = new Endereco
        {
            Id = Guid.NewGuid(),
            Cep = "12345678",
            Estado = "PR",
            Bairro = "Bairro",
            Cidade = "Cidade",
            CodigoIbgeCidade = "IbgeCidade",
            Complemento = "Complemento",
            Logradouro = "Logradouro",
            Numero = 0,
            Observacao = "Observacao",
        };
        var unidade = new UnidadeAtendimento
        {
            Id = Guid.NewGuid(),
            Nome = "Unidade Atendimento",
            TipoUnidade = TipoUnidade.Clinica,
            EnderecoId = endereco.Id,
        };
        var profissional = new Profissional
        {
            Id = Guid.NewGuid(),
            EnderecoId = endereco.Id,
            UnidadeId = unidade.Id,
            FimExpediente = TimeSpan.FromHours(24),
            InicioExpediente = TimeSpan.FromHours(0),
            Tipo = TipoProfissional.Enfermeiro,
            CredencialDeSaude = "CredencialDeSaude",
            Genero = Genero.Feminino,
            DataNascimento = DateTime.Now,
            Telefone = "12345678910",
            Email = "email@email.com",
            Cpf = "1234567890",
            Nome = "Nome",
            Sobrenome = "Sobrenome",
        };
        var paciente = new Paciente
        {
            Id = Guid.NewGuid(),
            EnderecoId = endereco.Id,
            Genero = Genero.Feminino,
            DataNascimento = DateTime.Now,
            Telefone = "12345678910",
            Email = "email@email.com",
            Cpf = "1234567890",
            Nome = "Nome",
            Sobrenome = "Sobrenome",
        };
        
        DbContext.Enderecos.Add(endereco);
        DbContext.UnidadeAtendimentos.Add(unidade);
        DbContext.Pacientes.Add(paciente);
        DbContext.Profissionais.Add(profissional);
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();
        
        var input = new ProcedimentoInput()
        {
            Id = Guid.NewGuid(),
            TipoProcedimento = TipoProcedimento.Consulta,
            IdProfissional = profissional.Id,
            IdPaciente = paciente.Id,
            IdUnidadeAtendimento = unidade.Id,
            Data = DateTime.Now.AddDays(1),
            CodigoTuss = "dsa",
            Status = Status.Ativo
        };
        
        // Act
        var result = await _service.Create(input);
        
        // Assert
        Assert.NotNull(result);
    }

    
}