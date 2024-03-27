using Medicaly.Application.Communs;
using Medicaly.Application.Enrecos;
using Medicaly.Domain.Enderecos.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medicaly.Api.Endereco;

[Authorize]
[Route("enderecos")]
public class EnderecoController: ControllerBase
{
    private readonly IEnderecoService _enderecoService;

    public EnderecoController(IEnderecoService enderecoService)
    {
        _enderecoService = enderecoService;
    }

    [HttpGet]
    public async Task<PagedResult<EnderecoOutput>> GetlList([FromQuery] PagedFilteredInput input)
    {
        return await _enderecoService.GetList(input);
    }

    [HttpGet("{enderecoId:guid}")]
    public async Task<ActionResult<EnderecoOutput>> Get([FromRoute] Guid enderecoId)
    {
        var endereco = await _enderecoService.Get(enderecoId);
        return endereco != null ? endereco : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] EnderecoInput input)
    {
        var enderecoId = await _enderecoService.Create(input);
        return enderecoId.HasValue ? Created(new Uri($"enderecos/{enderecoId.Value}"), null) : UnprocessableEntity();
    }

    [HttpPut("{enderecoId:guid}")]
    public async Task<ActionResult> Update([FromRoute] Guid enderecoId, [FromBody] EnderecoInput input)
    {
        var atuaizado = await _enderecoService.Update(enderecoId, input);
        return atuaizado ? Ok() : NotFound();
    }

    [HttpDelete("{enderecoId:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid enderecoId)
    {
        var deletado = await _enderecoService.Delete(enderecoId);
        return deletado ? Ok() : NotFound();
    }
}