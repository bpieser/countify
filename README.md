# Countify - Ein kleines Utility zum Zählen von Wörtern


## Aufbau

### Countify.Core
- **Typ:** .NET Core 8 Bibliothek
- **Funktionalität:** 
  - Beinhaltet die Logik zum Parsen von Dateien und Zählen von Wörtern.
  - Plattformübergreifende Unterstützung durch .NET Core 8.
- **Ziel:** Wiederverwendbare, testbare Komponenten für verschiedene Anwendungen.

### Countify.UI
- **Typ:** WPF-Anwendung basierend auf .NET Core 8
- **Funktionalität:** 
  - Hauptansicht der Anwendung.
  - Implementiert das **MVVM-Pattern** (Model-View-ViewModel) zur Modularisierung und Trennung von Logik und Darstellung.
- **Ziel:** Intuitive Benutzeroberfläche.

### **Tester**
- **Typ:** xUnit Testprojekt
- **Funktionalität:**
  - Automatisierte Tests für `Countify.Core` und `Countify.UI`.
  - Enthält Testdaten für reproduzierbare Tests.
  - Automatische Generierung großer Testdaten vor der Testausführung.
- **Ziel:** Sicherstellung der Stabilität und Zuverlässigkeit.


## Performance und Responsiveness

- **Asynchrones Datenladen:** 
  - Die ausgewählte Textdatei wird asynchron eingelesen, wodurch die Benutzeroberfläche reaktionsfähig bleibt.
  - Der Einlesealgorithmus hat eine Zeitkomplexität von **O(n)**, was effizientes Zählen auch bei großen Dateien gewährleistet.
  
- **Darstellung:** 
  - Die Daten werden in einem `DataGrid`-Control angezeigt. Dieses Standard-Control bietet eine gute Performance bei großen Datensätzen.
  - Virtualisierung des Controls ist aktiviert.


## Code-Qualität

- **Lesbarkeit:**
  - Der Code ist klar strukturiert und folgt gängigen Namenskonventionen (englischsprachige, selbsterklärende Bezeichner).
  
- **Architektur:**
  - Implementiert das **MVVM-Pattern**, um Präsentationslogik von der UI zu trennen.
  - Verwendet **Interfaces**, um Abhängigkeiten zu reduzieren und Dependency Injection zu ermöglichen.

- **Testbarkeit:**
  - **Unit-Tests** für:
    - Die File-Parse-Logik in `Countify.Core`.
    - Das ViewModel `DocumentViewModel.cs` in `Countify.UI`.
  - **Mocking** von Interfaces zur Isolation von Testfällen.

- **Qualitätsmerkmale:**
  - Der Code enthält keine Fehler, Warnungen oder Hinweise in Visual Studio.
  - `Nullable` ist in allen Projekten aktiviert, um potenzielle Nullreferenzfehler zu vermeiden.
 
- **Automatisierung:**
  - Der Code ist auf GitHub versioniert. [Link](https://github.com/bpieser/countify)
  - CI/CD-Pipeline ist aktiviert, um sicher zu stellen, dass der Code auch auf anderen Systemen baut und die Unit-Tests laufen. [Link](https://github.com/bpieser/countify/actions/runs/11880344999)
