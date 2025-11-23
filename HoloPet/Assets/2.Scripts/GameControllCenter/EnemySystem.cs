using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private List<GameObject> Enemies = new List<GameObject>();
    [SerializeField] private List<GameObject> Players = new List<GameObject>();
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerSpawnPoint; // optional separate spawn point

    public event EventHandler OnEnemyExist;
    public event EventHandler OnEnemyGone;

    private Coroutine spawnRoutine;
    private Coroutine playerSpawnRoutine;
    private int lastEnemyCount = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (spawnRoutine == null) // prevent multiple enemy spawns at once
            {
                spawnRoutine = StartCoroutine(SpawnEnemies(1, 1f));
                OnEnemyExist?.Invoke(this, EventArgs.Empty);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (spawnRoutine == null) // prevent multiple enemy spawns at once
            {
                spawnRoutine = StartCoroutine(SpawnEnemies(10, 1f));
                OnEnemyExist?.Invoke(this, EventArgs.Empty);
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (playerSpawnRoutine == null) // prevent multiple player spawns at once
            {
                playerSpawnRoutine = StartCoroutine(SpawnPlayers(1, 0.5f));
            }
        }

        // Enemy exist/gone events
        if (Enemies.Count > 0 && Players.Count > 0)
        {
            foreach (GameObject player in Players)
            {
                //start
            }
        }
    }

    private IEnumerator SpawnEnemies(int count, float delay)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            Enemies.Add(newEnemy);
            Debug.Log("Spawned Enemy " + i);

            OnEnemyExist?.Invoke(this, EventArgs.Empty);
            yield return new WaitForSeconds(delay);
        }

        spawnRoutine = null; // reset when finished
    }

    private IEnumerator SpawnPlayers(int count, float delay)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos = playerSpawnPoint != null ? playerSpawnPoint.position : spawnPoint.position;
            GameObject newPlayer = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
            Players.Add(newPlayer);
            Debug.Log("Spawned Player " + i);

            yield return new WaitForSeconds(delay);
        }

        playerSpawnRoutine = null; // reset when finished
    }
}
