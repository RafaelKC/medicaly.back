using Medicaly.Application.ResultadoAnexos;
using Medicaly.Domain.ResultadoAnexos.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medicaly.Api.ResultadoAnexos;

[Route("resultadoAnexo")]
[Authorize]
public class ResultadoAnexosController: ControllerBase
{
    private readonly IResultadoAnexoService _resultadoAnexo;

    public ResultadoAnexosController(IResultadoAnexoService resultadoAnexo)
    {
        _resultadoAnexo = resultadoAnexo;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ResultadoAnexoInput input)
    {
        var resultadoAnexoId = await _resultadoAnexo.Create(input);
        return resultadoAnexoId.HasValue ? Created($"resultadoAnexo/{resultadoAnexoId.Value}", null) : UnprocessableEntity();
    }
}