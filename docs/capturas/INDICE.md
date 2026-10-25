# Índice de capturas — Laboratorio 1 (personapi-dotnet)

| Número | Archivo | Paso del procedimiento | Qué muestra |
|---|---|---|---|
| 01 | 01_chip_mac.png | 1 | Versión de macOS y arquitectura arm64 en Terminal |
| 02 | 02_homebrew_git.png | 2, 3 | `brew --version` (Homebrew 7.0.10) y `git --version` |
| 03 | 03_docker_version.png | 4 | `docker version` con Docker Desktop activo |
| 04 | 04_dotnet_sdks.png | 5 | `dotnet --list-sdks` mostrando 7.0.410 |
| 05 | 05_dotnet_ef.png | 6 | `dotnet ef --version` (7.0.20) |
| 06 | 06_sqlcmd_version.png | 7 | `sqlcmd --version` |
| 07 | 07_sql_server_version.png | 12 | `SELECT @@VERSION`: SQL Server 2022 Express Edition |
| 08 | 08_docker_ps_ready.png | 10, 11 | `docker ps` y línea "SQL Server is now ready for client connections" |
| 09 | 09_docker_compose_yml.png | 9 | Contenido de docker-compose.yml |
| 10 | 10_vscode_explorer_proyecto.png | 19 | Explorador de VS Code con la estructura del proyecto |
| 11 | 11_vscode_persona_dbcontext.png | 23 | PersonaDbContext.cs generado por Scaffold |
| 12 | 12_vscode_persona_cs.png | 23 | Persona.cs generado por Scaffold |
| 13 | 13_vscode_appsettings.png | 24 | appsettings.json con la cadena de conexión PersonaDb |
| 14 | 14_vscode_dtos.png | 25 | Dtos.cs (DTOs) |
| 15 | 15_vscode_ipersona_repository.png | 26 | IPersonaRepository.cs (interfaz DAO) |
| 16 | 16_vscode_persona_repository.png | 27 | PersonaRepository.cs (repositorio) |
| 17 | 17_vscode_personas_api_controller.png | 28 | PersonasApiController.cs (API REST) |
| 18 | 18_vscode_personas_controller.png | 29 | PersonasController.cs (MVC) |
| 19 | 19_vscode_program_cs.png | 31 | Program.cs (DbContext, repositorios y Swagger) |
| 20 | 20_vscode_csproj.png | 21 | personapi-dotnet.csproj con los paquetes NuGet |
| 21 | 21_dotnet_list_package.png | 21 | `dotnet list package` |
| 23 | 23_scaffold_ef_salida.png | 23 | Salida de `dotnet ef dbcontext scaffold` |
| 34 | 34_dotnet_build.png | 34 | `dotnet build` sin errores |
| 35 | 35_dotnet_run_listening.png | 35 | `dotnet run` escuchando en http://localhost:5080 |
| 37 | 37_curl_api_pruebas.png | 37 | GET de las 4 entidades, POST y DELETE con curl |
| 38 | 38_dml_restaurado.png | 38 | Restauración de datos con 02_dml.sql |
| 39 | 39_dotnet_publish.png | 39 | `dotnet publish -c Release -o ./publish` |
| 39b | 39b_publish_ejecucion.png | 39 | App publicada respondiendo en el puerto 5080 |
