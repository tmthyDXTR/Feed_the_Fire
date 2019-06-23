﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TreeNode : MonoBehaviour
{
    public int woodAmount = 24;
    public int currentAmount;
    bool isDead;
    CapsuleCollider capsuleCollider;

    void Awake()
    {
    // Setting the current health when the enemy first spawns.
    currentAmount = woodAmount;
    capsuleCollider = GetComponent<CapsuleCollider>();
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