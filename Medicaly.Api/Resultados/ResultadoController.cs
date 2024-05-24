using Medicaly.Application.Communs;
using Medicaly.Application.Resultados;
using Medicaly.Domain.Resultados.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Medicaly.Api.Resultados;

[Route("resultados")]

public class ResultadoController : ControllerBase
{
    private readonly IResultadoService _resultado;

    public ResultadoController(IResultadoService resultadoService)
    {
        _resultado = resultadoService;
        
    }

    [HttpGet]

    public async Task<PagedResult<ResultadoOutput>> GetList([FromQuery] PagedFilteredInput input)
    {
        return await _resultado.GetList(input);
    }

    [HttpGet("{resultadoId:guid}")]

    public async Task<ResultadoOutput> Get(Guid resultadoId)
    {
        return await _resultado.Get(resultadoId);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ResultadoInput input)
    {
        var resultadoId = await _resultado.Create(input);
        return resultadoId.HasValue ? Created($"resultados/{resultadoId.Value}", null) : UnprocessableEntity();
    } 
    
    [HttpPut("{resultadoId:guid}")]
    public async Task<ActionResult> Update([FromRoute] Guid resultadoId, [FromBody] ResultadoInput input)
    {
        var atuaizado = await _resultado.Update(resultadoId, input);
        return atuaizado ? Ok() : NotFound();
    }

    [HttpDelete("{resultadoId:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid resultadoId)
    {
        var deletado = await _resultado.Delete(resultadoId);
        return deletado ? Ok() : NotFound();
    }
    
}