# FileRenamer - Usage
Command: renamer img-1.jpg 1-img.jpg
Result: Renames "img-1.jpg" to "1-img.jpg"

Command: renamer img1.jpg img-1.jpg
Result: Renames "img1.jpg" to "img-1.jpg"

Command: renamer img**
Result: Removes "img" from the filenames of all files

Command: renamer *jpg* *png*
Result: Changes the extension of all files to ".png"

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
