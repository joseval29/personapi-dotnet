using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Dtos;

namespace personapi_dotnet.Controllers.Api;

/// <summary>Endpoints REST para la entidad telefono.</summary>
[ApiController]
[Route("api/telefonos")]
[Produces("application/json")]
public class TelefonosApiController : ControllerBase
{
    private readonly ITelefonoRepository _repo;
    private readonly IPersonaRepository _personas;

    public TelefonosApiController(ITelefonoRepository repo, IPersonaRepository personas)
    {
        _repo = repo;
        _personas = personas;
    }

    /// <summary>Lista todos los teléfonos.</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TelefonoDto>>> GetAll()
    {
        var telefonos = await _repo.GetAllAsync();
        return Ok(telefonos.Select(t => t.ToDto()));
    }

    /// <summary>Obtiene un teléfono por número.</summary>
    [HttpGet("{num}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TelefonoDto>> GetById(string num)
    {
        var telefono = await _repo.GetByIdAsync(num);
        return telefono is null ? NotFound() : Ok(telefono.ToDto());
    }

    /// <summary>Crea un teléfono.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<TelefonoDto>> Create([FromBody] TelefonoDto dto)
    {
        if (!await _personas.ExistsAsync(dto.Duenio))
            return BadRequest(new { mensaje = $"No existe la persona con cédula {dto.Duenio}" });
        if (await _repo.ExistsAsync(dto.Num))
            return Conflict(new { mensaje = $"Ya existe el teléfono {dto.Num}" });

        var creado = await _repo.CreateAsync(dto.ToEntity());
        var resultado = await _repo.GetByIdAsync(creado.Num);
        return CreatedAtAction(nameof(GetById), new { num = creado.Num }, resultado!.ToDto());
    }

    /// <summary>Actualiza operador y dueño de un teléfono.</summary>
    [HttpPut("{num}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string num, [FromBody] TelefonoDto dto)
    {
        if (num != dto.Num)
            return BadRequest(new { mensaje = "El número de la URL no coincide con el del cuerpo" });
        if (!await _personas.ExistsAsync(dto.Duenio))
            return BadRequest(new { mensaje = $"No existe la persona con cédula {dto.Duenio}" });

        return await _repo.UpdateAsync(dto.ToEntity()) ? NoContent() : NotFound();
    }

    /// <summary>Elimina un teléfono.</summary>
    [HttpDelete("{num}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string num)
    {
        return await _repo.DeleteAsync(num) ? NoContent() : NotFound();
    }
}
