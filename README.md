# Moodle External Database Integration Example

**Achtung**: das vorliegende Projekt dient einzig dem Anschauungszweck und **sollte auf keinen Fall ohne sorgfältige Aufarbeitung und Kontrolle produktiv eingesetzt werden!**

Moodle unterstützt mehrere Möglichkeiten, Daten aus einer externen Datenbank zu importieren (und teils zu verwalten). Zu diesem Zweck haben wir eine prototypische Schnittstelle geschrieben, welche folgende Funktionen besitzt:

- Abgleich von Daten aus einer externen Quelle, bspw. einem Campus Management System wie HISInOne
- Zwischenspeicherung und eventuelle Transformation dieser Daten, um Persistenz zwischen der externen Quelle und der Schnittstelle zu gewährleisten
- Migration dieser Daten in spezifisch auf die entsprechenden Moodle-Funktionen zugeschnittenen Tabellen

Um die Integrität der Datenbank zu stärken, besitzen alle Einträge entsprechende Foreign Keys zu ihren jeweiligen externen, wie auch migrierten Datensätzen.
Die Schnittstelle läuft automatisiert, d.h. es muss nur der Prozess gestartet sein; es ist kein CRON Job von Nöten.
Die Docker-Container erklären sich wie folgt:

- **net-db**: Postgres Latest, dient der .NET 6 Applikation als Datenbank
  - Vom Host zu erreichen unter localhost:6543
- **net-app**: Release-Container der .NET 6 Applikation
  - Dieser sollte nur für ein exemplarisches Deploy dienen und kann für die lokale Entwicklung aus der Compose-Datei entfernt werden.

## Projektstruktur

Analog zu den Containern finden sich in dem Projekt insgesamt vier Unterprojekte:

### Moodle

Stellvertretend für den Stack einer Moodle-Instanz. In der Readme finden sich Erklärungen zu der Konfiguration des External Database Enrolments in Moodle.

### Client

Die "Schnittstelle" in Form einer .NET Konsolenapplikation, welche exemplarisch die Beschaffung der externen Daten, sowie die Migration dieser in das Format der- und in die entsprechende Tabellen für das External-Database-Enrolment übernimmt.

### Core

Definition von Interfaces und Klassen, welche für die Projekte benötigt werden.

### Data

Konfiguration der Datenbank basierend auf [Entity Framework Core](https://docs.microsoft.com/en-us/ef/).

## Setup

**Achtung**: der vorliegende Code wurde nur unter macOS Monterey ^12.3 getestet; insbesondere bei der Konfiguration der Docker-Volumes kann es bei anderen Betriebssystemen zu Problemen kommen. Der .NET-Code ist hingegen vollständig crossplatform.

Dieses Projekt benötigt das [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0).  
Darüber hinaus müssen, um die Änderungen an der Datenbank vorzunehmen bzw. eine frische Datenbank zu seeden, die globalen [Entity Framework Core Tools](https://docs.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli#install-entity-framework-core) installiert sein.

### Docker Setup

Dieses Projekt nutzt eine PostgreSQL Datenbank (es kann prinzipiell aber auch jede andere Datenbank genutzt werden - dies Bedarf jedoch ggf. Anpassungen im Datenbank-Model). Um die Entwicklung Crossplatform möglichst simpel zu halten, wird [Docker](https://www.docker.com/) benötigt. Nach der Installation ist lediglich in dem Root-Verzeichnis der Solution folgender Befehl von der Kommandozeile auszuführen:

```
docker-componse up -d
```

### Initiales Data Seeding

**Achtung**: für diesen Schritt ist es zwingend erforderlich, dass sowohl das .NET 6 SDK sowie Entity Framework Core installiert sind!

Um die benötigte Datenbank zu erstellen und zu seeden ist folgender Befehl auf der Kommandozeile aus dem Root-Verzeichnis der Solution auszuführen:

```
dotnet ef --startup-project moodle-external-database-integration.Client database update
```
