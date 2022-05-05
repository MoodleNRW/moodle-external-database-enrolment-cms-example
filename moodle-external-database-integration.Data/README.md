# Moodle-External-Database-Integration - Database

Dieses Projekt beinhaltet die Konfiguration für das Datenbankmodel auf Basis des Code-First-Ansatzes. Die Modelle aus dem Core-Projekt werden durch die jeweiligen Configurations als Datenbank-Entitäten definiert und in `MoodleExternalDatabaseIntegrationDbContext.cs` dem jeweiligen Context hinzugefügt.

## Development

Um nach Änderungen an dem Datenbank-Model eine neue Datenbank-Migration zu erstellen, muss auf der Kommandozeile in das Root-Verzeichnis der Solution navigiert und dort der folgender Befehle ausgeführt werden:

```
dotnet ef --startup-project moodle-external-database-integration.Client migrations add MIGRATIONNAME --project moodle-external-database-integration.Data
```

Ausstehende Migrationen werden mit

```
dotnet ef --startup-project moodle-external-database-integration.Client database update
```

eingespielt.

## Deployment

Die Datenbank kann via eines .sql-Scripts migriert werden. Diese wird über die Kommandozeile mit folgendem Befehl generiert:

```
dotnet ef --startup-project moodle-external-database-integration.Client migrations script --idempotent -o migration.sql
```

Durch das Flag `-idempotent` werden dabei alle Migrationen in Betracht gezogen. Dies kann auch auf einzelne Migrationen bzw. eine Range von Migrationen beschränkt werden.
