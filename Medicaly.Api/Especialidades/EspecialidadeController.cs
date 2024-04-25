using Medicaly.Application.Communs;
using Medicaly.Application.Especialidades;
using Medicaly.Domain.Especialidades.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medicaly.Api.Especialidades;


[Route("especialidades")]
[Authorize]
public class EspecialidadeController: ControllerBase
{
    private readonly IEspecialidadeService _especialidadeService;

    public EspecialidadeController(IEspecialidadeService especialidadeService)
    {
        _especialidadeService = especialidadeService;
    }

    [HttpGet]
    [Authorize]
    public async Task<PagedResult<EspecialidadeModel>> GetlList([FromQuery] PagedFilteredInput input)
    {
        return await _especialidadeService.GetList(input);
    }

    [HttpGet("{especialidadeId:guid}")]
    [Authorize]
    public async Task<ActionResult<EspecialidadeModel>> Get([FromRoute] Guid especialidadeId)
    {
        var especialidade = await _especialidadeService.Get(especialidadeId);
        return especialidade != null ? especialidade : NotFound();
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Create([FromBody] EspecialidadeModel input)
    {
        var especialidadeId = await _especialidadeService.Create(input);
        return especialidadeId.HasValue ? Created($"especialidades/{especialidadeId.Value}", null) : UnprocessableEntity();
    }

    [HttpPut("{especialidadeId:guid}")]
    [Authorize]
    public async Task<ActionResult> Update([FromRoute] Guid especialidadeId, [FromBody] EspecialidadeModel input)
    {
        var atuaizado = await _especialidadeService.Update(especialidadeId, input);
        return atuaizado ? Ok() : NotFound();
    }

    [HttpDelete("{especialidadeId:guid}")]
    [Authorize]
    public async Task<ActionResult> Delete([FromRoute] Guid especialidadeId)
    {
        var deletado = await _especialidadeService.Delete(especialidadeId);
        return deletado ? Ok() : NotFound();
    }
}