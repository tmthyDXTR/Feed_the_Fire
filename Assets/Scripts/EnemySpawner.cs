using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject unitPrefab;
    public GameObject unitPrefab2;
    public int amount;
    public float timeSinceLastSpawn = 0f;
    public float spawnTime = 180f;
    public float darknessSpawnTime = 30f;
    private Vector3 spawnPosition;
    public float timer;
    private Vector3 randomPos;
    GameStats gameStats;



    void Start()
    {
        spawnPosition = transform.position + new Vector3(1, 0, 0);
        timeSinceLastSpawn = Random.Range(0, 120);
        for (int i = 0; i < amount; i++)
        {
            randomPos = transform.position + new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
            GameObject enemy = Instantiate(unitPrefab, randomPos, Quaternion.identity) as GameObject;
            enemy.transform.SetParent(GameObject.Find("Level").transform);
        }
        gameStats = GameObject.Find("Game").GetComponent<GameStats>();




    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnTime)
        {
            SpawnEnemy(1, unitPrefab);
            timeSinceLastSpawn -= timeSinceLastSpawn;
        }
        if (gameStats.fireLife < 5)
        {
            timer += Time.deltaTime;
            if (timer >= darknessSpawnTime)
            {
                if (gameStats.omenSpawned < 1)
                {
                    SpawnEnemy(1, unitPrefab2);
                    gameStats.omenSpawned += 1;
                }
                timer -= timer;
            }
        }
    }

    public void SpawnEnemy(int amount, GameObject prefab)
    {
        for (int i = 0; i < amount; i++)
        {
            //Debug.Log("Enemy spawned with timer: " + spawnTime);
            randomPos = transform.position + new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
            GameObject enemy = Instantiate(prefab, randomPos, Quaternion.identity) as GameObject;
            enemy.transform.SetParent(GameObject.Find("Level").transform);
        }
    }
}
