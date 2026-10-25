using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Dtos;

namespace personapi_dotnet.Controllers;

/// <summary>Controlador MVC (vistas Razor) para telefono.</summary>
[ApiExplorerSettings(IgnoreApi = true)]
public class TelefonosController : Controller
{
    private readonly ITelefonoRepository _repo;
    private readonly IPersonaRepository _personas;

    public TelefonosController(ITelefonoRepository repo, IPersonaRepository personas)
    {
        _repo = repo;
        _personas = personas;
    }

    private async Task CargarPersonasAsync(int? seleccionada = null)
    {
        var personas = await _personas.GetAllAsync();
        ViewBag.Personas = new SelectList(
            personas.Select(p => new { p.Cc, Texto = $"{p.Cc} - {p.Nombre} {p.Apellido}" }),
            "Cc", "Texto", seleccionada);
    }

    public async Task<IActionResult> Index()
    {
        var telefonos = await _repo.GetAllAsync();
        return View(telefonos.Select(t => t.ToDto()).ToList());
    }

    public async Task<IActionResult> Details(string id)
    {
        var telefono = await _repo.GetByIdAsync(id);
        return telefono is null ? NotFound() : View(telefono.ToDto());
    }

    public async Task<IActionResult> Create()
    {
        await CargarPersonasAsync();
        return View(new TelefonoDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TelefonoDto dto)
    {
        if (ModelState.IsValid)
        {
            if (!await _personas.ExistsAsync(dto.Duenio))
                ModelState.AddModelError(nameof(dto.Duenio), "La persona no existe");
            if (await _repo.ExistsAsync(dto.Num))
                ModelState.AddModelError(nameof(dto.Num), $"Ya existe el teléfono {dto.Num}");
        }

        if (!ModelState.IsValid)
        {
            await CargarPersonasAsync(dto.Duenio);
            return View(dto);
        }

        await _repo.CreateAsync(dto.ToEntity());
        TempData["Mensaje"] = "Teléfono creado correctamente";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(string id)
    {
        var telefono = await _repo.GetByIdAsync(id);
        if (telefono is null) return NotFound();
        await CargarPersonasAsync(telefono.Duenio);
        return View(telefono.ToDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, TelefonoDto dto)
    {
        if (id != dto.Num) return BadRequest();

        if (ModelState.IsValid && !await _personas.ExistsAsync(dto.Duenio))
            ModelState.AddModelError(nameof(dto.Duenio), "La persona no existe");

        if (!ModelState.IsValid)
        {
            await CargarPersonasAsync(dto.Duenio);
            return View(dto);
        }

        if (!await _repo.UpdateAsync(dto.ToEntity())) return NotFound();
        TempData["Mensaje"] = "Teléfono actualizado correctamente";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(string id)
    {
        var telefono = await _repo.GetByIdAsync(id);
        return telefono is null ? NotFound() : View(telefono.ToDto());
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        await _repo.DeleteAsync(id);
        TempData["Mensaje"] = "Teléfono eliminado correctamente";
        return RedirectToAction(nameof(Index));
    }
}
