using Medicaly.Application.Communs;
using Medicaly.Application.UnidadesAtendimento;
using Medicaly.Application.UnidadesAtendimento.Dtos;
using Medicaly.Domain.UnidadesAtendimentos.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Medicaly.Api.UnidadesAtendimento;


[Route("unidades-atendimento")]
public class UnidadeAtendimentoController: ControllerBase
{
    private readonly IUnidadeAtendimentoService _unidadeAtendimentoService;
    private readonly ICreateUnidadeAtendimentoService _createUnidadeAtendimentoService;

    public UnidadeAtendimentoController(IUnidadeAtendimentoService unidadeAtendimentoService, ICreateUnidadeAtendimentoService createUnidadeAtendimentoService)
    {
        _unidadeAtendimentoService = unidadeAtendimentoService;
        _createUnidadeAtendimentoService = createUnidadeAtendimentoService;
        
    }

    [HttpGet]
    public async Task<PagedResult<UnidadeAtendimentoOutput>> GetlList([FromQuery] PagedFilteredInput input)
    {
        return await _unidadeAtendimentoService.GetList(input);
    }

    [HttpGet("{unidadeAtendimentoId:guid}")]
    public async Task<ActionResult<UnidadeAtendimentoOutput>> Get([FromRoute] Guid unidadeAtendimentoId)
    {
        var unidadeAtendimento = await _unidadeAtendimentoService.Get(unidadeAtendimentoId);
        return unidadeAtendimento != null ? unidadeAtendimento : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateUnidadeAtendimentoInput input)
    {
        var unidadeAtendimentoId = await _createUnidadeAtendimentoService.CreateUnidadeAtendimento(input);
        return unidadeAtendimentoId.HasValue ? Created($"unidades-atendimento/{unidadeAtendimentoId.Value}", null) : UnprocessableEntity();
    }
    

    [HttpPut("{unidadeAtendimentoId:guid}")]
    public async Task<ActionResult> Update([FromRoute] Guid unidadeAtendimentoId, [FromBody] UnidadeAtendimentoInput input)
    {
        var atuaizado = await _unidadeAtendimentoService.Update(unidadeAtendimentoId, input);
        return atuaizado ? Ok() : NotFound();
    }

    [HttpDelete("{unidadeAtendimentoId:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid unidadeAtendimentoId)
    {
        var deletado = await _unidadeAtendimentoService.Delete(unidadeAtendimentoId);
        return deletado ? Ok() : NotFound();
    }
}