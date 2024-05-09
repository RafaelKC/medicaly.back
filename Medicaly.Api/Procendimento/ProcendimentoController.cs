using Medicaly.Application.Communs;
using Medicaly.Application.Procedimentos;
using Medicaly.Application.Procedimentos.Dtos;
using Medicaly.Application.UnidadesAtendimento;
using Medicaly.Domain.Agendamentos.Dtos;
using Medicaly.Domain.Procedimentos.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Medicaly.Api.Procendimento;


[Route("procedimentos")]
public class ProcedimentoController: ControllerBase
{
    private readonly IProcedimentoService _procedimento;

    public ProcedimentoController(IProcedimentoService procedimentoService)
    {
        _procedimento = procedimentoService;
        
    }

    [HttpGet]
    public async Task<PagedResult<ProcedimentoOutput>> GetlList([FromQuery] GetListProcedimentoInput input)
    {
        return await _procedimento.GetList(input);
    }

    [HttpGet("{procedimentoId:guid}")]
    public async Task<ActionResult<ProcedimentoOutput>> Get([FromRoute] Guid procedimentoId)
    {
        var procedimento = await _procedimento.Get(procedimentoId);
        return procedimento != null ? procedimento : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ProcedimentoInput input)
    {
        var procedimentoId = await _procedimento.Create(input);
        return procedimentoId.HasValue ? Created($"procedimentos/{procedimentoId.Value}", null) : UnprocessableEntity();
    }
    

    [HttpPut("{procedimentoId:guid}")]
    public async Task<ActionResult> Update([FromRoute] Guid procedimentoId, [FromBody] ProcedimentoInput input)
    {
        var atuaizado = await _procedimento.Update(procedimentoId, input);
        return atuaizado ? Ok() : NotFound();
    }

    [HttpDelete("{procedimentoId:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid procedimentoId)
    {
        var deletado = await _procedimento.Delete(procedimentoId);
        return deletado ? Ok() : NotFound();
    }
}