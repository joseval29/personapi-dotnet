using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces;

/// <summary>DAO de la tabla telefono.</summary>
public interface ITelefonoRepository
{
    Task<IEnumerable<Telefono>> GetAllAsync();
    Task<Telefono?> GetByIdAsync(string num);
    Task<bool> ExistsAsync(string num);
    Task<Telefono> CreateAsync(Telefono telefono);
    Task<bool> UpdateAsync(Telefono telefono);
    Task<bool> DeleteAsync(string num);
}
