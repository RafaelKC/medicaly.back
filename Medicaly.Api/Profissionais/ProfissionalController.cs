using Medicaly.Application.Communs;
using Medicaly.Application.Profissionais;
using Medicaly.Application.Profissionais.Dtos;
using Medicaly.Application.Profissionals;
using Medicaly.Domain.Profissionais.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medicaly.Api.Profissionais;

[ApiController]
[Route("profissionais")]
[Authorize]
public class ProfissionalController: ControllerBase
{
    private readonly IProfissionalService _profissionaisService;
    private readonly ICreateProfissionalService _createProfissionaiservice;

    public ProfissionalController(IProfissionalService profissionaisService, ICreateProfissionalService createProfissionaiservice)
    {
        _profissionaisService = profissionaisService;
        _createProfissionaiservice = createProfissionaiservice;
    }

    [HttpGet]
    public async Task<PagedResult<ProfissionalOutput>> GetlList([FromQuery] PagedFilteredInput input)
    {
        return await _profissionaisService.GetList(input);
    }

    [HttpGet("{ProfissionalId:guid}")]
    public async Task<ActionResult<ProfissionalOutput>> Get([FromRoute] Guid ProfissionalId)
    {
        var Profissional = await _profissionaisService.Get(ProfissionalId);
        return Profissional != null ? Profissional : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateProfissionalInput input)
    {
        var ProfissionalId = await _createProfissionaiservice.CreateUser(input);
        return ProfissionalId.HasValue ? Created($"profissionais/{ProfissionalId.Value}", null) : UnprocessableEntity();
    }

    [HttpPut("{ProfissionalId:guid}")]
    public async Task<ActionResult> Update([FromRoute] Guid ProfissionalId, [FromBody] ProfissionalInput input)
    {
        var atuaizado = await _profissionaisService.Update(ProfissionalId, input);
        return atuaizado ? Ok() : NotFound();
    }

    [HttpDelete("{ProfissionalId:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid ProfissionalId)
    {
        var deletado = await _profissionaisService.Delete(ProfissionalId);
        return deletado ? Ok() : NotFound();
    }
}