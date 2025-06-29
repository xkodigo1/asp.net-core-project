# Database Scripts

Esta carpeta contiene los scripts de base de datos para el proyecto AutoTallerManager.

## Estructura

- `/SqlServer`: Scripts específicos para Microsoft SQL Server
  - `schema.sql`: Script de creación del esquema de la base de datos
  - `seed.sql`: Script de datos iniciales (cuando se genere)
  - `migrations/`: Scripts de migración generados por EF Core

## Convenciones de Nombrado

1. Los nombres de los scripts deben seguir el formato:
   - Para migraciones: `YYYYMMDDHHMMSS_MigrationName.sql`
   - Para otros scripts: `purpose_description.sql`

2. Todos los scripts deben:
   - Incluir un encabezado con fecha y propósito
   - Ser idempotentes cuando sea posible
   - Incluir manejo de errores apropiado

## Uso

1. Para crear la base de datos desde cero:
   ```sql
   -- Ejecutar en orden:
   1. schema.sql
   2. seed.sql (si existe)
   ```

2. Para actualizaciones:
   - Los scripts de migración se generarán automáticamente en la carpeta `migrations/`
   - Cada script de migración tendrá su correspondiente script de reversión

## Notas

- Los scripts se generan automáticamente a partir de las migraciones de Entity Framework Core
- Mantener este README actualizado con cualquier cambio en las convenciones o estructura 