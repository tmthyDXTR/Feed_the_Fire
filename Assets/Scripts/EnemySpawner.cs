using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject unitPrefab;
    public int amount;
    public float spawnTimer;
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
        
    }

    public void SpawnEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(unitPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
