using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject unitPrefab;
    public int amount;
    public float timeSinceLastSpawn = 0f;
    public float spawnTime = 180f;
    public float darknessSpawnTime = 30f;
    private Vector3 spawnPosition;

    public float timer;

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
        if (timeSinceLastSpawn >= spawnTime)
        {
            SpawnEnemy(1);
            timeSinceLastSpawn -= timeSinceLastSpawn;
        }
        if (ResourceBank.fireLife < 5)
        {
            timer += Time.deltaTime;
            if (timer >= darknessSpawnTime)
            {
                timeSinceLastSpawn = spawnTime;
                timer -= timer;
            }
        }
    }

    public void SpawnEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Debug.Log("Enemy spawned with timer: " + spawnTime);
            Instantiate(unitPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
