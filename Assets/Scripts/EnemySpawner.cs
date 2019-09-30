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
    private Vector3 randomPos;



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

        

    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnTime)
        {
            SpawnEnemy(1);
            timeSinceLastSpawn -= timeSinceLastSpawn;
        }
        if (gameHandler.fireLife < 5)
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
            //Debug.Log("Enemy spawned with timer: " + spawnTime);
            randomPos = transform.position + new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
            GameObject enemy = Instantiate(unitPrefab, randomPos, Quaternion.identity) as GameObject;
            enemy.transform.SetParent(GameObject.Find("Level").transform);
        }
    }
}
