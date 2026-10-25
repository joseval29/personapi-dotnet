using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces;

/// <summary>DAO de la tabla profesion.</summary>
public interface IProfesionRepository
{
    Task<IEnumerable<Profesion>> GetAllAsync();
    Task<Profesion?> GetByIdAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<Profesion> CreateAsync(Profesion profesion);
    Task<bool> UpdateAsync(Profesion profesion);
    /// <summary>Elimina la profesión junto con los estudios asociados.</summary>
    Task<bool> DeleteAsync(int id);
}
