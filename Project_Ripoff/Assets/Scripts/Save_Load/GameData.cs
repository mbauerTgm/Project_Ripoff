using UnityEngine;
using UnityEngine.UIElements;

/**
 * Mapper Klasse für Daten die gespeichert werden können.
 * @author Maximilian Bauer
 * @version 1.0.0
 */
[System.Serializable]
public class GameData
{
    //Game Attribute
    public int highestUnlockedLevel; //Das höhste freigeschaltene Level

    //Settings Attributes
    public string resolution;
    public float masterVolume;
    public float mouseSensitivity;
    public bool isFullscreen;

    //Konstruktor zum Setzen von Standardwerten (Neues Spiel)
    public GameData()
    {
        highestUnlockedLevel = 0;
        resolution = "1920x1080";
        masterVolume = 1.0f;
        mouseSensitivity = 5.0f;
        isFullscreen = true;
    }
}
