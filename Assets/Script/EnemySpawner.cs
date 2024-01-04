using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int maxEnemyNumber;
    private int currentEnemyNumber;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private float maxSpawnTime;
    private float currentSpawnTime = 0;

    private void OnEnable()
    {
        MapManager.Instance.mapEnemyCount = maxEnemyNumber;
    }

    private void Update()
    {
        if (MapManager.Instance.mapClaer)
        {
            return;
        }

        if (currentSpawnTime <= maxSpawnTime)
        {
            currentSpawnTime += Time.deltaTime;
            return;
        }
        SpawnEnemy();
        currentSpawnTime = 0;

        if(currentEnemyNumber >= maxEnemyNumber)
            Destroy(gameObject);
    }

    void SpawnEnemy()
    {
        int spawnEnemy = Random.Range(0, enemies.Length);
        int spawnPoint = Random.Range(0, spawnPoints.Length);
        Instantiate(enemies[spawnEnemy], spawnPoints[spawnPoint].position, Quaternion.identity);
        currentEnemyNumber++;
    }
}
