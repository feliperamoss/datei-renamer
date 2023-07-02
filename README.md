# FileRenamer - Verwendung in der Konsole

Kopiere den gesamten Pfad von FileRenamer.exe, zum Beispiel: 

C:\Documents\FileRenamer\FileRenamer\bin\Debug\FileRenamer.exe und füge den gewünschten Befehl hinzu.

C:\docs\c#\FileRenamer\FileRenamer\bin\Debug\FileRenamer.exe renamer img1.jpg img-1.jgp

# Befehle Beispiele:

Befehl: `renamer img-1.jpg 1-img.jpg`

Ergebnis: "img-1.jpg" -> "1-img.jpg"

Befehl: `renamer img1.jpg img-1.jpg`

Ergebnis: "img1.jpg" -> "img-1.jpg"

Befehl: `renamer img**`

Ergebnis: Entfernt "img" aus den Dateinamen aller Dateien

Befehl: `renamer *.jpg* *.png*`

Ergebnis: Ändert den Dateityp aller Dateien in "png"

Befehl: `renamer img-* image-*`

Ergebnis: "img-1.jpg" -> "image-1.jpg"

Befehl: `renamer img-1.jpg image-1.jpg`

Ergebnis: Ändern alle Dateien mit dem gleichen Datentyp "img-1.jpg" -> "image-1.jpg"

Befehl: `renamer *.jpg*`

Ergebnis: Entfernt ".jpg" aus den Dateinamen aller Dateie

# Workflow
1. Repo klonen

2. Wechseln zu develop Branch. In Visual Studio: 
- Git (oben)
- Branches verwalten
- Auf develop clicken (links)
- Right click (neue Branch) von develop. Name Beispiel: feature-change-filename

Wechseln zu develop Branch mit git command:

# Wechseln zu develop Branch mit git command
`git checkout develop`

# Erstellen eines neuen Branches
`git branch new-branch-name`

# Wechseln zum neu erstellten Branch
`git checkout new-branch-name` (um die neue Branch zu wechseln)

3. Commits:
`git commit -m 'änderungen'`

5. Push Changes:
`git push origin branch-name`
Wenn du noch kein Remote-Repository eingerichtet hast, kannst du eines mit dem folgenden Befehl hinzufügen:

- git remote add origin remote-url (Ersetze "remote-url" durch die URL deines Remote-Repositorys. Sobald du das Remote hinzugefügt hast, kannst du mit dem oben erwähnten Push-Befehl fortfahren. Vor dem Pushen solltest du sicherstellen, dass du deine Änderungen mit dem Befehl git commit festgehalten hast.)
