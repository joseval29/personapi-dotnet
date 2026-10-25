using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repositories;

public class TelefonoRepository : ITelefonoRepository
{
    private readonly PersonaDbContext _context;

    public TelefonoRepository(PersonaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Telefono>> GetAllAsync() =>
        await _context.Telefonos.AsNoTracking()
            .Include(t => t.DuenioNavigation)
            .OrderBy(t => t.Duenio).ThenBy(t => t.Num)
            .ToListAsync();

    public async Task<Telefono?> GetByIdAsync(string num) =>
        await _context.Telefonos.AsNoTracking()
            .Include(t => t.DuenioNavigation)
            .FirstOrDefaultAsync(t => t.Num == num);

    public async Task<bool> ExistsAsync(string num) =>
        await _context.Telefonos.AnyAsync(t => t.Num == num);

    public async Task<Telefono> CreateAsync(Telefono telefono)
    {
        _context.Telefonos.Add(telefono);
        await _context.SaveChangesAsync();
        return telefono;
    }

    public async Task<bool> UpdateAsync(Telefono telefono)
    {
        var actual = await _context.Telefonos.FindAsync(telefono.Num);
        if (actual is null) return false;

        actual.Oper = telefono.Oper;
        actual.Duenio = telefono.Duenio;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(string num)
    {
        var actual = await _context.Telefonos.FindAsync(num);
        if (actual is null) return false;

        _context.Telefonos.Remove(actual);
        await _context.SaveChangesAsync();
        return true;
    }
}
