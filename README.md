Maximilian Bauer, Viktor Bublinskyy, Jakob Fenzl, Dominik Sandler
# Lastenheft Project Ripoff

## Einleitung
Project Ripoff ist ein [Singleplayer](#Singleplayer) [CQB](#CQB) Taktik Shooter, bei dem der Spieler in die Rolle eines [Squad Leaders](#SquadLeader) springt und mit einem KI-Team Gebäude bzw. Bereiche sichern muss.
Durch ein begrenztes Sichtfeld und eingeschränkte Informationen muss stets taktisch vorgegangen werden um das Missionsziel zu erreichen.
Das [Assault-Team](#AssaultTeam) kann dabei eine Reihe an Werkzeugen verwenden, um sich zu sichern und Eintritt zu verschaffen.

### Technologie
##### Unity (Game Engine): 
Als [Game Engine](#GameEngine) kommt Unity zum Einsatz. Unity bietet eine Vielzahl an Tools, wie z.B. eine ausgezeichnete Physik-Simulation, die uns die Umsetzung unserer Vision erleichtern. Unser Team hat am meisten Erfahrung mit Unity und muss sich daher nicht erst mit der Umgebung vertraut machen, was es uns ermöglicht, am effizientesten die Funktionen dieser Game Engine zu nutzen.

##### Autodesk Maya (3D-Modellierung)
Für die Modellierung unserer Assets kommt Autodesk Maya zum Einsatz. Genau wie bei Unity hat unser Team am meisten Erfahrung mit dieser Software im Hinblick auf 3D-Modellierung. Maya bietet eine große Auswahl an Tools zur Moddellierung und Animierung, weshalb Maya von führenden Firmen in der Industrie, wie [Disney](https://thewaltdisneycompany.com), [Electronic Arts](https://ea.com), [Epic Games](https://epicgames.com) und [Rockstar Games](https://rockstargames.com) verwendet wird.
<div style="page-break-after: always;"></div>

## Recherche

### Existierende Games
Wir haben 4 Spiele, von denen wir zum Teil Spielmechaniken einbinden möchten:
#### Ready Or Not (Spielmechaniken):
[Ready Or Not](https://store.steampowered.com/app/1144200/Ready_or_Not/) ist ein taktischer Ego-Shooter, bei dem Spieler die Rolle eines Teamleiters übernehmen und in verschiedenen Szenarien strategisch vorgehen müssen. Es gibt verschiedene Spielmodi, darunter einen taktischen Multiplayer-Modus und einen Einzelspieler-Modus.
Das Spiel legt großen Wert auf taktische Planung, Teamkoordination und eine realistische Spielumgebung. Von diesem Spiel möchten wir die Grundidee nehmen, nämlich SWAT-Missionen die in Teams ausgeführt werden mit Befehlen.
![ef8c85df-3a37-4055-aa89-d9210b32e5e7_1920x1080](https://hackmd.io/_uploads/H1GGFy0yge.jpg)
![inhWAcs](https://hackmd.io/_uploads/ByNR2kAkeg.png)
Gameplay Footage:
https://www.youtube.com/watch?v=b30s0P3zza8
<div style="page-break-after: always;"></div>

#### Tactical Breach Wizards (Kamera Perspektive)
[Tactical Breach Wizards](https://store.steampowered.com/app/1043810/Tactical_Breach_Wizards/) ist ein humorvolles, rundenbasiertes Taktikspiel. Spieler führen ein Team von Magiern in moderner taktischer Ausrüstung durch Missionen, bei denen sie kreative Zaubersprüche einsetzen, um Gegner zu besiegen. Das Spiel bietet eine Hauptkampagne, optionale Herausforderungen und einen Level-Editor. Von dem Spiel möchten wir lediglich die Kameraperspektive namens Angled Top-Down nehmen, die so aussieht:
![image](https://hackmd.io/_uploads/HJETTP6yee.png)
<div style="page-break-after: always;"></div>

#### Door Kickers 2 (Spielmechaniken, Sichtkegel)
[Door Kickers 2](https://store.steampowered.com/app/1239080/Door_Kickers_2_Task_Force_North/) ist ein taktisches Echtzeit-Strategiespiel, das in einer Top-Down-Perspektive gespielt wird. Es bietet eine Vogelperspektive auf das Spielgeschehen. Spieler kommandieren Spezialeinheiten gegen  Organisationen im Nahen Osten. Das Spiel legt großen Wert auf taktische Planung, den Einsatz von Spezialkräften und die Koordination von Teammitgliedern. Das Spiel bietet auch eine Pausefunktion, die es Spielern ermöglicht, das Spielgeschehen anzuhalten und ihre nächsten Schritte zu planen. Bei diesem Spiel möchten wir die Mechaniken nehmen, dass der Spieler militärische Spezialeinheiten koordiniert um ein Missionsziel zu erreichen. Außerdem möchten wir das Sichtkegelsystem benutzen, bei dem der Spieler nur ein bestimmtes Sichtfeld hat:
![image](https://hackmd.io/_uploads/B1QA1_pkgg.png)
Gameplay-Footage:
https://www.youtube.com/watch?v=oea7XbnAj6s
<div style="page-break-after: always;"></div>

<a id="TAVR"></a>
#### Tactical Assault VR (Spielmechaniken, Grafischer Look)
Spielmechaniken: [Tactical Assault VR](https://store.steampowered.com/app/2314160/Tactical_Assault_VR/) ist ein taktischer Shooter, der sich auf den Einsatz realer [CQB](#CQB)-Taktiken konzentriert. Spieler können entweder solo oder in einem Online-Modus mit bis zu 8 Spielern spielen. Das Spiel bietet sowohl PvE- als auch PvP-Modi und legt großen Wert auf taktische Planung und Teamkoordination. Es gibt eine Vielzahl von Ausrüstungen zur Auswahl. Von diesem Spiel möchten wir den [CQB](#QCB) Spielstil, sowie auch den grafischen Stil des Spiels nehmen, welcher nicht zu realistisch ist, aber auch nicht zu simpel.
![ss_c77d32432ec291b6ca6b4953a49196c6a9e8323a.1920x1080](https://hackmd.io/_uploads/rkLfb_p1lg.jpg)
Gameplay-Footage:
https://www.youtube.com/watch?v=bFo1LuGP1pc&t=19s
<div style="page-break-after: always;"></div>

## Game Logik / Mechanics
Die Gamelogik besteht darin, dass der Spieler, zusammen mit KI-Teammidgliedern, ein oder mehrere Missionsziele erfüllen muss. Diese könnten unter anderem daraus bestehen, [Intel](#Intel) zu beschaffen oder eine Person zu evakuieren. Unabhängig vom Missionsziel muss immer das ganze Gelände bzw. alle Räume gesichert und alle Feinde eliminiert werden.

Das KI Team folgt und gibt Deckung. Der Spieler kann der KI auch Anweisungen geben, wie z.B. sich an einer Tür aufzustellen, diese dann aufzutreten, ein [Flashbang](#Flashbang) zu werfen und den Raum mit einem [Enter and Clear](https://www.youtube.com/watch?v=vyNWIcjjG2w) Manöver zu betreten. 
Diese Anweisungen können über ein Comander Menü gegeben werden.

Sowohl dem Spieler als auch dem KI Team stehen verschiedene Ausrüstungs Gegenstände zur Verfügung um das [Breaching](#Breaching) zu vereinfachen. Diese Gegenstände währen unter anderem C-4/C-2 Ladungen und [Breaching Shotguns](#BreachingShotgun) (beides ausschließlich zum öffnen von Türen bzw. enfernen von Wänden) oder [Flashbangs](#Flashbang).

Die Angled-Top-Down Kamera kann gedreht werden, damit der Spieler stets das Geschehen im Blick hat. Damit aber der Spieler stets bedacht vorgehen muss, ist das Gebiet verdunkelt und man kann nur dort Gegner oder ähnliches in Sichtkegeln, dort wo der Spieler oder das Team hinsieht, ausmachen.

Die Charackter-Steuerung wäre wegen der Angled-Top-Down View durch Mausklicks an einen Ort, zu dem sich das Team bewegen soll. Ebenfalls steuert man auch die Blickrichtung des Charakters durch die Maus.
<div style="page-break-after: always;"></div>

## Assets
#### Artstyle
Der Artstyle soll ähnlich wie bei [Tactical Assault VR](#TAVR), in 3D und in einem halb-realistischen Low Poly Style umgesetzt werden wobei die Kamera in einer Angled-Top-Down Perspective ist.

#### Models
Geplant sind selbsterstellte Player sowie Teammitglieder Models. Außerdem zumindest 2 verschiedene Gegner Models die auch selbst erstellt werden.
Es sollen 3 verschiedene Levels/Maps mit verschiedenen Szenarien und Looks erstellt werden. Des Weiteren werden wir die Waffen modellieren.

#### Animationen
Die Character sowie Utensilien werden animiert. Zum einen das Movement in verschiedenen Geschwindigkeiten, sowie Gangweisen und zum anderen die Utensilien beim Nachladen, Feuern und Blockierungen beheben. 

#### Schätzung Assets
* 4 Character Models
* 5 Animationen pro Character
* 3 Weapon Models
* 4 Animationen pro Weapon
* 3 Umgebungen/Szenarien
<div style="page-break-after: always;"></div>

## Grobplanung & Zeitschätzung
Maximilian Bauer - Programmierung, Game-Mechanics, Game Design
Viktor Bublinskyy - GUI, Programmierung
Dominik Sandler - Programmierung, Mechanics, Sound-Design
Jakob Fenzl - Modellieren, Animieren


### Zeitschätzung:

<br>

#### 3D Modelling/Animation:

##### <ins>Charakter Models</ins>
- 2 **Base Charakter Models** (Player und Gegner)
    - jeweils 7 Stunden 30 min
- 2 **Varianten Charakter Models** (Teammidglieder und Gegner)
    - jeweils 4 Stunden
- 5 **Animationen** (diese können auf alle Charakter Models gleich angewendet werden)
    - jeweils 30 Minuten

##### <ins>Weapon Models</ins>
- 3 **Weapon Models** 
    - jeweils 5 Stunden
- 4 **Weapon Animationen** pro Model
    - jeweils 15 Minuten

#### <ins>Maps</ins>
- 3 verschiedene **Levels/Maps**
    - jeweils ca. 25 Stunden

#### Zeitschätzung 3D Modelling/Animation insgesammt
((2 * 7h 30min) + (2 * 4h) + (5 * 30 min)) + ((3 * 5h) + (4 * 15min)) + (3 * 25h) = <ins>**116 Stunden 30 Minuten**</ins>
<div style="page-break-after: always;"></div>

#### Game Development:
##### <ins>Game Mechanics</ins>
- **Player Movement**
    - 10 Stunden
- **Sicht** (Sichtkegel)
    - 5 Stunden
- **KI Team** (Movement, Verhalten, Befehlsausführung, ...)
    - 40 Stunden
- **Gegner KI** (Movement, Verhalten)
    - 20 Stunden
- **Ein Stückchen Blei mit Kinetischer Energie bewegen**
    - 10 Stunden
- **Kamera Movement**
    - 4 Stunden
- **Durchbrechbare Wände und Türen**
    - 15 Stunden
- **Gadgets** ([Flashbangs](#Flashbang), [Breaching Shotguns](#BreachingShotgun), ...)
    - jeweils 5 Stunden

##### <ins>User Interface</ins>
- **UI**
    - 10 Stunden

##### <ins>Sounds</ins>
- **Sounds sourcen/aufnehmen**
    - ca. 10 Stunden
- **Ingame Soundeffects**
    - 5 Stunden

##### <ins>Visual Effects</ins>
- **Visuelle Effekte**
    - ca. 20 Stunden

#### Zeitschätzung Game Development insgesammt
10h + 5h + 40h + 20h + 10h + 4h + 15h + (4 * 5h) + 10h + 10h + 5h + 20h = <ins>**169 Stunden**</ins>

<br>

#### Testing & Bugfixing
##### <ins>Testing</ins>
- Spielelemente Testen (Sowohl während der Entwicklung als auch danach)
    - ca. **10 Stunden**
- Bugfixing
    - ca. **40 Stunden**
#### Zeitschätzung Game Development insgesammt
10h + 40h = <ins>**50 Stunden**</ins>


### Zeitschätzung insgesammt
116 Stunden & 30 Minuten (3D Modelling/Animation) + 169 Stunden (Game Development) + 50 Stunden (Testing & Bugfixing) = <ins>**335 Stunden 30 Minuten**</ins>
<div style="page-break-after: always;"></div>

## Glossar
<a id="Singleplayer"></a>
- **Singleplayer**: Der Einzelspieler-Modus (englisch singleplayer oder single-player) ist ein Modus in Videospielen, bei dem man ohne weitere menschliche Mit- oder Gegenspieler spielt.
<a id="GameEngine"></a>
- **Game-Engine**: Eine Spiel-Engine, auch Game-Engine oder Game Engine ist ein spezielles Framework zur Entwicklung von Computerspielen, das den Spielverlauf steuert und für die visuelle Darstellung des Spielablaufes verantwortlich ist. In der Regel stellen derartige Plattformen auch integrierte Entwicklungsumgebungen bereit. Zu den am häufigsten verwendeten 3D-Engines gehören CryEngine, Frostbite, Godot, **Unity** und Unreal Engine.
<a id="CQB"></a>
- **CQB (Close-quarters battle)**: Nahkampf in engen, begrenzten Räumen wie Gebäuden, Gängen oder Fahrzeugen.
<a id="Breaching"></a>
- **Breaching**: Das gewaltsame Öffnen oder Überwinden von Zugängen wie Türen, Fenstern oder Barrikaden, um in einen Raum oder ein Gebäude einzudringen. Methoden können mechanisch, ballistisch oder explosiv sein.
<a id="BreachingShotgun"></a>
- **Breaching Shotgun**: Mit einer Breaching Shotgun ist eine, meist kurze, Schrotflinte die mit speziellen breaching Patronen geladen ist. Eine breaching round ist eine Schrotpatrone, die speziell für das Aufbrechen von Türen entwickelt wurde. Sie wird in der Regel auf eine Entfernung von 15 cm oder weniger abgefeuert, zielt auf die Türscharniere oder den Bereich zwischen Türknauf, Schloss und Türpfosten und ist so konzipiert, dass sie das getroffene Objekt zerstört und sich anschließend in ein relativ harmloses Pulver auflöst.
<a id="SquadLeader"></a>
- **Squad Leader**: Der Anführer eines kleinen Teams (Squad), der für die Koordination, Taktik und Entscheidungsfindung während eines Einsatzes zuständig ist. Er gibt Befehle, setzt Ziele und sorgt für den Überblick im Gefecht.
<a id="FireTeam"></a>
- **Fire Team**: Eine kleine militärische Einheit, typischerweise bestehend aus 3 bis 5 Soldaten, die gemeinsam operieren. Das **Fire Team** agiert flexibel und eigenständig innerhalb eines größeren [Squads](#SquadLeader). Es besteht meist aus Rollen wie Schütze, Unterstützer, Grenadier oder Teamführer und ist darauf ausgelegt, Feuerkraft und Bewegung effektiv zu kombinieren.
<a id="AssaultTeam"></a>
- **Assault-Team**: Beim betreten eines zu sichernden Gebäudes teilen sich die [Fireteams](#FireTeam) des [Squads](#SquadLeader) oft in [Follow On Team](#FollowOnTeam) und **Assault Team** auf. Das **Assault-Team** führt den Sturm auf ein Ziel aus.
<a id="FollowOnTeam"></a>
- **Follow On Team**: Das zweite [Fire Team](#FireTeam), dass dem [Assault Team](#AssaultTeam) während des [breachens](#Breaching) Dekung gibt.
<a id="Intel"></a>
- **Intel (Intelligence)**: Informationen über Feindbewegungen, Positionen, Ausrüstung oder Pläne. Intel ist entscheidend für die Einsatzplanung und kann aus Beobachtungen, Drohnen, Dokumenten oder Befragungen stammen.
<a id="Flashbang"></a>
- **Flashbang**: Eine Blendgranate, die mit einem lauten Knall und grellem Licht Gegner kurzzeitig desorientiert. Sie wird eingesetzt, um Räume zu sichern oder Gegner zu überraschen, ohne tödliche Gewalt anzuwenden.
