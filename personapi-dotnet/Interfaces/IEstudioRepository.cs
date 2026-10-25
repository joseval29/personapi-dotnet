using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces;

/// <summary>DAO de la tabla estudios (llave compuesta id_prof + cc_per).</summary>
public interface IEstudioRepository
{
    Task<IEnumerable<Estudio>> GetAllAsync();
    Task<Estudio?> GetByIdAsync(int idProf, int ccPer);
    Task<bool> ExistsAsync(int idProf, int ccPer);
    Task<Estudio> CreateAsync(Estudio estudio);
    Task<bool> UpdateAsync(Estudio estudio);
    Task<bool> DeleteAsync(int idProf, int ccPer);
}
