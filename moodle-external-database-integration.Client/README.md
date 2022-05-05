# Moodle External Database Integration Example - Client

Dieses Projekt beinhaltet die Konsolenapplikation, welche die Abfrage/Entgegennahme von Daten aus einer externen Quelle (bspw. einem Campusmanagement-System) entgegen nimmt, zwischenspeichert und anschließend für die Nutzung mit Moodle External Database Authentication / Enrolment aufbereitet.

Dabei kommen zwei Workerservices zum Einsatz, welche die entsprechenden Aufgaben zu vordefinierten Zeiten ausführen: einmal für den Import externer Daten, einmal für die Aufbereitung der Daten für Moodle.
