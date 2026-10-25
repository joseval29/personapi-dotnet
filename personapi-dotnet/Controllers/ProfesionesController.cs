using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Dtos;

namespace personapi_dotnet.Controllers;

/// <summary>Controlador MVC (vistas Razor) para profesion.</summary>
[ApiExplorerSettings(IgnoreApi = true)]
public class ProfesionesController : Controller
{
    private readonly IProfesionRepository _repo;

    public ProfesionesController(IProfesionRepository repo)
    {
        _repo = repo;
    }

    public async Task<IActionResult> Index()
    {
        var profesiones = await _repo.GetAllAsync();
        return View(profesiones.Select(p => p.ToDto()).ToList());
    }

    public async Task<IActionResult> Details(int id)
    {
        var profesion = await _repo.GetByIdAsync(id);
        return profesion is null ? NotFound() : View(profesion.ToDto());
    }

    public IActionResult Create() => View(new ProfesionDto());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProfesionDto dto)
    {
        if (ModelState.IsValid && await _repo.ExistsAsync(dto.Id))
            ModelState.AddModelError(nameof(dto.Id), $"Ya existe una profesión con id {dto.Id}");

        if (!ModelState.IsValid) return View(dto);

        await _repo.CreateAsync(dto.ToEntity());
        TempData["Mensaje"] = "Profesión creada correctamente";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var profesion = await _repo.GetByIdAsync(id);
        return profesion is null ? NotFound() : View(profesion.ToDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProfesionDto dto)
    {
        if (id != dto.Id) return BadRequest();
        if (!ModelState.IsValid) return View(dto);

        if (!await _repo.UpdateAsync(dto.ToEntity())) return NotFound();
        TempData["Mensaje"] = "Profesión actualizada correctamente";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var profesion = await _repo.GetByIdAsync(id);
        return profesion is null ? NotFound() : View(profesion.ToDto());
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _repo.DeleteAsync(id);
        TempData["Mensaje"] = "Profesión eliminada correctamente";
        return RedirectToAction(nameof(Index));
    }
}
