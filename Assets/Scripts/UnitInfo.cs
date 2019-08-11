﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    public int health = 10;
    public string job;
    public int invMax = 1;
    public int invWood = 0;
    public bool isAlive = true;

    private WorkerUnitAI unitAI;
    private EnemyRoam attacker; // Temp Fix

    void Awake()
    {
        unitAI = GetComponent<WorkerUnitAI>();
        isAlive = true;
    }

    void Update()
    {
        
    }

    public void TakeDamage(GameObject hitter, int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            attacker = hitter.GetComponent<EnemyRoam>(); // Temp Fix, Get other Attack Script
            attacker.DeRegisterTarget(this.GetComponent<Collider>());
            Death();
        }
    }

    void Death()
    {      
        isAlive = false;
        Destroy(this.gameObject);        
    }
}
