using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Speichert und verwaltet States wie Pause, GameOver und Objective Completion
/// <br></br>
/// <br></br>
/// <b>Author: Maximilian Bauer</b>
/// <br></br>
/// <b>Version 1.0.0</b>
/// </summary>
public class GameManager : MonoBehaviour
{
    private Messaging_Service messaging_Service;
    private bool isGamePaused = false;
    private bool isObjectiveComplete = false;
    private float nextWinCheck = 0f;
    private List<Enemy> enemys = new List<Enemy>();
    private bool atLeastOneEnemyWasCreated = false;

    private void Awake()
    {
        messaging_Service = FindFirstObjectByType<Messaging_Service>();
        Time.timeScale = 1;
    }

    private void OnEnable()
    {

        if (messaging_Service != null)
        {
            messaging_Service.togglePauseMenu += ToggleGamePause;
            messaging_Service.restartLevel += RestartLevel;
            messaging_Service.objectiveComplete += ObjectiveComplete;
            messaging_Service.onEnemyCreation += AddEnemyToList;
            messaging_Service.onEnemyDestruction += RemoveEnemyFromList;
        }
    }

    private void OnDisable()
    {
        if (messaging_Service != null)
        {
            messaging_Service.togglePauseMenu -= ToggleGamePause;
            messaging_Service.restartLevel -= RestartLevel;
            messaging_Service.objectiveComplete -= ObjectiveComplete;
            messaging_Service.onEnemyCreation -= AddEnemyToList;
            messaging_Service.onEnemyDestruction -= RemoveEnemyFromList;
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(nextWinCheck >= 100)
        {
            nextWinCheck = 0;
            if (CheckWinConditions())
            {
                Debug.Log("LevelComplete");
            }
        }
    }

    private void ToggleGamePause()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void RestartLevel()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }

    private void ObjectiveComplete()
    {
        isObjectiveComplete = true;
    }

    private bool CheckWinConditions()
    {
        if(!atLeastOneEnemyWasCreated) return false;

        if(isObjectiveComplete && enemys.Count == 0) return true;

        return false;
    }

    private void AddEnemyToList(Enemy enemy)
    {
        if(enemy == null) return;
        enemys.Add(enemy);
        atLeastOneEnemyWasCreated = true;
    }

    private void RemoveEnemyFromList(Enemy enemy)
    {
        if (enemy == null) return;
        enemys.Remove(enemy);
    }
}
