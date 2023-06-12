using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawningPositions;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnTime;

    private float spawnCooldown;

    private void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, spawningPositions.Length);
        Vector3 position = spawningPositions[index].position;

        if (spawnCooldown >= spawnTime)
        {
            Instantiate(enemyPrefab, position, Quaternion.identity);
            spawnCooldown = 0;
        }
        else
            spawnCooldown += Time.deltaTime;
    }
}
