/* =========================================================
   personapi-dotnet - Script DML (datos de prueba)
   Ejecutar después de 01_ddl.sql
   ========================================================= */
USE persona_db;
GO

DELETE FROM dbo.telefono;
DELETE FROM dbo.estudios;
DELETE FROM dbo.profesion;
DELETE FROM dbo.persona;
GO

INSERT INTO dbo.persona (cc, nombre, apellido, genero, edad) VALUES
(1010101010, 'Juan',    'Pérez',     'M', 22),
(1020202020, 'María',   'Gómez',     'F', 25),
(1030303030, 'Carlos',  'Rodríguez', 'M', 30),
(1040404040, 'Laura',   'Martínez',  'F', 21),
(1050505050, 'Andrés',  'López',     'M', NULL);
GO

INSERT INTO dbo.profesion (id, nom, des) VALUES
(1, 'Ingeniería de Sistemas',  'Diseño, desarrollo y gestión de sistemas de información y software.'),
(2, 'Ingeniería Industrial',   'Optimización de procesos productivos y logísticos.'),
(3, 'Medicina',                'Prevención, diagnóstico y tratamiento de enfermedades.'),
(4, 'Derecho',                 'Estudio y aplicación de las normas jurídicas.'),
(5, 'Administración de Empresas', NULL);
GO

INSERT INTO dbo.estudios (id_prof, cc_per, fecha, univer) VALUES
(1, 1010101010, '2024-06-15', 'Pontificia Universidad Javeriana'),
(2, 1010101010, '2025-12-10', 'Universidad Nacional de Colombia'),
(3, 1020202020, '2023-11-30', 'Universidad de los Andes'),
(1, 1030303030, '2018-07-20', 'Universidad del Rosario'),
(4, 1040404040, NULL,         'Universidad Externado de Colombia');
GO

INSERT INTO dbo.telefono (num, oper, duenio) VALUES
('3001234567', 'Claro',    1010101010),
('3109876543', 'Movistar', 1010101010),
('3204567890', 'Tigo',     1020202020),
('3157654321', 'Claro',    1030303030),
('6013456789', 'ETB',      1040404040);
GO

SELECT * FROM dbo.persona;
SELECT * FROM dbo.profesion;
SELECT * FROM dbo.estudios;
SELECT * FROM dbo.telefono;
GO
