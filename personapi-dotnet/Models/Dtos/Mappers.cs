using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Models.Dtos;

/// <summary>Conversión entre entidades (EF Core) y DTOs.</summary>
public static class Mappers
{
    // ---------- Persona ----------
    public static PersonaDto ToDto(this Persona e) => new()
    {
        Cc = e.Cc,
        Nombre = e.Nombre,
        Apellido = e.Apellido,
        Genero = e.Genero,
        Edad = e.Edad
    };

    public static Persona ToEntity(this PersonaDto d) => new()
    {
        Cc = d.Cc,
        Nombre = d.Nombre.Trim(),
        Apellido = d.Apellido.Trim(),
        Genero = d.Genero.Trim().ToUpperInvariant(),
        Edad = d.Edad
    };

    // ---------- Profesion ----------
    public static ProfesionDto ToDto(this Profesion e) => new()
    {
        Id = e.Id,
        Nom = e.Nom,
        Des = e.Des
    };

    public static Profesion ToEntity(this ProfesionDto d) => new()
    {
        Id = d.Id,
        Nom = d.Nom.Trim(),
        Des = d.Des
    };

    // ---------- Estudio ----------
    public static EstudioDto ToDto(this Estudio e) => new()
    {
        IdProf = e.IdProf,
        CcPer = e.CcPer,
        Fecha = e.Fecha,
        Univer = e.Univer,
        NombrePersona = e.CcPerNavigation is null ? null : $"{e.CcPerNavigation.Nombre} {e.CcPerNavigation.Apellido}",
        NombreProfesion = e.IdProfNavigation?.Nom
    };

    public static Estudio ToEntity(this EstudioDto d) => new()
    {
        IdProf = d.IdProf,
        CcPer = d.CcPer,
        Fecha = d.Fecha,
        Univer = d.Univer
    };

    // ---------- Telefono ----------
    public static TelefonoDto ToDto(this Telefono e) => new()
    {
        Num = e.Num,
        Oper = e.Oper,
        Duenio = e.Duenio,
        NombreDuenio = e.DuenioNavigation is null ? null : $"{e.DuenioNavigation.Nombre} {e.DuenioNavigation.Apellido}"
    };

    public static Telefono ToEntity(this TelefonoDto d) => new()
    {
        Num = d.Num.Trim(),
        Oper = d.Oper.Trim(),
        Duenio = d.Duenio
    };
}
