using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildingInfo : MonoBehaviour
{
    public int maxHealth = 10;
    public float currentHealth;
    public int costWood = 5;
    public int costStone = 10;
    public int reqWood;
    public int reqStone;
    public int currentWood;
    public int currentStone;
    public bool isConstruction;
    bool isDead;
    CapsuleCollider capsuleCollider;


    void Awake()
    {
        currentHealth = 0.01f;
        isConstruction = true;
        reqWood = costWood;
        reqStone = costStone;
    }


    public void GainHealth()
    {
        // If the building is dead...
        if (isDead)
            // ... no need to gain health so exit the function.
            return;

        // Reduce the current health by the amount of damage sustained.
        if ((int)currentHealth < (int)maxHealth)
        {
            currentHealth += (maxHealth / (costWood + costStone) * 1f);
        }
        if ((int)currentHealth >= (int)maxHealth)
        {
            currentHealth = maxHealth;
        }

        // If the current health is less than or equal to zero...
        if (currentHealth <= 0)
        {
            // ... the enemy is dead.
            Death();
        }
    }

    void Death()
    {
        // The enemy is dead.
        isDead = true;

        Destroy(gameObject);
    }

    public bool moreResourcesNeeded()        
    {
        if (reqWood + reqStone > 0)
        {
            return true;
        }

        if (reqWood + reqStone == 0)
        {
            return false;
        }
        else
        {
            return false;
        }         
    }

    public void CurrentResourcePlus(string resource)
    {
        if (resource == "Wood")
        {
            currentWood += 1;
        }
        if (resource == "Stone")
        {
            currentStone += 1;
        }
    }

    public int GetCurrentResource(string resource)
    {
        if (resource == "Wood")
        {
            return currentWood;
        }
        if (resource == "Stone")
        {
            return currentStone;
        }
        else
        {
            return 0;
        }
    }

    public int GetReqResource(string resource)
    {
        if (resource == "Wood")
        {
            return reqWood;
        }
        if (resource == "Stone")
        {
            return reqStone;
        }
        else
        {
            return 0;
        }
    }

    public void ReqResourceMinus(string resource)
    {
        if (resource == "Wood")
        {
            reqWood -= 1;
        }
        if (resource == "Stone")
        {
            reqStone -= 1;
        }
    }   

    // Update is called once per frame
    void Update()
    {
        
    }
}
