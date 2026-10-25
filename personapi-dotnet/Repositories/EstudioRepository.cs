using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repositories;

public class EstudioRepository : IEstudioRepository
{
    private readonly PersonaDbContext _context;

    public EstudioRepository(PersonaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Estudio>> GetAllAsync() =>
        await _context.Estudios.AsNoTracking()
            .Include(e => e.CcPerNavigation)
            .Include(e => e.IdProfNavigation)
            .OrderBy(e => e.CcPer).ThenBy(e => e.IdProf)
            .ToListAsync();

    public async Task<Estudio?> GetByIdAsync(int idProf, int ccPer) =>
        await _context.Estudios.AsNoTracking()
            .Include(e => e.CcPerNavigation)
            .Include(e => e.IdProfNavigation)
            .FirstOrDefaultAsync(e => e.IdProf == idProf && e.CcPer == ccPer);

    public async Task<bool> ExistsAsync(int idProf, int ccPer) =>
        await _context.Estudios.AnyAsync(e => e.IdProf == idProf && e.CcPer == ccPer);

    public async Task<Estudio> CreateAsync(Estudio estudio)
    {
        _context.Estudios.Add(estudio);
        await _context.SaveChangesAsync();
        return estudio;
    }

    public async Task<bool> UpdateAsync(Estudio estudio)
    {
        var actual = await _context.Estudios.FindAsync(estudio.IdProf, estudio.CcPer);
        if (actual is null) return false;

        // La llave (id_prof, cc_per) no se modifica; solo los atributos
        actual.Fecha = estudio.Fecha;
        actual.Univer = estudio.Univer;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int idProf, int ccPer)
    {
        var actual = await _context.Estudios.FindAsync(idProf, ccPer);
        if (actual is null) return false;

        _context.Estudios.Remove(actual);
        await _context.SaveChangesAsync();
        return true;
    }
}
