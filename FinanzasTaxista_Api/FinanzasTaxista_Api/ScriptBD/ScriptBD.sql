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

-- Tabla de días de trabajo
CREATE TABLE dia_trabajo (
  id INT IDENTITY(1,1) PRIMARY KEY,
  fecha DATE NOT NULL UNIQUE
);
GO

-- Tabla de categorías
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

-- Inserción a la tabla rol.
INSERT INTO rol (nombre_rol)
VALUES 
  ('Administrador'),
  ('Taxista');

-- Inserción a la tabla usuario.
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
  'Pérez', -- apellido1
  'González', -- apellido2
  'juan.perez@example.com', -- correo_electronico
  '1234Segura!' -- contrasena 
);