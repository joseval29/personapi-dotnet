using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Dtos;

namespace personapi_dotnet.Controllers;

/// <summary>Controlador MVC (vistas Razor) para estudios.</summary>
[ApiExplorerSettings(IgnoreApi = true)]
public class EstudiosController : Controller
{
    private readonly IEstudioRepository _repo;
    private readonly IPersonaRepository _personas;
    private readonly IProfesionRepository _profesiones;

    public EstudiosController(IEstudioRepository repo, IPersonaRepository personas, IProfesionRepository profesiones)
    {
        _repo = repo;
        _personas = personas;
        _profesiones = profesiones;
    }

    private async Task CargarListasAsync(EstudioDto? dto = null)
    {
        var personas = await _personas.GetAllAsync();
        var profesiones = await _profesiones.GetAllAsync();
        ViewBag.Personas = new SelectList(
            personas.Select(p => new { p.Cc, Texto = $"{p.Cc} - {p.Nombre} {p.Apellido}" }),
            "Cc", "Texto", dto?.CcPer);
        ViewBag.Profesiones = new SelectList(
            profesiones.Select(p => new { p.Id, Texto = $"{p.Id} - {p.Nom}" }),
            "Id", "Texto", dto?.IdProf);
    }

    public async Task<IActionResult> Index()
    {
        var estudios = await _repo.GetAllAsync();
        return View(estudios.Select(e => e.ToDto()).ToList());
    }

    // GET: Estudios/Details?idProf=1&ccPer=1010101010
    public async Task<IActionResult> Details(int idProf, int ccPer)
    {
        var estudio = await _repo.GetByIdAsync(idProf, ccPer);
        return estudio is null ? NotFound() : View(estudio.ToDto());
    }

    public async Task<IActionResult> Create()
    {
        await CargarListasAsync();
        return View(new EstudioDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EstudioDto dto)
    {
        if (ModelState.IsValid)
        {
            if (!await _personas.ExistsAsync(dto.CcPer))
                ModelState.AddModelError(nameof(dto.CcPer), "La persona no existe");
            if (!await _profesiones.ExistsAsync(dto.IdProf))
                ModelState.AddModelError(nameof(dto.IdProf), "La profesión no existe");
            if (await _repo.ExistsAsync(dto.IdProf, dto.CcPer))
                ModelState.AddModelError(string.Empty, "Ese estudio ya está registrado para esa persona");
        }

        if (!ModelState.IsValid)
        {
            await CargarListasAsync(dto);
            return View(dto);
        }

        await _repo.CreateAsync(dto.ToEntity());
        TempData["Mensaje"] = "Estudio registrado correctamente";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int idProf, int ccPer)
    {
        var estudio = await _repo.GetByIdAsync(idProf, ccPer);
        return estudio is null ? NotFound() : View(estudio.ToDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EstudioDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

        if (!await _repo.UpdateAsync(dto.ToEntity())) return NotFound();
        TempData["Mensaje"] = "Estudio actualizado correctamente";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int idProf, int ccPer)
    {
        var estudio = await _repo.GetByIdAsync(idProf, ccPer);
        return estudio is null ? NotFound() : View(estudio.ToDto());
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int idProf, int ccPer)
    {
        await _repo.DeleteAsync(idProf, ccPer);
        TempData["Mensaje"] = "Estudio eliminado correctamente";
        return RedirectToAction(nameof(Index));
    }
}
