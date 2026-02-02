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

    private List<GameObject> teammates = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < Teammate_count; i++)
        {
            SpawnBot(i + 1);
        }
    }


    public void SpawnBot(int i)
    {
        Vector3 spawnPos = GetRandomNavMeshPosition();
        GameObject teammate = Instantiate(TeammatePrefab, spawnPos, Quaternion.identity);
        TeammateEventListener listener = teammate.GetComponent<TeammateEventListener>();
        var bb = teammate.GetComponent<Blackboard>();

        if (listener != null)
        {
            listener.setIndex(i);
        }
        }

    private Vector3 GetRandomNavMeshPosition()
    {
        int x = UnityEngine.Random.Range( - spawnRadius,spawnRadius);
        int z = UnityEngine.Random.Range(-spawnRadius, spawnRadius);
        return new Vector3(x, spawn_height , z);
    }
    void Update()
    {
        
    }
}
