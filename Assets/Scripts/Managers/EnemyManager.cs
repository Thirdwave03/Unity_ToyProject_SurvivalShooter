using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth PlayerHealth;
    public GameObject[] enemies;
    public float zombunnySpawnTime = 3f;
    public float zombearSpawnTime = 3f;
    public float hellephantSpawnTime = 10f;
    public Transform[] zombunnySpawnPoints;
    public Transform[] zombearSpawnPoints;
    public Transform[] hellephantSpawnPoints;

    private void Start()
    {
        InvokeRepeating("SpawnZombunny", zombunnySpawnTime, zombunnySpawnTime);
        InvokeRepeating("SpawnZombear", zombearSpawnTime, zombearSpawnTime);
        InvokeRepeating("SpawnHellephant", hellephantSpawnTime, hellephantSpawnTime);
    }

    private void SpawnZombunny()
    {
        if (PlayerHealth.currentHealth <= 0)
            return;

        int spawnPointIndex = Random.Range(0, zombunnySpawnPoints.Length);

        Instantiate(enemies[0], zombunnySpawnPoints[spawnPointIndex].position, zombunnySpawnPoints[spawnPointIndex].rotation);
    }

    private void SpawnZombear()
    {
        if (PlayerHealth.currentHealth <= 0)
            return;

        int spawnPointIndex = Random.Range(0, zombearSpawnPoints.Length);

        Instantiate(enemies[1], zombearSpawnPoints[spawnPointIndex].position, zombearSpawnPoints[spawnPointIndex].rotation);
    }

    private void SpawnHellephant()
    {
        if (PlayerHealth.currentHealth <= 0)
            return;

        int spawnPointIndex = Random.Range(0, hellephantSpawnPoints.Length);

        Instantiate(enemies[2], hellephantSpawnPoints[spawnPointIndex].position, hellephantSpawnPoints[spawnPointIndex].rotation);
    }
}
