﻿using Medicaly.Application.Enrecos;
using Medicaly.Application.Profissionais.Dtos;
using Medicaly.Application.Transients;
using Medicaly.Domain.Users;
using Medicaly.Domain.Users.Enums;
using Medicaly.Infrastructure.Authentication;

namespace Medicaly.Application.Profissionais;

public interface ICreateProfissionalService
{
    public Task<Guid?> CreateUser(CreateProfissionalInput input);
}

public class CreateAdministradorService: ICreateProfissionalService, IAutoTransient
{
    private readonly IEnderecoService _enderecoService;
    private readonly IProfissionalService _ProfissionalService;
    private readonly IAuthenticationService _authenticationService;
    public CreateAdministradorService(IEnderecoService enderecoService, IAuthenticationService authenticationService,IProfissionalService ProfissionalService)
    {
        _enderecoService = enderecoService;
        _ProfissionalService = ProfissionalService;
        _authenticationService = authenticationService;
    }

    public async Task<Guid?> CreateUser(CreateProfissionalInput input)
    {
        input.Endereco.Id = input.Endereco.Id != Guid.Empty ? input.Endereco.Id : Guid.NewGuid();
        var enderecoCriado = await _enderecoService.Create(input.Endereco);
        if (enderecoCriado.HasValue)
        {
            input.Profissional.EnderecoId = input.Endereco.Id;
            var usuarioCriado = await _ProfissionalService.Create(input.Profissional);
            if (usuarioCriado.HasValue)
            {
                input.Profissional.Id = usuarioCriado.Value;
               var result =  await _authenticationService.RegisterAsync(input.Profissional.Email, input.Password, new User(input.Profissional, UserTipo.ProfissionalSaude));
                if (result.Success)
                {
                    return usuarioCriado;
                }
            }
        }

        return null;
    }
}