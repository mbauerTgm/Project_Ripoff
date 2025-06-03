Maximilian Bauer, Viktor Bublinskyy, Jakob Fenzl, Dominik Sandler
# Project Ripoff

## Einleitung
Project Ripoff ist ein [Singleplayer](#Singleplayer) [CQB](#CQB) Taktik Shooter, bei dem der Spieler in die Rolle eines [Squad Leaders](#SquadLeader) springt und mit einem KI-Team Gebäude bzw. Bereiche sichern muss.
Durch ein begrenztes Sichtfeld und eingeschränkte Informationen muss stets taktisch vorgegangen werden um das Missionsziel zu erreichen.
Das [Assault-Team](#AssaultTeam) kann dabei eine Reihe an Werkzeugen verwenden, um sich zu sichern und Eintritt zu verschaffen.

### Technologie
##### Unity (Game Engine): 
Als [Game Engine](#GameEngine) kommt Unity zum Einsatz. Unity bietet eine Vielzahl an Tools, wie z.B. eine ausgezeichnete Physik-Simulation, die uns die Umsetzung unserer Vision erleichtern. Unser Team hat am meisten Erfahrung mit Unity und muss sich daher nicht erst mit der Umgebung vertraut machen, was es uns ermöglicht, am effizientesten die Funktionen dieser Game Engine zu nutzen.

##### Autodesk Maya (3D-Modellierung)
Für die Modellierung unserer Assets kommt Autodesk Maya zum Einsatz. Genau wie bei Unity hat unser Team am meisten Erfahrung mit dieser Software im Hinblick auf 3D-Modellierung. Maya bietet eine große Auswahl an Tools zur Moddellierung und Animierung, weshalb Maya von führenden Firmen in der Industrie, wie [Disney](https://thewaltdisneycompany.com), [Electronic Arts](https://ea.com), [Epic Games](https://epicgames.com) und [Rockstar Games](https://rockstargames.com) verwendet wird.

## Game Logik / Mechanics
Die Gamelogik besteht darin, dass der Spieler, zusammen mit KI-Teammidgliedern, ein oder mehrere Missionsziele erfüllen muss. Diese könnten unter anderem daraus bestehen, [Intel](#Intel) zu beschaffen oder eine Person zu evakuieren. Unabhängig vom Missionsziel muss immer das ganze Gelände bzw. alle Räume gesichert und alle Feinde eliminiert werden.

Das KI Team folgt und gibt Deckung. Der Spieler kann der KI auch Anweisungen geben, wie z.B. sich an einer Tür aufzustellen, diese dann aufzutreten, ein [Flashbang](#Flashbang) zu werfen und den Raum mit einem [Enter and Clear](https://www.youtube.com/watch?v=vyNWIcjjG2w) Manöver zu betreten. 
Diese Anweisungen können über ein Comander Menü gegeben werden.

Sowohl dem Spieler als auch dem KI Team stehen verschiedene Ausrüstungs Gegenstände zur Verfügung um das [Breaching](#Breaching) zu vereinfachen. Diese Gegenstände währen unter anderem C-4/C-2 Ladungen und [Breaching Shotguns](#BreachingShotgun) (beides ausschließlich zum öffnen von Türen bzw. enfernen von Wänden) oder [Flashbangs](#Flashbang).

Die Angled-Top-Down Kamera kann gedreht werden, damit der Spieler stets das Geschehen im Blick hat. Damit aber der Spieler stets bedacht vorgehen muss, ist das Gebiet verdunkelt und man kann nur dort Gegner oder ähnliches in Sichtkegeln, dort wo der Spieler oder das Team hinsieht, ausmachen.

Die Charackter-Steuerung wäre wegen der Angled-Top-Down View durch Mausklicks an einen Ort, zu dem sich das Team bewegen soll. Ebenfalls steuert man auch die Blickrichtung des Charakters durch die Maus.

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
