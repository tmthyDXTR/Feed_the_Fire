using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoneNode : MonoBehaviour
{
    public int stoneAmount = 24;
    public int currentAmount;
    bool isDead;
    BoxCollider boxCollider;

    void Awake()
    {
        // Setting the current health when the enemy first spawns.
        currentAmount = stoneAmount;
        boxCollider = GetComponent<BoxCollider>();
    }


    void Update()
    {
        
    }

    private void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }

    public void TakeDamage()
    {
        // If the enemy is dead...
        if (isDead)
            // ... no need to take damage so exit the function.
            return;

        // Reduce the current health by the amount of damage sustained.
        currentAmount -= 1;

        // If the current health is less than or equal to zero...
        if (currentAmount <= 0)
        {
            // ... the enemy is dead.
            Death();
        }
    }

    void Death()
    {        
        isDead = true;
        Destroy(gameObject);
    }
}