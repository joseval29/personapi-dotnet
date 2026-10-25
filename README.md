# personapi-dotnet

Laboratorio 1 – Arquitectura de Software
**Monolito con patrón MVC y DAO** para el CRUD del modelo `persona – profesion – estudios – telefono`.

| Componente | Versión |
|---|---|
| .NET / ASP.NET Core MVC | 7.0 |
| Entity Framework Core (SqlServer, Tools) | 7.0.20 |
| MS SQL Server | 2022 Express (Docker) |
| REST + Swagger (OpenAPI 3) | Swashbuckle.AspNetCore 6.5.0 |
| IDE | VS Code + C# Dev Kit (macOS) |

---

## 1. Arquitectura

```
Navegador (Vistas Razor)  ─┐                ┌─ Swagger UI / cliente REST
                           ▼                ▼
                 Controllers (MVC)   Controllers/Api (REST, JSON)
                           └───────┬────────┘
                                   ▼
                        Interfaces (I*Repository)      <- contrato DAO
                                   ▼
                        Repositories (*Repository)     <- implementación DAO
                                   ▼
                  Models/Entities + PersonaDbContext   <- EF Core (Database First)
                                   ▼
                       SQL Server  ->  persona_db
```

```
personapi-dotnet/
├── personapi-dotnet.sln
├── docker-compose.yml        # SQL Server 2022 Express
├── scripts/
│   ├── 01_ddl.sql            # crea persona_db, dueño sa, tablas
│   └── 02_dml.sql            # datos de prueba
└── personapi-dotnet/
    ├── Program.cs            # DI, EF Core, Swagger, rutas
    ├── appsettings.json      # cadena de conexión "PersonaDb"
    ├── Models/
    │   ├── Entities/         # Persona, Profesion, Estudio, Telefono, PersonaDbContext (Scaffold-DbContext)
    │   └── Dtos/             # DTOs con validaciones + Mappers
    ├── Interfaces/           # IPersonaRepository, IProfesionRepository, IEstudioRepository, ITelefonoRepository
    ├── Repositories/         # Implementaciones DAO con EF Core
    ├── Controllers/          # MVC: Home, Personas, Profesiones, Estudios, Telefonos
    │   └── Api/              # REST: /api/personas, /api/profesiones, /api/estudios, /api/telefonos
    ├── Views/                # Vistas Razor (Index, Details, Create, Edit, Delete)
    └── personapi-dotnet.http # Peticiones de prueba para la API
```

## 2. Endpoints REST

| Recurso | GET (todos) | GET (uno) | POST | PUT | DELETE |
|---|---|---|---|---|---|
| Persona | `/api/personas` | `/api/personas/{cc}` | `/api/personas` | `/api/personas/{cc}` | `/api/personas/{cc}` |
| Profesión | `/api/profesiones` | `/api/profesiones/{id}` | `/api/profesiones` | `/api/profesiones/{id}` | `/api/profesiones/{id}` |
| Estudio | `/api/estudios` | `/api/estudios/{idProf}/{ccPer}` | `/api/estudios` | `/api/estudios/{idProf}/{ccPer}` | `/api/estudios/{idProf}/{ccPer}` |
| Teléfono | `/api/telefonos` | `/api/telefonos/{num}` | `/api/telefonos` | `/api/telefonos/{num}` | `/api/telefonos/{num}` |

Documentación interactiva: **http://localhost:5080/swagger**

Vistas MVC: `http://localhost:5080/Personas`, `/Profesiones`, `/Estudios`, `/Telefonos`.

> Al eliminar una persona se eliminan también sus estudios y teléfonos; al eliminar una profesión se eliminan sus estudios (las FK no tienen `ON DELETE CASCADE`, lo maneja el repositorio).

## 3. Configuración del ambiente (macOS)

> SQL Server, SSMS y Visual Studio Community no existen para macOS, así que se usan sus equivalentes:
> SQL Server 2022 Express en **Docker**, **VS Code** (extensiones *C# Dev Kit* y *SQL Server (mssql)*) en lugar de Visual Studio/SSMS, y la CLI `dotnet ef` en lugar de la Consola del Administrador de paquetes.

1. **Homebrew** (https://brew.sh).
2. **Docker Desktop** (https://www.docker.com/products/docker-desktop/). En Mac con Apple Silicon (M1–M4) activar *Settings › General › Use Rosetta for x86_64/amd64 emulation on Apple Silicon*.
3. **SDK de .NET 7**: instalador para macOS (Arm64 o x64) en https://dotnet.microsoft.com/download/dotnet/7.0. Verificar con `dotnet --list-sdks`.
4. **Herramienta de EF Core**: `dotnet tool install --global dotnet-ef --version 7.0.20`
5. **sqlcmd**: `brew install sqlcmd`
6. **VS Code** con las extensiones *C# Dev Kit* y *SQL Server (mssql)*.
7. **Git** (`git --version`; si no está, `xcode-select --install`).

## 4. Base de datos

Levantar SQL Server 2022 Express (usuario `sa`, contraseña `PersonaApi2026!`):

```bash
docker compose up -d
docker logs -f personapi-sqlserver    # esperar "SQL Server is now ready for client connections"
```

Ejecutar los scripts en orden (comillas simples por el `!`):

```bash
sqlcmd -S localhost,1433 -U sa -P 'PersonaApi2026!' -C -i scripts/01_ddl.sql
sqlcmd -S localhost,1433 -U sa -P 'PersonaApi2026!' -C -i scripts/02_dml.sql
```

`01_ddl.sql` crea `persona_db`, asigna como dueño a `sa` y crea las tablas; `02_dml.sql` inserta datos de prueba.
Para ver la base gráficamente: en VS Code, extensión *SQL Server (mssql)* › nueva conexión `localhost,1433`, autenticación SQL, usuario `sa`.

Cadena de conexión (`personapi-dotnet/appsettings.json`):

```json
"ConnectionStrings": {
  "PersonaDb": "Server=localhost,1433;Database=persona_db;User Id=sa;Password=PersonaApi2026!;TrustServerCertificate=true"
}
```

> En Windows con SQL Server Express instalado nativamente se puede usar
> `Server=localhost\SQLEXPRESS;Database=persona_db;Trusted_Connection=True;TrustServerCertificate=true`.

### Regenerar las entidades (Database First)

Las entidades de `Models/Entities` se generaron por ingeniería inversa de la base de datos (equivalente a `Scaffold-DbContext`):

```bash
cd personapi-dotnet
dotnet ef dbcontext scaffold "Server=localhost,1433;Database=persona_db;User Id=sa;Password=PersonaApi2026!;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models/Entities --context PersonaDbContext --no-onconfiguring --force
```

## 5. Compilación y ejecución

```bash
cd personapi-dotnet
dotnet restore
dotnet build
dotnet run
```

La aplicación queda en **http://localhost:5080** (Swagger en `/swagger`). En VS Code también se puede ejecutar con *Run › Start Debugging* (C# Dev Kit).

## 6. Despliegue

```bash
cd personapi-dotnet
dotnet publish -c Release -o ./publish
cd publish
dotnet personapi-dotnet.dll --urls "http://0.0.0.0:5080"
```

El contenedor de SQL Server debe estar corriendo (`docker compose up -d`). Para detenerlo: `docker compose down` (los datos se conservan en el volumen `sqldata`).

## 7. Versionamiento

```bash
git add .
git commit -m "Laboratorio 1: monolito MVC + DAO"
git push origin main
git tag -a v1.0.0 -m "Entrega Laboratorio 1"
git push origin v1.0.0
```

## Autores

Jose Eduardo Valeriano Castiblanco (Joseval29), Jorge Andres Fortich Ordosgoitia (FortichOrdosgoitiaJorge)
