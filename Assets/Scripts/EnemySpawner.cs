using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject unitPrefab;
    public int amount;
    private float timeSinceLastSpawn = 0f;
    public float respawnTime = 15f;
    private Vector3 spawnPosition;

    void Awake()
    {
        spawnPosition = transform.position + new Vector3(1, 0, 0);
        for (int i = 0; i < amount; i++)
        {
            Instantiate(unitPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= respawnTime)
        {
            SpawnEnemy(1);
            timeSinceLastSpawn -= timeSinceLastSpawn;
        }
    }

    public void SpawnEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Debug.Log("Enemy spawned with timer: " + respawnTime);
            Instantiate(unitPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
