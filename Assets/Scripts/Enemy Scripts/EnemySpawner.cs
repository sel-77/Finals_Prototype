using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab
    public Transform[] spawnPoints; // Array of spawn points
    public float spawnInterval = 3f; // Time in seconds between spawns

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f; // Reset the timer
        }
    }

    void SpawnEnemy()
    {
        // Choose a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instantiate the enemy at the chosen spawn point
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}
