using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repositories;

public class ProfesionRepository : IProfesionRepository
{
    private readonly PersonaDbContext _context;

    public ProfesionRepository(PersonaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Profesion>> GetAllAsync() =>
        await _context.Profesions.AsNoTracking().OrderBy(p => p.Id).ToListAsync();

    public async Task<Profesion?> GetByIdAsync(int id) =>
        await _context.Profesions.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

    public async Task<bool> ExistsAsync(int id) =>
        await _context.Profesions.AnyAsync(p => p.Id == id);

    public async Task<Profesion> CreateAsync(Profesion profesion)
    {
        _context.Profesions.Add(profesion);
        await _context.SaveChangesAsync();
        return profesion;
    }

    public async Task<bool> UpdateAsync(Profesion profesion)
    {
        var actual = await _context.Profesions.FindAsync(profesion.Id);
        if (actual is null) return false;

        actual.Nom = profesion.Nom;
        actual.Des = profesion.Des;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var actual = await _context.Profesions.FindAsync(id);
        if (actual is null) return false;

        _context.Estudios.RemoveRange(_context.Estudios.Where(e => e.IdProf == id));
        _context.Profesions.Remove(actual);

        await _context.SaveChangesAsync();
        return true;
    }
}
