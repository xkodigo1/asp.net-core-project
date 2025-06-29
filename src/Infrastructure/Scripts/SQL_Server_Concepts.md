# Conceptos Básicos de SQL Server

Este documento explica los conceptos fundamentales de SQL Server utilizados en nuestro proyecto AutoTallerManager.

## 1. Esquema [dbo]

### ¿Qué es?
- `[dbo]` significa "database owner" (propietario de la base de datos)
- Es el esquema predeterminado en SQL Server
- Funciona como un contenedor o namespace para objetos de la base de datos

### Beneficios
- **Organización**: Permite agrupar objetos relacionados
- **Seguridad**: Facilita la gestión de permisos
- **Prevención de conflictos**: Permite tener objetos con el mismo nombre en diferentes esquemas

### Ejemplos
```sql
-- Formas de referirse a una tabla
SELECT * FROM Users;            -- Implícitamente usa [dbo]
SELECT * FROM [dbo].Users;     -- Explícitamente usa [dbo]

-- Crear tabla en esquema específico
CREATE TABLE [dbo].[Users] (    -- Esquema explícito
    Id INT,
    Name NVARCHAR(100)
);
```

## 2. IDENTITY(1,1)

### ¿Qué es?
- Equivalente a AUTO_INCREMENT en MySQL
- Genera automáticamente valores únicos para columnas
- Formato: IDENTITY(valor_inicial, incremento)

### Características
- **Valor inicial**: Primer número que se asignará
- **Incremento**: Cantidad que se suma para el siguiente valor
- No necesita intervención manual
- Garantiza valores únicos

### Ejemplos
```sql
-- ID que comienza en 1 y aumenta de uno en uno
[Id] INT IDENTITY(1,1)         -- Generará: 1, 2, 3, 4...

-- ID que comienza en 100 y aumenta de 2 en 2
[Id] INT IDENTITY(100,2)       -- Generará: 100, 102, 104...

-- Uso típico en una tabla
CREATE TABLE [dbo].[Products] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(100)
);
```

## 3. GO

### ¿Qué es?
- Separador de lotes (batch separator)
- No es un comando SQL, es una instrucción para el motor
- Indica el fin de un lote de instrucciones

### Usos
- Separar instrucciones que deben ejecutarse independientemente
- Asegurar el orden de ejecución
- Necesario cuando ciertas instrucciones deben completarse antes que otras

### Ejemplos
```sql
-- Crear y usar una base de datos
CREATE DATABASE AutoTaller;
GO                              -- Ejecuta la creación primero
USE AutoTaller;                 -- Luego cambia a la nueva DB
GO

-- Crear objetos en la base de datos
CREATE TABLE Customers(...);
GO
CREATE TABLE Orders(...);
GO
```

## 4. NVARCHAR vs VARCHAR

### ¿Qué es?
- **VARCHAR**: Almacena caracteres ASCII (1 byte por carácter)
- **NVARCHAR**: Almacena caracteres Unicode (2 bytes por carácter)

### Comparación
| Característica   | VARCHAR         | NVARCHAR        |
|-----------------|-----------------|-----------------|
| Bytes/carácter  | 1              | 2               |
| Soporte Unicode | No             | Sí              |
| Uso de espacio  | Menor          | Mayor           |
| Internacionalización | Limitada   | Completa        |

### Cuándo usar cada uno
- **VARCHAR**:
  - Solo texto en inglés
  - Datos que no necesitan caracteres especiales
  - Cuando el espacio es crítico

- **NVARCHAR**:
  - Nombres y direcciones
  - Texto en múltiples idiomas
  - Cuando se necesitan emojis o caracteres especiales

### Ejemplos
```sql
-- Columnas con VARCHAR
[Code] VARCHAR(10)          -- Para códigos simples: "ABC123"
[Zip] VARCHAR(5)           -- Para códigos postales: "12345"

-- Columnas con NVARCHAR
[Name] NVARCHAR(100)       -- Para nombres: "José Martínez"
[Address] NVARCHAR(200)    -- Para direcciones: "Calle Mayor 123, 2º"
[Description] NVARCHAR(MAX) -- Para texto largo sin límite
```

## Uso en Nuestro Proyecto

En AutoTallerManager usamos:

1. **[dbo] para todas las tablas**:
   - Mantiene la organización simple
   - Facilita las consultas y el mantenimiento

2. **IDENTITY(1,1) para IDs**:
   - Asegura IDs únicos y secuenciales
   - Simplifica la creación de registros

3. **GO entre creaciones de tablas**:
   - Asegura la creación ordenada de objetos
   - Facilita la detección de errores

4. **NVARCHAR para texto**:
   - Soporta caracteres especiales del español
   - Permite almacenar cualquier tipo de texto

### Ejemplo de Nuestra Implementación
```sql
-- Ejemplo de una tabla de nuestro sistema
CREATE TABLE [dbo].[Customers] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [FirstName] NVARCHAR(50) NOT NULL,
    [LastName] NVARCHAR(50) NOT NULL,
    [Email] NVARCHAR(100) UNIQUE NOT NULL,
    [Phone] NVARCHAR(20),
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    [CreatedBy] NVARCHAR(100) NOT NULL
);
GO
``` 