using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Dtos;

namespace personapi_dotnet.Controllers.Api;

/// <summary>Endpoints REST para la entidad persona.</summary>
[ApiController]
[Route("api/personas")]
[Produces("application/json")]
public class PersonasApiController : ControllerBase
{
    private readonly IPersonaRepository _repo;

    public PersonasApiController(IPersonaRepository repo)
    {
        _repo = repo;
    }

    /// <summary>Lista todas las personas.</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonaDto>>> GetAll()
    {
        var personas = await _repo.GetAllAsync();
        return Ok(personas.Select(p => p.ToDto()));
    }

    /// <summary>Obtiene una persona por su cédula.</summary>
    [HttpGet("{cc:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PersonaDto>> GetById(int cc)
    {
        var persona = await _repo.GetByIdAsync(cc);
        return persona is null ? NotFound() : Ok(persona.ToDto());
    }

    /// <summary>Crea una persona.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<PersonaDto>> Create([FromBody] PersonaDto dto)
    {
        if (await _repo.ExistsAsync(dto.Cc))
            return Conflict(new { mensaje = $"Ya existe una persona con cédula {dto.Cc}" });

        var creada = await _repo.CreateAsync(dto.ToEntity());
        return CreatedAtAction(nameof(GetById), new { cc = creada.Cc }, creada.ToDto());
    }

    /// <summary>Actualiza una persona existente.</summary>
    [HttpPut("{cc:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int cc, [FromBody] PersonaDto dto)
    {
        if (cc != dto.Cc)
            return BadRequest(new { mensaje = "La cédula de la URL no coincide con la del cuerpo" });

        return await _repo.UpdateAsync(dto.ToEntity()) ? NoContent() : NotFound();
    }

    /// <summary>Elimina una persona (y sus estudios y teléfonos).</summary>
    [HttpDelete("{cc:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int cc)
    {
        return await _repo.DeleteAsync(cc) ? NoContent() : NotFound();
    }
}
