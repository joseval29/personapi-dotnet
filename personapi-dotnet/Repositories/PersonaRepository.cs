using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repositories;

public class PersonaRepository : IPersonaRepository
{
    private readonly PersonaDbContext _context;

    public PersonaRepository(PersonaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Persona>> GetAllAsync() =>
        await _context.Personas.AsNoTracking().OrderBy(p => p.Apellido).ThenBy(p => p.Nombre).ToListAsync();

    public async Task<Persona?> GetByIdAsync(int cc) =>
        await _context.Personas.AsNoTracking().FirstOrDefaultAsync(p => p.Cc == cc);

    public async Task<bool> ExistsAsync(int cc) =>
        await _context.Personas.AnyAsync(p => p.Cc == cc);

    public async Task<Persona> CreateAsync(Persona persona)
    {
        _context.Personas.Add(persona);
        await _context.SaveChangesAsync();
        return persona;
    }

    public async Task<bool> UpdateAsync(Persona persona)
    {
        var actual = await _context.Personas.FindAsync(persona.Cc);
        if (actual is null) return false;

        actual.Nombre = persona.Nombre;
        actual.Apellido = persona.Apellido;
        actual.Genero = persona.Genero;
        actual.Edad = persona.Edad;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int cc)
    {
        var actual = await _context.Personas.FindAsync(cc);
        if (actual is null) return false;

        // Las FK no tienen ON DELETE CASCADE: se eliminan primero los dependientes
        _context.Estudios.RemoveRange(_context.Estudios.Where(e => e.CcPer == cc));
        _context.Telefonos.RemoveRange(_context.Telefonos.Where(t => t.Duenio == cc));
        _context.Personas.Remove(actual);

        await _context.SaveChangesAsync();
        return true;
    }
}
