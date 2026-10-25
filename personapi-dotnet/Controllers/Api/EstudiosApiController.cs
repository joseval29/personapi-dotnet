using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Dtos;

namespace personapi_dotnet.Controllers.Api;

/// <summary>Endpoints REST para la entidad estudios (llave compuesta idProf + ccPer).</summary>
[ApiController]
[Route("api/estudios")]
[Produces("application/json")]
public class EstudiosApiController : ControllerBase
{
    private readonly IEstudioRepository _repo;
    private readonly IPersonaRepository _personas;
    private readonly IProfesionRepository _profesiones;

    public EstudiosApiController(IEstudioRepository repo, IPersonaRepository personas, IProfesionRepository profesiones)
    {
        _repo = repo;
        _personas = personas;
        _profesiones = profesiones;
    }

    /// <summary>Lista todos los estudios.</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstudioDto>>> GetAll()
    {
        var estudios = await _repo.GetAllAsync();
        return Ok(estudios.Select(e => e.ToDto()));
    }

    /// <summary>Obtiene un estudio por profesión y persona.</summary>
    [HttpGet("{idProf:int}/{ccPer:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EstudioDto>> GetById(int idProf, int ccPer)
    {
        var estudio = await _repo.GetByIdAsync(idProf, ccPer);
        return estudio is null ? NotFound() : Ok(estudio.ToDto());
    }

    /// <summary>Registra un estudio de una persona en una profesión.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<EstudioDto>> Create([FromBody] EstudioDto dto)
    {
        if (!await _personas.ExistsAsync(dto.CcPer))
            return BadRequest(new { mensaje = $"No existe la persona con cédula {dto.CcPer}" });
        if (!await _profesiones.ExistsAsync(dto.IdProf))
            return BadRequest(new { mensaje = $"No existe la profesión con id {dto.IdProf}" });
        if (await _repo.ExistsAsync(dto.IdProf, dto.CcPer))
            return Conflict(new { mensaje = "Ese estudio ya está registrado" });

        var creado = await _repo.CreateAsync(dto.ToEntity());
        var resultado = await _repo.GetByIdAsync(creado.IdProf, creado.CcPer);
        return CreatedAtAction(nameof(GetById), new { idProf = creado.IdProf, ccPer = creado.CcPer }, resultado!.ToDto());
    }

    /// <summary>Actualiza fecha y universidad de un estudio.</summary>
    [HttpPut("{idProf:int}/{ccPer:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int idProf, int ccPer, [FromBody] EstudioDto dto)
    {
        if (idProf != dto.IdProf || ccPer != dto.CcPer)
            return BadRequest(new { mensaje = "La llave de la URL no coincide con la del cuerpo" });

        return await _repo.UpdateAsync(dto.ToEntity()) ? NoContent() : NotFound();
    }

    /// <summary>Elimina un estudio.</summary>
    [HttpDelete("{idProf:int}/{ccPer:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int idProf, int ccPer)
    {
        return await _repo.DeleteAsync(idProf, ccPer) ? NoContent() : NotFound();
    }
}
