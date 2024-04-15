using Medicaly.Application.Communs;
using Medicaly.Application.Pacientes;
using Medicaly.Application.Pacientes.Dtos;
using Medicaly.Domain.Pacientes.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medicaly.Api.Pacientes;

[Route("pacientes")]
public class PacienteController: ControllerBase
{
    private readonly IPacienteService _pacienteService;
    private readonly ICreatePacienteService _createPacienteService;

    public PacienteController(IPacienteService pacienteService, ICreatePacienteService createPacienteService)
    {
        _pacienteService = pacienteService;
        _createPacienteService = createPacienteService;
    }

    [HttpGet]
    public async Task<PagedResult<PacienteOutput>> GetlList([FromQuery] PagedFilteredInput input)
    {
        return await _pacienteService.GetList(input);
    }

    [HttpGet("{pacienteId:guid}")]
    public async Task<ActionResult<PacienteOutput>> Get([FromRoute] Guid pacienteId)
    {
        var paciente = await _pacienteService.Get(pacienteId);
        return paciente != null ? paciente : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateMedicoInput input)
    {
        var pacienteId = await _createPacienteService.CreateUser(input);
        return pacienteId.HasValue ? Created($"pacientes/{pacienteId.Value}", null) : UnprocessableEntity();
    }

    [HttpPut("{pacienteId:guid}")]
    public async Task<ActionResult> Update([FromRoute] Guid pacienteId, [FromBody] PacienteInput input)
    {
        var atuaizado = await _pacienteService.Update(pacienteId, input);
        return atuaizado ? Ok() : NotFound();
    }

    [HttpDelete("{pacienteId:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid pacienteId)
    {
        var deletado = await _pacienteService.Delete(pacienteId);
        return deletado ? Ok() : NotFound();
    }
}