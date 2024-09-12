using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medicaly.Application.UnidadesAtendimento;
using Medicaly.Domain.Enderecos;
using Medicaly.Domain.UnidadesAtendimentos;
using Medicaly.Domain.UnidadesAtendimentos.Dtos;
using Medicaly.Domain.UnidadesAtendimentos.Enums;
using Medicaly.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace Medicaly.Test;

public class UnidadeAtendimentoServiceTests: BaseTestWithDbContext
{
    private readonly Mock<ILogger<UnidadeAtendimentoService>> _loggerMock;
    private readonly UnidadeAtendimentoService _service;
    
    
    public UnidadeAtendimentoServiceTests()
    {
        _loggerMock = new Mock<ILogger<UnidadeAtendimentoService>>();
        _service = new UnidadeAtendimentoService(DbContext, _loggerMock.Object);
    }
    
    [Fact]
    public async Task TesteDeleteComSucesso()
    {
        // Arrange
        var unidadeAtendimentoId = Guid.NewGuid();
        var enderecoId = Guid.NewGuid();

        var endereco = new Endereco
        {
            Id = enderecoId,
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
            Id = unidadeAtendimentoId,
            Nome = "Unidade Atendimento",
            TipoUnidade = TipoUnidade.Clinica,
            EnderecoId = enderecoId,
        };
        DbContext.Enderecos.Add(endereco);
        DbContext.UnidadeAtendimentos.Add(unidade);
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();
        
        // Act
        var result = await _service.Delete(unidadeAtendimentoId);

        // Assert
        Assert.True(result);
        Assert.Equal(0, await DbContext.UnidadeAtendimentos.CountAsync());
    }
    
    [Fact]
    public async Task TesteDeleteSemSucesso()
    {
        // Arrange
        var unidadeAtendimentoId = Guid.NewGuid();
        
        // Act
        var result = await _service.Delete(unidadeAtendimentoId);

        // Assert
        Assert.False(result);
        Assert.Equal(0, await DbContext.UnidadeAtendimentos.CountAsync());
    }

    [Fact]
    public async Task CreateUnidade()
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
        DbContext.Enderecos.Add(endereco);
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();
        
        var unidade = new UnidadeAtendimentoInput
        {
            Id = Guid.NewGuid(),
            Nome = "Unidade Atendimento",
            TipoUnidade = TipoUnidade.Clinica,
            EnderecoId = endereco.Id,
        };
        

        // Act
        var result = await _service.Create(unidade);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, await DbContext.UnidadeAtendimentos.CountAsync());
    }
}