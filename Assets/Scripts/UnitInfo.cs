using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    public int health = 10;
    public bool isAlive = true;

    private EnemyRoam attacker;

    void Awake()
    {
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
