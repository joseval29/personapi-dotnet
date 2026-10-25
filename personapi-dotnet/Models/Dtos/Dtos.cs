using System.ComponentModel.DataAnnotations;

namespace personapi_dotnet.Models.Dtos;

/// <summary>Objeto de transferencia para persona (API y vistas).</summary>
public class PersonaDto
{
    [Display(Name = "Cédula")]
    [Required(ErrorMessage = "La cédula es obligatoria")]
    [Range(1, int.MaxValue, ErrorMessage = "La cédula debe ser un número positivo de máximo 10 dígitos (≤ 2147483647)")]
    public int Cc { get; set; }

    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(45)]
    public string Nombre { get; set; } = string.Empty;

    [Display(Name = "Apellido")]
    [Required(ErrorMessage = "El apellido es obligatorio")]
    [StringLength(45)]
    public string Apellido { get; set; } = string.Empty;

    [Display(Name = "Género")]
    [Required(ErrorMessage = "El género es obligatorio")]
    [RegularExpression("^[MF]$", ErrorMessage = "El género debe ser 'M' o 'F'")]
    public string Genero { get; set; } = string.Empty;

    [Display(Name = "Edad")]
    [Range(0, 150, ErrorMessage = "La edad debe estar entre 0 y 150")]
    public int? Edad { get; set; }
}

/// <summary>Objeto de transferencia para profesion.</summary>
public class ProfesionDto
{
    [Display(Name = "Id")]
    [Required(ErrorMessage = "El id es obligatorio")]
    [Range(1, 999999, ErrorMessage = "El id debe estar entre 1 y 999999")]
    public int Id { get; set; }

    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(90)]
    public string Nom { get; set; } = string.Empty;

    [Display(Name = "Descripción")]
    public string? Des { get; set; }
}

/// <summary>Objeto de transferencia para estudios (llave compuesta id_prof + cc_per).</summary>
public class EstudioDto
{
    [Display(Name = "Profesión")]
    [Required(ErrorMessage = "La profesión es obligatoria")]
    [Range(1, int.MaxValue, ErrorMessage = "Seleccione una profesión")]
    public int IdProf { get; set; }

    [Display(Name = "Persona")]
    [Required(ErrorMessage = "La persona es obligatoria")]
    [Range(1, int.MaxValue, ErrorMessage = "Seleccione una persona")]
    public int CcPer { get; set; }

    [Display(Name = "Fecha de grado")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? Fecha { get; set; }

    [Display(Name = "Universidad")]
    [StringLength(50)]
    public string? Univer { get; set; }

    /// <summary>Solo lectura (se ignora al crear/actualizar).</summary>
    [Display(Name = "Nombre persona")]
    public string? NombrePersona { get; set; }

    /// <summary>Solo lectura (se ignora al crear/actualizar).</summary>
    [Display(Name = "Nombre profesión")]
    public string? NombreProfesion { get; set; }
}

/// <summary>Objeto de transferencia para telefono.</summary>
public class TelefonoDto
{
    [Display(Name = "Número")]
    [Required(ErrorMessage = "El número es obligatorio")]
    [StringLength(15)]
    public string Num { get; set; } = string.Empty;

    [Display(Name = "Operador")]
    [Required(ErrorMessage = "El operador es obligatorio")]
    [StringLength(45)]
    public string Oper { get; set; } = string.Empty;

    [Display(Name = "Dueño (cédula)")]
    [Required(ErrorMessage = "El dueño es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "Seleccione el dueño")]
    public int Duenio { get; set; }

    /// <summary>Solo lectura (se ignora al crear/actualizar).</summary>
    [Display(Name = "Nombre dueño")]
    public string? NombreDuenio { get; set; }
}
