-- Crear base de datos
CREATE DATABASE NecliDB;


USE NecliDB;


-- Tabla de Usuarios
CREATE TABLE Usuarios (
    Identificacion NVARCHAR(50) PRIMARY KEY,
    Tipo NVARCHAR(20) NOT NULL,
    Nombres NVARCHAR(100) NOT NULL,
    Apellidos NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    NumeroTelefono NVARCHAR(15) UNIQUE NOT NULL,
    ContraseñaHash NVARCHAR(255) NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);


-- Tabla de Cuentas
CREATE TABLE Cuentas (
    Numero INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId NVARCHAR(50) NOT NULL,
    Saldo DECIMAL(18,2) NOT NULL DEFAULT 0,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Identificacion)
);


-- Tabla de Transacciones
CREATE TABLE Transacciones (
    Numero INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    CuentaOrigenId INT NOT NULL,
    CuentaDestinoId INT NOT NULL,
    Monto DECIMAL(18,2) NOT NULL,
    Tipo NVARCHAR(20) NOT NULL, -- 'entrada' o 'salida'
    FOREIGN KEY (CuentaOrigenId) REFERENCES Cuentas(Numero),
    FOREIGN KEY (CuentaDestinoId) REFERENCES Cuentas(Numero)
);

USE NecliDB;
GO

-- Insertar  usuarios  (hash BCrypt)
INSERT INTO Usuarios (Identificacion, Tipo, Nombres, Apellidos, Email, NumeroTelefono, ContraseñaHash, FechaCreacion)
VALUES 
('user-001', 'cc', 'María', 'Morales', 'maria@example.com', '3001112222', '$2b$12$Urt02fTUuixZzhLFux2jbOFQTZxLPEO30r5bPEwtKV3OPFetfrTM6', GETDATE()),
('user-002', 'cc', 'Juan', 'Pérez', 'juan@example.com', '3003334444', '$2b$12$DDMtRK825Mz3vd8Lg9xGb.iNwt/dPOLYd0QYDuY3PDmq0d01/Xkd.', GETDATE()),
('user-003', 'cc', 'Camila', 'Gómez', 'camila@example.com', '3005556666', '$2b$12$52UhqLdqczKkb1w1VM/ADOe5XQ54FbHzei6rq7FKFcYUx6285oPCS', GETDATE()),
('user-004', 'cc', 'Pedro', 'Ramírez', 'pedro@example.com', '3007778888', '$2b$12$dpQl5W88MLAZO.a87S5vrOuYt3N94qNrAybdtPYzoU4Ft3kDhfPG.', GETDATE()),
('user-005', 'cc', 'Laura', 'Fernández', 'laura@example.com', '3009990000', '$2b$12$.QfOVJk6tdoLED6R3yaJGeHqC5uD8kxcRbafePIhPYGaM/AXwbjhm', GETDATE());

-- Insertar cuentas
INSERT INTO Cuentas (UsuarioId, Saldo, FechaCreacion)
VALUES
('user-001', 50000, GETDATE()),
('user-002', 75000, GETDATE()),
('user-003', 30000, GETDATE()),
('user-004', 100000, GETDATE()),
('user-005', 15000, GETDATE());

-- Insertar transacciones
INSERT INTO Transacciones (CuentaOrigenId, CuentaDestinoId, Monto, Fecha)
VALUES
(1, 2, 10000, GETDATE()),
(2, 3, 15000, GETDATE()),
(3, 4, 5000, GETDATE()),
(4, 5, 20000, GETDATE()),
(5, 1, 3000, GETDATE());
