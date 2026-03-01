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

    //Konstruktor zum Setzen von Standardwerten (Neues Spiel)
    public GameData()
    {
        highestUnlockedLevel = 0;
    }
}
