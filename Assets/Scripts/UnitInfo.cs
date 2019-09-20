using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    public float health;
    public float currentHealth;
    public string job;
    public int invMax = 1;
    public int invWood = 0;
    public int invStone = 0;
    public int invShroom = 0;
    public int invSpores = 0;
    public int invFire = 0;
    public bool isDead = false;
    public Collider target;


    private WorkerUnitAI unitAI;
    private EnemyRoam attacker; // Temp Fix

    void Awake()
    {
        unitAI = GetComponent<WorkerUnitAI>();
    }

    void Update()
    {

    }

    
}
