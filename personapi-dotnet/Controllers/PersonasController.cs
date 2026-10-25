using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Dtos;

namespace personapi_dotnet.Controllers;

/// <summary>Controlador MVC (vistas Razor) para persona.</summary>
[ApiExplorerSettings(IgnoreApi = true)]
public class PersonasController : Controller
{
    private readonly IPersonaRepository _repo;

    public PersonasController(IPersonaRepository repo)
    {
        _repo = repo;
    }

    // GET: Personas
    public async Task<IActionResult> Index()
    {
        var personas = await _repo.GetAllAsync();
        return View(personas.Select(p => p.ToDto()).ToList());
    }

    // GET: Personas/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var persona = await _repo.GetByIdAsync(id);
        return persona is null ? NotFound() : View(persona.ToDto());
    }

    // GET: Personas/Create
    public IActionResult Create() => View(new PersonaDto());

    // POST: Personas/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PersonaDto dto)
    {
        if (ModelState.IsValid && await _repo.ExistsAsync(dto.Cc))
            ModelState.AddModelError(nameof(dto.Cc), $"Ya existe una persona con cédula {dto.Cc}");

        if (!ModelState.IsValid) return View(dto);

        await _repo.CreateAsync(dto.ToEntity());
        TempData["Mensaje"] = "Persona creada correctamente";
        return RedirectToAction(nameof(Index));
    }

    // GET: Personas/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var persona = await _repo.GetByIdAsync(id);
        return persona is null ? NotFound() : View(persona.ToDto());
    }

    // POST: Personas/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PersonaDto dto)
    {
        if (id != dto.Cc) return BadRequest();
        if (!ModelState.IsValid) return View(dto);

        if (!await _repo.UpdateAsync(dto.ToEntity())) return NotFound();
        TempData["Mensaje"] = "Persona actualizada correctamente";
        return RedirectToAction(nameof(Index));
    }

    // GET: Personas/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var persona = await _repo.GetByIdAsync(id);
        return persona is null ? NotFound() : View(persona.ToDto());
    }

    // POST: Personas/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _repo.DeleteAsync(id);
        TempData["Mensaje"] = "Persona eliminada correctamente";
        return RedirectToAction(nameof(Index));
    }
}
