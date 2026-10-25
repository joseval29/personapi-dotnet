using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces;

/// <summary>DAO de la tabla persona.</summary>
public interface IPersonaRepository
{
    Task<IEnumerable<Persona>> GetAllAsync();
    Task<Persona?> GetByIdAsync(int cc);
    Task<bool> ExistsAsync(int cc);
    Task<Persona> CreateAsync(Persona persona);
    Task<bool> UpdateAsync(Persona persona);
    /// <summary>Elimina la persona junto con sus estudios y teléfonos.</summary>
    Task<bool> DeleteAsync(int cc);
}
