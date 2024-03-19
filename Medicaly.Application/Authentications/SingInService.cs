using Medicaly.Application.Authentications.Dtos;
using Medicaly.Application.Transients;
using Medicaly.Domain.Enderecos;
using Medicaly.Domain.Pacientes;
using Medicaly.Domain.Pacientes.Dtos;
using Medicaly.Domain.Users;
using Medicaly.Domain.Users.Enums;
using Medicaly.Infrastructure.Authentication;
using Medicaly.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Medicaly.Application.Authentications;

public interface ISingUpService
{
    public Task<CreateUserOutput> SingUp(CreateUserInput<PacienteInput> pacienteInput);
}

public class SingUpService: ISingUpService, IAutoTransient
{
    private readonly MedicalyDbContext _medicalyDbContext;
    private readonly IAuthenticationService _authenticationService;

    public SingUpService(MedicalyDbContext medicalyDbContext, IAuthenticationService authenticationService)
    {
        _medicalyDbContext = medicalyDbContext;
        _authenticationService = authenticationService;
    }

    public async Task<CreateUserOutput> SingUp(CreateUserInput<PacienteInput> pacienteInput)
    {
        var paciente = await GetAlredyCreatedPaciente(pacienteInput.User.Cpf);
        var alredyCreated = paciente != null;

        if (alredyCreated) paciente = await UpdatePaciente(paciente, pacienteInput);
        else paciente = await CreatePaciente(pacienteInput);

        var credenciais = await _authenticationService.RegisterAsync(paciente.Email, pacienteInput.Password, new User(paciente, UserTipo.Paciente));

        return new CreateUserOutput
        {
            Success = credenciais.Success,
            RefreshToken = credenciais.RefreshToken,
            Token = credenciais.Token,
        };
    }

    private async Task<Paciente> CreatePaciente(CreateUserInput<PacienteInput> pacienteInput)
    {
        var endereco = new Endereco(pacienteInput.Endereco);
        var paciente = new Paciente(pacienteInput.User);
        paciente.EnderecoId = endereco.Id;
        paciente.Endereco = endereco;

        await _medicalyDbContext.Pacientes.AddAsync(paciente);
        await _medicalyDbContext.SaveChangesAsync();
        _medicalyDbContext.ChangeTracker.Clear();

        return paciente;
    }

    private async Task<Paciente> UpdatePaciente(Paciente paciente, CreateUserInput<PacienteInput> pacienteInput)
    {
        paciente.Endereco.Update(pacienteInput.Endereco);
        paciente.Update(pacienteInput.User);

        _medicalyDbContext.Pacientes.Update(paciente);
        await _medicalyDbContext.SaveChangesAsync();
        _medicalyDbContext.ChangeTracker.Clear();

        return paciente;
    }

    public async Task<Paciente?> GetAlredyCreatedPaciente(string cpf)
    {
        return await _medicalyDbContext.Pacientes
            .Include(paciente => paciente.Endereco)
            .FirstOrDefaultAsync(paciente =>
            paciente.Cpf == cpf);

    }
}