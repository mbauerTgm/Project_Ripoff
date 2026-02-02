using UnityEngine;

/**
 * Manger für Save&Load Logik.
 * Daten können über onDataUpdate Event aktualisiert werden.
 * @author Maximilian Bauer
 * @version 1.0.0
 */
public class DataManager : MonoBehaviour
{
    private Messaging_Service messaging_Service;
    public GameData currentData;

    private void Awake()
    {
        messaging_Service = FindFirstObjectByType<Messaging_Service>();
        if(currentData  == null)
        {
            LoadData();
        }
    }

    private void OnEnable()
    {
        messaging_Service.onDataUpdate += updateData;
    }

    private void OnDisable()
    {
        messaging_Service.onDataUpdate -= updateData;
    }

    private void updateData(GameData updatedData)
    {
        if(updatedData != null)
        {
            currentData = updatedData;
            SaveSettings();
        }
    }

    private void SaveSettings()
    {
        string json = JsonUtility.ToJson(currentData);

        // JSON in PlayerPrefs speichern
        PlayerPrefs.SetString("GameData", json);
        PlayerPrefs.Save();

        Debug.Log("Data gespeichert: " + json);
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey("GameData"))
        {
            string json = PlayerPrefs.GetString("GameData");
            currentData = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            currentData = new GameData();
        }
        messaging_Service.onDataLoad?.Invoke(currentData);
    }
}
