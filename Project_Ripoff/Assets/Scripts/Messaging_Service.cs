using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Die Klasse bietet eine Funktionalität als Messaging Service.
 * Dabei können Service wie Input mit zB. dem Player Movenment kommunizieren ohne gekoppelt zu sein 
 * bzw. sich gegeseitig zu kennen.
 * Dokumentation/Guideline zur Implementierung ist unter 
 * "https://projekte.tgm.ac.at/youtrack/articles/RIP-A-9/Observer-Pattern-Guideline-Ripoff-Disposable-Heroes" zu finden.
 * @author Maximilian Bauer
 * @version 1.0.1
 */
public class Messaging_Service : MonoBehaviour
{
    //----------------------
    // Game
    //----------------------

    //Sollte der Spieler in einem Level sterben
    public Action GameOver;

    //----------------------
    //player Interaction
    //----------------------

    //Wenn der Spieler, mittels Rechtsklick, eine neue Position für den Charakter bestimmt hat
    public Action<Vector3> playerMoveEvent;

    //Wenn der Spieler, mittels Shift + Rechtsklick, eine Position designiert die in folge Erreichtwerden soll
    public Action<Vector3> playerMoveShiftEvent;

    //Wenn der Spieler mittels Shift die Queue visualisieren will
    public Action<bool> showPlayerQueue;

    //Wenn der Spieler mittels Linksklick einen "Feuerbefehl" für den Playercharacter gibt
    public Action fireLaserShotPlayer;

    //Mit diesem Event kann entkoppelt, die Position des PlayerCharacters bestimmt werden
    public Func<Vector3> getPlayerPosition;

    //Wenn der Spieler mit der Maus über das Objective hovert und den capture Button drückt ("F")
    public Action interactObjectiveButtonDepress;

    //Wenn der Spieler mit der Maus über das Objective hovert und den capture Button los lässt ("F")
    public Action interactObjectiveButtonRelease;

    //Wenn der Spieler über ein Objective Hovert
    public Action<ObjectiveInteractable> processInteractionEvent;

    //Wenn der Spieler aufhört über ein Objective Hovern
    public Action<ObjectiveInteractable> resetProgressEvent;

    //----------------------
    //camera
    //----------------------

    //Wenn der Spieler mittels W,A,S oder D, die Kamera in der Welt bewegen will
    public Action<Vector3> moveCameraTarget;

    //Wenn der Spieler mittels Mausrad die Kamera hinein oder raus zoomed
    public Action<float> zoomCamera;

    //Wenn der Spieler Mausrad gedrückt hält und mit der Maus die Kamera bewegt
    public Action<bool> rotateCameraWithMouse;

    //----------------------
    //Game-Data (Save&Load)
    //----------------------

    //Um geänderte Daten oder Settings, wie das höchste Level mitzuteilen, diese werden durch DataManger gespeichert
    public Action<GameData> onDataUpdate;

    //Um geladene Daten & Settings zuerhalten, wird durch DataManger ausgelöst
    public Action<GameData> onDataLoad;

    //----------------------
    //AI
    //----------------------

    //Wenn der Spieler das Event ausloest sollen die Teammates dem Spieler folgen
    public Action followPlayerEvent;

    //Wenn der Spieler das Event ausloest sollen die Teammates dem Spieler nicht mehr folgen, sondern dort belieben wo sie sind
    public Action holdPositionEvent;

    //Wenn er Spieler das Event ausloest sollen dei Teammates an eine vom Spieler ausgewaehlte Position sich bewegen
    public Action<Vector3> teammateMoveEvent;

    //Wenn der Spieler, mittels Shift + Rechtsklick, eine Position designiert die das Team Queue Weise erreichen soll
    public Action<Vector3> teammateMoveShiftEvent;

    //Wenn der Spieler eine Position die suppressed werden soll, designiert
    public Action<Vector3> suppressPositionEvent;

    // Die Teammates sollen eine Wedge Formation einnehmen
    public Action wedgeFormationEvent;

    // Die Teammates sollen eine Line Formation einnehmen
    public Action lineFormationEvent;

    //Die Teammate sollen keine Formation einnehmen
    public Action noneFormationEvent;

    //----------------------
    //UI
    //----------------------

    //Wenn der Spieler mittels F, bzw. dem UI Button das Commander Menü öffnen will
    public Action openCommanderMenu;

    public Action toggleSettings;

    //Wenn der Spieler mit Esc das Pausemenü öffnen oder schließen will
    public Action togglePauseMenu;

    //Wenn der Spieler eine Position für das MoveTo auswählt soll der Cursor geändert werden
    public Action<bool> selectTeamMovePosition;

    //Wenn der Spieler über ein Objective Hovert und dies angezeigt werden soll.
    public Action<Vector3> visualizeObjectiveHoverAt;

    //----------------------
    //SFX
    //----------------------
    // Wenn ein Sound abgespielt wird, dabei ist der String der Name des Sounds und Vector3 die Position wo es abgespielt werden soll.
    public Action<string, Vector3> playSFXEvent;
    // Wenn ein Sound für die UI abgespielt wird, dabei ist der String der Name des Sounds und Vector3 die Position wo es abgespielt werden soll.
    public Action<string> playUISFXEvent;
}
