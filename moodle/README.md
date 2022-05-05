# Moodle - Konfiguration External Database Authentication / Enrolment

## Zu den Begrifflichkeiten

Moodle unterscheidet bei der Anbindung einer externen Datenbank zwei unterschiedliche Dienste:

- **External Database Authentication**
  - Erstellung von Usern
- **External Database Enrolment**
  - Erstellung von Kursen, Enrolment von Usern in Kursen

## Aktivieren der External Database Authentication

Zunächst muss die Funktionalität zur externen Erstellung von Usern aktiviert werden. Dies geschieht unter `Dashboard / Site administration / Plugins / Authentication / External database`. Siehe dazu die [Dokumentation](https://docs.moodle.org/311/en/External_database_enrolment) für weitere Informationen.

### Konfiguration der External Database Authentication

Für das hier exemplarisch konfigurierten PDO-PostgreSQL-Docker Setup muss der Database-Driver auf

```
pdo
```

und der Database-Host auf

```
pgsql:host=net-db
```

gesetzt werden (vorausgesetzt Moodle läuft im selben Docker Network - ggf. muss die Containerbezeichnung daher angepasst oder auf eine IP gesetzt werden).  
Die Zugangsdaten für die Datenbank sind in der `docker-compose.yml` zu finden bzw. konfigurieren.

Weitere Konfigurationen sind wie folgt:

- auth_db | table → moodle_users
- auth_db | fielduser → user_name
- auth_db | fieldpass → password
- auth_db | setupsql → SET NAMES 'utf8'
- auth_db | debugauthdb → Yes (Optional)
- auth_db | removeuser → Suspend Internal (Optional)
- auth_db | field_map_email → e_mail
- auth_db | field_lock_email → Locked (Optional)
- auth_db | field_map_idnumber → id_number
- auth_db | field_lock_idnumber → Locked

Anschließend muss diese Funktionalität zwecks Automatisierung im CRON freigeschaltet werden, was unter `Dashboard / Site administration / Server / Tasks / Scheduled Tasks` geschieht. Hier ist der Dienst `\enrol_database\task\sync_users` zu aktivieren.

### Erstellung und Einschreibung von Accounts

Externe User-Datensätze landen in der Tabelle "external_transfer_user". Von dort werden sie in entsprechende Moodle-User Datensätze migriert, welche in der Tabelle "moodle_users" zu finden sind.

Die Tabelle "moodle_users" nutzt als Primary Key eine GUID, welche bei Erstellung eines Datensatzes als Computed Property, konvertiert zu varchar, ebenso in das Feld "id_number" geschrieben wird.
Das Feld "id_number" dient in dieser Konfiguration als Identifikationsmerkmal für die Moodle-Funktion zur Erstellung von Usern.

## Aktivieren des External Database Enrolment

Zunächst muss die Funktionalität zur externen Erstellung von Kursen und Enrolments aktiviert werden. Dies geschieht unter `Dashboard / Site administration / Plugins / Enrolments / External database`. Siehe dazu die [Dokumentation](https://docs.moodle.org/311/en/External_database_enrolment) für weitere Informationen.

### Konfiguration des External Database Enrolment

Für das hier exemplarisch konfigurierten PDO-PostgreSQL-Docker Setup muss der Database-Driver auf

```
pdo
```

und der Database-Host auf

```
pgsql:host=net-db
```

gesetzt werden (vorausgesetzt Moodle läuft im selben Docker Network - ggf. muss die Containerbezeichnung daher angepasst oder auf eine IP gesetzt werden).  
Die Zugangsdaten für die Datenbank sind in der `docker-compose.yml` zu finden bzw. konfigurieren.

Weitere Konfigurationen sind wie folgt:

- enrol_database | dbsetupsql → SET NAMES 'utf8'
- enrol_database | debugdb → Yes (Optional)
- enrol_database | remoteenroltable → moodle_enrolments
- enrol_database | remotecoursefield → course_id_number
- enrol_database | remoteuserfield → user_id_number
- enrol_database | newcoursetable → moodle_courses
- enrol_database | newcoursefullname → full_name
- enrol_database | newcourseshortname → short_name
- enrol_database | newcourseidnumber → id_number

Anschließend muss diese Funktionalität zwecks Automatisierung im CRON freigeschaltet werden, was unter `Dashboard / Site administration / Server / Tasks / Scheduled Tasks` geschieht. Hier ist der Dienst `\enrol_database\task\sync_enrolments` zu aktivieren.

### Erstellung von Kursen

Externe Kurs-Datensätze landen in der Tabelle "external_transfer_course". Von dort werden sie in entsprechende Moodle-Course Datensätze migriert, welche in der Tabelle "moodle_courses" zu finden sind.  
Die Verknüpfung von Kurs und User findet in der Tabelle "external_transfer_course_user" statt, von wo sie in die speziellen Moodle-Enrolment Datensätze migriert werden, welche in der Tabelle "moodle_enrolments" zu finden sind.

Die Tabelle "moodle_courses" nutzt als Primary Key eine GUID, welche bei Erstellung eines Datensatzes als Computed Property, konvertiert zu varchar, ebenso in das Feld "id_number" geschrieben wird.  
Das Feld "id_number" dient in dieser Konfiguration als Identifikationsmerkmal für die Moodle-Funktion zur Erstellung von Kursen.

Für die Enrolments wird jeweils der "id_number"-Wert in das "user_id_number" bzw. "course_id_number" Feld geschrieben, welche der Enrolment-Funktion als Identifikationsmerkmale übergeben werden.
