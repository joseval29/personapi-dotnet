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
| 40 | 40_git_log_status.png | 40 | `git log --oneline` y `git status` |
| 41 | 41_navegador_inicio.png | 36 | Página de inicio en Safari (http://localhost:5080) |
| 42 | 42_navegador_personas.png | 36 | Listado de personas en Safari |
| 43 | 43_navegador_profesiones.png | 36 | Listado de profesiones en Safari |
| 44 | 44_navegador_estudios.png | 36 | Listado de estudios en Safari |
| 45 | 45_navegador_telefonos.png | 36 | Listado de teléfonos en Safari |
| 46 | 46_swagger_ui.png | 36 | Swagger UI en /swagger |
| 47 | 47_persona_crear_errores.png | 36 | Formulario de nueva persona con errores de validación (enviado vacío) |
| 48 | 48_persona_crear_formulario.png | 36 | Formulario de nueva persona lleno |
| 49 | 49_persona_listado_tras_crear.png | 36 | Listado de personas con el registro nuevo |
| 50 | 50_persona_editar_formulario.png | 36 | Formulario de editar persona |
| 51 | 51_persona_listado_tras_editar.png | 36 | Listado de personas tras editar |
| 52 | 52_persona_confirmar_eliminar.png | 36 | Confirmación de eliminar persona |
| 53 | 53_persona_listado_tras_eliminar.png | 36 | Listado de personas tras eliminar |
| 54 | 54_profesion_crear_errores.png | 36 | Formulario de nueva profesión con errores de validación |
| 55 | 55_profesion_crear_formulario.png | 36 | Formulario de nueva profesión lleno |
| 56 | 56_profesion_listado_tras_crear.png | 36 | Listado de profesiones tras crear |
| 57 | 57_profesion_editar_formulario.png | 36 | Formulario de editar profesión |
| 58 | 58_profesion_listado_tras_editar.png | 36 | Listado de profesiones tras editar |
| 59 | 59_profesion_confirmar_eliminar.png | 36 | Confirmación de eliminar profesión |
| 60 | 60_profesion_listado_tras_eliminar.png | 36 | Listado de profesiones tras eliminar |
| 61 | 61_telefono_crear_errores.png | 36 | Formulario de nuevo teléfono con errores de validación |
| 62 | 62_telefono_crear_formulario.png | 36 | Formulario de nuevo teléfono lleno |
| 63 | 63_telefono_listado_tras_crear.png | 36 | Listado de teléfonos tras crear |
| 64 | 64_telefono_editar_formulario.png | 36 | Formulario de editar teléfono |
| 65 | 65_telefono_listado_tras_editar.png | 36 | Listado de teléfonos tras editar |
| 66 | 66_telefono_confirmar_eliminar.png | 36 | Confirmación de eliminar teléfono |
| 67 | 67_telefono_listado_tras_eliminar.png | 36 | Listado de teléfonos tras eliminar |
| 68 | 68_estudio_crear_errores.png | 36 | Formulario de nuevo estudio con errores de validación |
| 69 | 69_estudio_crear_formulario.png | 36 | Formulario de nuevo estudio lleno |
| 70 | 70_estudio_listado_tras_crear.png | 36 | Listado de estudios tras crear |
| 71 | 71_estudio_editar_formulario.png | 36 | Formulario de editar estudio |
| 72 | 72_estudio_listado_tras_editar.png | 36 | Listado de estudios tras editar |
| 73 | 73_estudio_confirmar_eliminar.png | 36 | Confirmación de eliminar estudio |
| 74 | 74_estudio_listado_tras_eliminar.png | 36 | Listado de estudios tras eliminar |
| 75 | 75_swagger_personas_get.png | 36 | Swagger: GET /api/personas con 200 |
| 76 | 76_swagger_personas_post.png | 36 | Swagger: POST /api/personas (201) |
| 77 | 77_swagger_personas_get_id.png | 36 | Swagger: GET /api/personas/{cc} (200) |
| 78 | 78_swagger_personas_put.png | 36 | Swagger: PUT /api/personas/{cc} (204) |
| 79 | 79_swagger_personas_delete.png | 36 | Swagger: DELETE /api/personas/{cc} (204) |
| 80 | 80_swagger_personas_get_id_404.png | 36 | Swagger: GET /api/personas/{cc} tras eliminar (404) |
| 81 | 81_swagger_profesiones_get.png | 36 | Swagger: GET /api/profesiones (200) |
| 82 | 82_swagger_profesiones_post.png | 36 | Swagger: POST /api/profesiones (201) |
| 83 | 83_swagger_profesiones_get_id.png | 36 | Swagger: GET /api/profesiones/{id} (200) |
| 84 | 84_swagger_profesiones_put.png | 36 | Swagger: PUT /api/profesiones/{id} (204) |
| 85 | 85_swagger_profesiones_delete.png | 36 | Swagger: DELETE /api/profesiones/{id} (204) |
| 86 | 86_swagger_profesiones_get_id_404.png | 36 | Swagger: GET /api/profesiones/{id} tras eliminar (404) |
| 87 | 87_swagger_telefonos_get.png | 36 | Swagger: GET /api/telefonos (200) |
| 88 | 88_swagger_telefonos_post.png | 36 | Swagger: POST /api/telefonos (201) |
| 89 | 89_swagger_telefonos_get_num.png | 36 | Swagger: GET /api/telefonos/{num} (200) |
| 90 | 90_swagger_telefonos_put.png | 36 | Swagger: PUT /api/telefonos/{num} (204) |
| 91 | 91_swagger_telefonos_delete.png | 36 | Swagger: DELETE /api/telefonos/{num} (204) |
| 92 | 92_swagger_telefonos_get_num_404.png | 36 | Swagger: GET /api/telefonos/{num} tras eliminar (404) |
| 93 | 93_swagger_estudios_get.png | 36 | Swagger: GET /api/estudios (200) |
| 94 | 94_swagger_estudios_post.png | 36 | Swagger: POST /api/estudios (201) |
| 95 | 95_swagger_estudios_get_id.png | 36 | Swagger: GET /api/estudios/{idProf}/{ccPer} (200) |
| 96 | 96_swagger_estudios_put.png | 36 | Swagger: PUT /api/estudios/{idProf}/{ccPer} (204) |
| 97 | 97_swagger_estudios_delete.png | 36 | Swagger: DELETE /api/estudios/{idProf}/{ccPer} (204) |
| 98 | 98_swagger_estudios_get_id_404.png | 36 | Swagger: GET /api/estudios/{idProf}/{ccPer} tras eliminar (404) |
