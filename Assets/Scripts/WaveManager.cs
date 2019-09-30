using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private float waveTime = 300;
    [SerializeField] private float timeSinceLastWave = 0.0f;
    [SerializeField] private int waveEnemyAmount = 1;

    private Transform enemySpawners;
    public List<GameObject> spawnerList = new List<GameObject>();
    void Awake()
    {
        enemySpawners = GameObject.Find("EnemySpawners").transform;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastWave += Time.deltaTime;
        if (timeSinceLastWave >= waveTime)
        {
            StartWave();
            timeSinceLastWave -= timeSinceLastWave;
            waveEnemyAmount += 1;
        }
    }

    private void StartWave()
    {
        foreach (Transform child in enemySpawners)
        {
            if (child.GetComponent<EnemySpawner>() != null)
            {
                child.GetComponent<EnemySpawner>().SpawnEnemy(waveEnemyAmount);
            }
        }
    }
}
