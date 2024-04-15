using Medicaly.Application.Administradores;
using Medicaly.Application.Administradores.Dtos;
using Medicaly.Application.Communs;
using Medicaly.Domain.Administradores.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medicaly.Api.Administradores;

[ApiController]
[Route("administradores")]
[Authorize]
public class AdministradorController: ControllerBase
{
    private readonly IAdministradorService _profissionaisService;
    private readonly ICreateAdministradorService _createProfissionaiservice;

    public AdministradorController(IAdministradorService profissionaisService, ICreateAdministradorService createProfissionaiservice)
    {
        _profissionaisService = profissionaisService;
        _createProfissionaiservice = createProfissionaiservice;
    }

    [HttpGet]
    public async Task<PagedResult<AdministradorOutput>> GetlList([FromQuery] PagedFilteredInput input)
    {
        return await _profissionaisService.GetList(input);
    }

    [HttpGet("{administradorId:guid}")]
    public async Task<ActionResult<AdministradorOutput>> Get([FromRoute] Guid administradorId)
    {
        var Administrador = await _profissionaisService.Get(administradorId);
        return Administrador != null ? Administrador : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateAdministradorInput input)
    {
        var administradorId = await _createProfissionaiservice.CreateUser(input);
        return administradorId.HasValue ? Created($"administradores/{administradorId.Value}", null) : UnprocessableEntity();
    }

    [HttpPut("{administradorId:guid}")]
    public async Task<ActionResult> Update([FromRoute] Guid administradorId, [FromBody] AdministradorInput input)
    {
        var atuaizado = await _profissionaisService.Update(administradorId, input);
        return atuaizado ? Ok() : NotFound();
    }

    [HttpDelete("{administradorId:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid administradorId)
    {
        var deletado = await _profissionaisService.Delete(administradorId);
        return deletado ? Ok() : NotFound();
    }
}