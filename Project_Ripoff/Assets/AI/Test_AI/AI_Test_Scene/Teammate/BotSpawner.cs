using MBT;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
 * @author Dominik Sandler
 * 
 * Diese Klasse ermöglicht es ,dass man die Teammates spawnen kann
 * 
 */
public class BotSpawner : MonoBehaviour
{
    public GameObject TeammatePrefab;
    public int Teammate_count;

    public Transform player;

    public float spawn_height;

    public int spawnRadius = 10;
    public Transform spawnpoint;

    private List<GameObject> teammates = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < Teammate_count; i++)
        {
            SpawnBot(i);
        }
    }


    public void SpawnBot(int i)
    {
        Vector3 spawnPos = GetRandomNavMeshPosition();
        GameObject teammate = Instantiate(TeammatePrefab, spawnPos, Quaternion.identity);
        teammate.SetActive(true);
        TeammateEventListener listener = teammate.GetComponent<TeammateEventListener>();

        if (listener != null)
        {
            listener.setIndex(i);
            listener.formationManager = FindFirstObjectByType<FormationManager>();
        }
        }

    private Vector3 GetRandomNavMeshPosition()
    {
        int x = UnityEngine.Random.Range( - spawnRadius,spawnRadius);
        int z = UnityEngine.Random.Range(-spawnRadius, spawnRadius);
        return spawnpoint.position + new Vector3(x, spawn_height , z);
    }
    void Update()
    {
        
    }
}
