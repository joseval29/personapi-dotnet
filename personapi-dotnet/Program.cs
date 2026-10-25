using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repositories;

var builder = WebApplication.CreateBuilder(args);

// MVC (controladores + vistas Razor) y API REST en el mismo monolito
builder.Services.AddControllersWithViews()
    .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Contexto de Entity Framework Core (cadena de conexión en appsettings.json)
builder.Services.AddDbContext<PersonaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PersonaDb")));

// Capa DAO: interfaces -> repositorios
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IProfesionRepository, ProfesionRepository>();
builder.Services.AddScoped<IEstudioRepository, EstudioRepository>();
builder.Services.AddScoped<ITelefonoRepository, TelefonoRepository>();

// Swagger / OpenAPI 3
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "personapi-dotnet",
        Version = "v1",
        Description = "API REST para el CRUD de persona, profesion, estudios y telefono (Laboratorio 1 - MVC + DAO)"
    });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

// Swagger habilitado en todos los ambientes (laboratorio)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "personapi-dotnet v1");
    c.DocumentTitle = "personapi-dotnet - Swagger";
});

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Endpoints REST (atributos [Route]) y rutas MVC convencionales
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
