using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildingInfo : MonoBehaviour
{
    public int maxHealth;
    public float currentHealth;
    public int costWood;
    public int costStone;
    public int reqWood;
    public int reqStone;
    public int currentWood;
    public int currentStone;
    public string originalTag;
    public bool isConstruction;
    bool isDead;
    CapsuleCollider capsuleCollider;


    void Awake()
    {

        // Create According Building Stats
        if (this.gameObject.CompareTag("ResidentialHouse"))
        {
            originalTag = this.gameObject.tag;
            maxHealth = 20;
            costWood = 10;
            costStone = 10;
        }
        if (this.gameObject.CompareTag("Storage"))
        {
            originalTag = this.gameObject.tag;
            maxHealth = 15;
            costWood = 15;
            costStone = 0;
        }
        if (this.gameObject.CompareTag("Tavern"))
        {
            originalTag = this.gameObject.tag;
            maxHealth = 30;
            costWood = 15;
            costStone = 15;
        }

        if (this.gameObject.CompareTag("Bonfire"))
        {
            originalTag = this.gameObject.tag;
            maxHealth = 15;
            costWood = 15;
            costStone = 0;
        }


        currentHealth = 0.01f;
        isConstruction = true;
        reqWood = costWood;
        reqStone = costStone;
    }

    #region Methods

    public void ConstructionComplete()
    {
        this.gameObject.tag = originalTag;
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
            currentHealth += (maxHealth / (costWood + costStone) * 1f);  //-- Construction Speed 
        }
        if ((int)currentHealth >= (int)maxHealth)
        {
            currentHealth = maxHealth;
        }

        // If the current health is less than or equal to zero...
        if (currentHealth <= 0)
        {
            // ... the building is dead.
            Death();
        }
    }

    void Death()
    {
        // The building is dead.
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

    #endregion

   
    void Update()
    {
        
    }
}
