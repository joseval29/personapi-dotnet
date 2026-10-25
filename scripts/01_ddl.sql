/* =========================================================
   personapi-dotnet - Script DDL (MS SQL Server 2022)
   Crea la base de datos persona_db, le asigna como dueño al
   usuario sa y crea las tablas del modelo:
   persona, profesion, estudios, telefono
   ========================================================= */

IF DB_ID(N'persona_db') IS NULL
    CREATE DATABASE persona_db;
GO

-- Propiedad de la base de datos para el usuario sa
ALTER AUTHORIZATION ON DATABASE::persona_db TO sa;
GO

USE persona_db;
GO

-- Borrado en orden inverso a las dependencias (permite re-ejecutar el script)
IF OBJECT_ID(N'dbo.telefono', N'U') IS NOT NULL DROP TABLE dbo.telefono;
IF OBJECT_ID(N'dbo.estudios', N'U') IS NOT NULL DROP TABLE dbo.estudios;
IF OBJECT_ID(N'dbo.profesion', N'U') IS NOT NULL DROP TABLE dbo.profesion;
IF OBJECT_ID(N'dbo.persona',  N'U') IS NOT NULL DROP TABLE dbo.persona;
GO

-- -----------------------------------------------------
-- Tabla persona
-- -----------------------------------------------------
CREATE TABLE dbo.persona (
    cc        INT          NOT NULL,
    nombre    VARCHAR(45)  NOT NULL,
    apellido  VARCHAR(45)  NOT NULL,
    genero    CHAR(1)      NOT NULL,
    edad      INT          NULL,
    CONSTRAINT PK_persona PRIMARY KEY (cc),
    -- Equivalente al ENUM('M','F') de MySQL
    CONSTRAINT CK_persona_genero CHECK (genero IN ('M', 'F'))
);
GO

-- -----------------------------------------------------
-- Tabla profesion
-- -----------------------------------------------------
CREATE TABLE dbo.profesion (
    id   INT           NOT NULL,
    nom  VARCHAR(90)   NOT NULL,
    des  VARCHAR(MAX)  NULL,      -- TEXT en MySQL (TEXT está obsoleto en SQL Server)
    CONSTRAINT PK_profesion PRIMARY KEY (id)
);
GO

-- -----------------------------------------------------
-- Tabla estudios (relación N:M persona - profesion)
-- -----------------------------------------------------
CREATE TABLE dbo.estudios (
    id_prof  INT          NOT NULL,
    cc_per   INT          NOT NULL,
    fecha    DATE         NULL,
    univer   VARCHAR(50)  NULL,
    CONSTRAINT PK_estudios PRIMARY KEY (id_prof, cc_per),
    CONSTRAINT estudio_persona_fk   FOREIGN KEY (cc_per)  REFERENCES dbo.persona (cc),
    CONSTRAINT estudio_profesion_fk FOREIGN KEY (id_prof) REFERENCES dbo.profesion (id)
);
GO
CREATE INDEX estudio_persona_fk ON dbo.estudios (cc_per);
GO

-- -----------------------------------------------------
-- Tabla telefono (1:N persona - telefono)
-- -----------------------------------------------------
CREATE TABLE dbo.telefono (
    num     VARCHAR(15)  NOT NULL,
    oper    VARCHAR(45)  NOT NULL,
    duenio  INT          NOT NULL,
    CONSTRAINT PK_telefono PRIMARY KEY (num),
    CONSTRAINT telefono_persona_fk FOREIGN KEY (duenio) REFERENCES dbo.persona (cc)
);
GO
CREATE INDEX telefono_persona_fk ON dbo.telefono (duenio);
GO
