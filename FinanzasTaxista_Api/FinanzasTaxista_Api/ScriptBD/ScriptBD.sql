-- Crear la base de datos
CREATE DATABASE finanzas_taxista;
GO

USE finanzas_taxista;
GO

-- Tabla de roles
CREATE TABLE rol (
  id INT IDENTITY(1,1) PRIMARY KEY,
  nombre_rol VARCHAR(50) NOT NULL
);
GO

-- Tabla de usuarios
CREATE TABLE usuario (
  id INT IDENTITY(1,1) PRIMARY KEY,
  id_rol INT NOT NULL,
  nombre_usuario VARCHAR(50) NOT NULL UNIQUE,
  apellido1 VARCHAR(50) NOT NULL,
  apellido2 VARCHAR(50) NOT NULL,
  correo_electronico VARCHAR(100) NOT NULL UNIQUE,
  contrasena VARCHAR(100) NOT NULL,
  FOREIGN KEY (id_rol) REFERENCES rol(id)
);
GO


-- Tabla de d�as de trabajooooo
CREATE TABLE dia_trabajo (
  id INT IDENTITY(1,1) PRIMARY KEY,
  fecha DATE NOT NULL UNIQUE
);
GO

-- Actualizacion de la tabla /Jason Zu�iga/27/07/2025

-- 1.Se agrega la columna id_usuario para relacionar los d�as de trabajo con los usuarios.
ALTER TABLE dia_trabajo ADD id_usuario INT;

--	2.Luego se agrega la restricci�n de clave for�nea para relacionar la columna id_usuario con la tabla usuario.
UPDATE dia_trabajo SET id_usuario = 1;

-- 2.Aqui vamos a revisar si la tabla dia de trabajo ya tiene datos, si no tiene datos(ejemplo que me salio a mi: UQ__dia_trab__E1141322ACDB3B44), se puede agregar la restricci�n 
-- de clave for�nea(en caso que no les salga nada saltar al paso 6).
SELECT name FROM sys.indexes WHERE object_id = OBJECT_ID('dia_trabajo') AND is_unique = 1;

-- 3.Probablemente les saldra la restriccion que ya el sql crea el por si solo asi que aqui simplemente la quitamos
ALTER TABLE dia_trabajo
DROP CONSTRAINT UQ__dia_trab__E1141322ACDB3B44;

-- 4.Ahora si agregamos la restricci�n de clave for�nea para relacionar la columna id_usuario con la tabla usuario.
CREATE UNIQUE INDEX UX_dia_trabajo_usuario_fecha
ON dia_trabajo (id_usuario, fecha);


-- Tabla de categor�as
CREATE TABLE categoria (
  id INT IDENTITY(1,1) PRIMARY KEY,
  nombre_categoria VARCHAR(100) NOT NULL
);
GO

-- Tabla de viajes
CREATE TABLE viaje (
  id INT IDENTITY(1,1) PRIMARY KEY,
  id_usuario INT NOT NULL,
  id_dia INT NOT NULL,
  id_categoria INT NOT NULL,
  monto DECIMAL(10,2) NOT NULL CHECK (monto >= 0),
  ubicacion VARCHAR(200) NOT NULL,
  FOREIGN KEY (id_usuario) REFERENCES usuario(id),
  FOREIGN KEY (id_dia) REFERENCES dia_trabajo(id),
  FOREIGN KEY (id_categoria) REFERENCES categoria(id)
);
GO

-- Tabla de gastos
CREATE TABLE gasto (
  id INT IDENTITY(1,1) PRIMARY KEY,
  id_usuario INT NOT NULL,
  id_dia INT NOT NULL,
  id_categoria INT NOT NULL,
  monto DECIMAL(10,2) NOT NULL CHECK (monto >= 0),
  FOREIGN KEY (id_usuario) REFERENCES usuario(id),
  FOREIGN KEY (id_dia) REFERENCES dia_trabajo(id),
  FOREIGN KEY (id_categoria) REFERENCES categoria(id)
);
GO

-- Tabla de balance diario actualizada
CREATE TABLE balance_diario (
  id INT IDENTITY(1,1) PRIMARY KEY,
  id_usuario INT NOT NULL,
  id_dia INT NOT NULL,
  utilidad VARCHAR(200) NOT NULL ,
  FOREIGN KEY (id_usuario) REFERENCES usuario(id),
  FOREIGN KEY (id_dia) REFERENCES dia_trabajo(id),
  UNIQUE (id_usuario, id_dia)
);
GO

-- Inserciones

-- Inserci�n a la tabla rol.
INSERT INTO rol (nombre_rol)
VALUES 
  ('Administrador'),
  ('Taxista');

-- Inserci�n a la tabla usuario.
INSERT INTO usuario (
  id_rol,
  nombre_usuario,
  apellido1,
  apellido2,
  correo_electronico,
  contrasena
)
VALUES (
  1, -- id_rol
  'Juan', -- nombre_usuario
  'P�rez', -- apellido1
  'Gonz�lez', -- apellido2
  'juan.perez@example.com', -- correo_electronico
  '1234Segura!' -- contrasena 
);