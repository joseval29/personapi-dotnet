using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Dtos;

namespace personapi_dotnet.Controllers.Api;

/// <summary>Endpoints REST para la entidad profesion.</summary>
[ApiController]
[Route("api/profesiones")]
[Produces("application/json")]
public class ProfesionesApiController : ControllerBase
{
    private readonly IProfesionRepository _repo;

    public ProfesionesApiController(IProfesionRepository repo)
    {
        _repo = repo;
    }

    /// <summary>Lista todas las profesiones.</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProfesionDto>>> GetAll()
    {
        var profesiones = await _repo.GetAllAsync();
        return Ok(profesiones.Select(p => p.ToDto()));
    }

    /// <summary>Obtiene una profesión por id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProfesionDto>> GetById(int id)
    {
        var profesion = await _repo.GetByIdAsync(id);
        return profesion is null ? NotFound() : Ok(profesion.ToDto());
    }

    /// <summary>Crea una profesión.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ProfesionDto>> Create([FromBody] ProfesionDto dto)
    {
        if (await _repo.ExistsAsync(dto.Id))
            return Conflict(new { mensaje = $"Ya existe una profesión con id {dto.Id}" });

        var creada = await _repo.CreateAsync(dto.ToEntity());
        return CreatedAtAction(nameof(GetById), new { id = creada.Id }, creada.ToDto());
    }

    /// <summary>Actualiza una profesión.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] ProfesionDto dto)
    {
        if (id != dto.Id)
            return BadRequest(new { mensaje = "El id de la URL no coincide con el del cuerpo" });

        return await _repo.UpdateAsync(dto.ToEntity()) ? NoContent() : NotFound();
    }

    /// <summary>Elimina una profesión (y los estudios asociados).</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        return await _repo.DeleteAsync(id) ? NoContent() : NotFound();
    }
}
