using Medicaly.Application.Anexos;
using Medicaly.Application.Anexos.Dtos;
using Medicaly.Application.Communs;
using Medicaly.Domain.Anexos.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Medicaly.Api.Anexos;

[Route("anexos")]
public class AnexoController: ControllerBase
{
    private readonly IAnexoService _anexoService;
    private readonly IAnexoCreateAndGetService _anexoCreateAndGetService;

    public AnexoController(IAnexoService anexoService, IAnexoCreateAndGetService anexoCreateAndGetService)
    {
        _anexoService = anexoService;
        _anexoCreateAndGetService = anexoCreateAndGetService;
    }

    [HttpGet]
    public async Task<PagedResult<AnexoComLinkOutput>> GetlList([FromQuery] PagedFilteredInput input)
    {
        return await _anexoCreateAndGetService.GetList(input);
    }

    [HttpGet("{anexoId:guid}")]
    public async Task<ActionResult<AnexoComLinkOutput>> Get([FromRoute] Guid anexoId)
    {
        var endereco = await _anexoCreateAndGetService.Get(anexoId);
        return endereco != null ? endereco : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<AnexoCreatedOutput>> Create([FromBody] AnexoInput input)
    {
        var anexo = await _anexoCreateAndGetService.Create(input);
        return anexo != null ? Created($"enderecos/{anexo.AnexoId}", anexo) : UnprocessableEntity();
    }


    [HttpDelete("{anexoId:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid anexoId)
    {
        var deletado = await _anexoCreateAndGetService.Delete(anexoId);
        return deletado ? Ok() : NotFound();
    }
}