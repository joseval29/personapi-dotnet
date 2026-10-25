# Laboratorio 1 — Monolito ASP.NET Core MVC con DAO (macOS, Apple Silicon)

Procedimiento seguido desde cero en un Mac con chip Apple Silicon. Cada paso lleva su captura en `capturas/`, numerada según `capturas/INDICE.md`.

## Stack

| Componente | Versión |
|---|---|
| macOS | 27.0.1 (arm64) |
| .NET SDK | 7.0.410 |
| Entity Framework Core | 7.0.20 (Database First) |
| SQL Server | 2022 Express (imagen Docker `mssql/server:2022-latest`) |
| Swagger | Swashbuckle 6.5.0 |
| Editor | Visual Studio Code (C# Dev Kit, SQL Server (mssql)) |

## 1. Ambiente

1. Información del sistema: `sw_vers` y `uname -m` (arm64). ![](capturas/01_chip_mac.png)
2. Homebrew y Git: `brew --version` (7.0.10) y `git --version` (2.54.0). ![](capturas/02_homebrew_git.png)
3. Docker Desktop: `docker version`. ![](capturas/03_docker_version.png)
4. Docker Desktop con Rosetta: en Settings › General › Virtual Machine Options se activó *Use Rosetta for x86_64/amd64 emulation on Apple Silicon* y se aplicó con Apply. ![](capturas/99_docker_rosetta_activado.png)

5. SDK de .NET 7: se instaló el `.pkg` oficial de 7.0.410 para arm64. `dotnet --list-sdks`. ![](capturas/04_dotnet_sdks.png)
6. Herramienta EF Core: `dotnet tool install --global dotnet-ef --version 7.0.20`. ![](capturas/05_dotnet_ef.png)
7. sqlcmd: `brew install sqlcmd`. ![](capturas/06_sqlcmd_version.png)
8. VS Code con las extensiones C# Dev Kit y SQL Server (mssql) instaladas. ![](capturas/10_vscode_explorer_proyecto.png)

## 2. Base de datos

8. `docker-compose.yml` con la imagen de SQL Server 2022 y `MSSQL_PID=Express`. ![](capturas/09_docker_compose_yml.png)
9. `docker compose up -d` y `docker ps`; el log muestra "SQL Server is now ready for client connections". ![](capturas/08_docker_ps_ready.png)
10. `SELECT @@VERSION` confirma **SQL Server 2022 Express Edition**. ![](capturas/07_sql_server_version.png)
11. Scripts `01_ddl.sql` (crea `persona_db` y las cuatro tablas) y `02_dml.sql` (datos de prueba). Al final se volvió a ejecutar `02_dml.sql` para restaurar los datos iniciales. ![](capturas/38_dml_restaurado.png)

## 3. Creación del proyecto

12. `git init`, `dotnet new sln` y `dotnet new mvc --framework net7.0 --no-https --auth None`. Se agregó el proyecto a la solución y se copió el `.gitignore`. ![](capturas/40_git_log_status.png)
13. Paquetes NuGet: `Microsoft.EntityFrameworkCore` 7.0.20, `.SqlServer` 7.0.20, `.Tools` 7.0.20, `.Design` 7.0.20 y `Swashbuckle.AspNetCore` 6.5.0. ![](capturas/21_dotnet_list_package.png) ![](capturas/20_vscode_csproj.png)

## 4. Entidades (Database First)

14. `dotnet ef dbcontext scaffold` generó `Persona`, `Profesion`, `Estudio`, `Telefono` y `PersonaDbContext` en `Models/Entities`. ![](capturas/23_scaffold_ef_salida.png) ![](capturas/11_vscode_persona_dbcontext.png) ![](capturas/12_vscode_persona_cs.png)
15. Cadena de conexión `PersonaDb` en `appsettings.json`. ![](capturas/13_vscode_appsettings.png)

**Ajustes:** las entidades generadas coinciden con las de la referencia (mismos nombres de propiedades y tipos), así que no fue necesario cambiar DTOs, repositorios ni controladores. Solo difieren comentarios y el BOM del archivo.

## 5. Código MVC + DAO

16. DTOs. ![](capturas/14_vscode_dtos.png)
17. Interfaces de repositorio (`IPersonaRepository`). ![](capturas/15_vscode_ipersona_repository.png)
18. Repositorios (`PersonaRepository`). ![](capturas/16_vscode_persona_repository.png)
19. Controladores REST (`PersonasApiController`). ![](capturas/17_vscode_personas_api_controller.png)
20. Controladores MVC (`PersonasController`). ![](capturas/18_vscode_personas_controller.png)
21. `Program.cs` con el registro del DbContext, los repositorios y Swagger. ![](capturas/19_vscode_program_cs.png)

## 6. Compilación y ejecución

22. `dotnet build`: 0 errores, 0 advertencias. ![](capturas/34_dotnet_build.png)
23. `dotnet run`: la app escucha en `http://localhost:5080`. ![](capturas/35_dotnet_run_listening.png)

## 7. Pruebas en navegador

24. Página de inicio y listados de cada entidad. ![](capturas/41_navegador_inicio.png) ![](capturas/42_navegador_personas.png) ![](capturas/43_navegador_profesiones.png) ![](capturas/44_navegador_estudios.png) ![](capturas/45_navegador_telefonos.png)
25. Formularios de crear, editar y eliminar para **personas**. Errores de validación con el formulario vacío, formulario lleno, listado tras crear, editar, confirmar eliminar y listado tras eliminar. ![](capturas/47_persona_crear_errores.png) ![](capturas/48_persona_crear_formulario.png) ![](capturas/49_persona_listado_tras_crear.png) ![](capturas/50_persona_editar_formulario.png) ![](capturas/51_persona_listado_tras_editar.png) ![](capturas/52_persona_confirmar_eliminar.png) ![](capturas/53_persona_listado_tras_eliminar.png)
26. Lo mismo para **profesiones**. ![](capturas/54_profesion_crear_errores.png) ![](capturas/55_profesion_crear_formulario.png) ![](capturas/56_profesion_listado_tras_crear.png) ![](capturas/57_profesion_editar_formulario.png) ![](capturas/58_profesion_listado_tras_editar.png) ![](capturas/59_profesion_confirmar_eliminar.png) ![](capturas/60_profesion_listado_tras_eliminar.png)
27. Lo mismo para **teléfonos**. ![](capturas/61_telefono_crear_errores.png) ![](capturas/62_telefono_crear_formulario.png) ![](capturas/63_telefono_listado_tras_crear.png) ![](capturas/64_telefono_editar_formulario.png) ![](capturas/65_telefono_listado_tras_editar.png) ![](capturas/66_telefono_confirmar_eliminar.png) ![](capturas/67_telefono_listado_tras_eliminar.png)
28. Lo mismo para **estudios**. ![](capturas/68_estudio_crear_errores.png) ![](capturas/69_estudio_crear_formulario.png) ![](capturas/70_estudio_listado_tras_crear.png) ![](capturas/71_estudio_editar_formulario.png) ![](capturas/72_estudio_listado_tras_editar.png) ![](capturas/73_estudio_confirmar_eliminar.png) ![](capturas/74_estudio_listado_tras_eliminar.png)

## 8. Swagger

29. Interfaz general. ![](capturas/46_swagger_ui.png)
30. Por entidad se probaron GET de lista, POST (201), GET por id (200), PUT (204), DELETE (204) y GET tras eliminar (404).
    - Personas: ![](capturas/75_swagger_personas_get.png) ![](capturas/76_swagger_personas_post.png) ![](capturas/77_swagger_personas_get_id.png) ![](capturas/78_swagger_personas_put.png) ![](capturas/79_swagger_personas_delete.png) ![](capturas/80_swagger_personas_get_id_404.png)
    - Profesiones: ![](capturas/81_swagger_profesiones_get.png) ![](capturas/82_swagger_profesiones_post.png) ![](capturas/83_swagger_profesiones_get_id.png) ![](capturas/84_swagger_profesiones_put.png) ![](capturas/85_swagger_profesiones_delete.png) ![](capturas/86_swagger_profesiones_get_id_404.png)
    - Teléfonos: ![](capturas/87_swagger_telefonos_get.png) ![](capturas/88_swagger_telefonos_post.png) ![](capturas/89_swagger_telefonos_get_num.png) ![](capturas/90_swagger_telefonos_put.png) ![](capturas/91_swagger_telefonos_delete.png) ![](capturas/92_swagger_telefonos_get_num_404.png)
    - Estudios: ![](capturas/93_swagger_estudios_get.png) ![](capturas/94_swagger_estudios_post.png) ![](capturas/95_swagger_estudios_get_id.png) ![](capturas/96_swagger_estudios_put.png) ![](capturas/97_swagger_estudios_delete.png) ![](capturas/98_swagger_estudios_get_id_404.png)

## 9. Pruebas con curl

31. GET de las cuatro entidades, POST y DELETE. ![](capturas/37_curl_api_pruebas.png)

## 10. Publicación

32. `dotnet publish -c Release -o ./publish`. La carpeta `publish/` está en `.gitignore`. ![](capturas/39_dotnet_publish.png)
33. Ejecución de la app publicada en el puerto 5080. ![](capturas/39b_publish_ejecucion.png)

## 11. Cierre

34. Commits locales: `96a7130` (monolito MVC + DAO), `abf9134`, `660c847` y `6b0e5f4` (capturas).

## Pendiente

- **Publicación en GitHub** (repo público, push a `main`, tag `v1.0.0`): pendiente de autorización.
