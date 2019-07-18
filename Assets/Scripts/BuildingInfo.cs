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
            maxHealth = 15;
            costWood = 15;
            costStone = 0;
        }
        if (this.gameObject.CompareTag("WoodcutterHut"))
        {
            originalTag = this.gameObject.tag;
            maxHealth = 10;
            costWood = 10;
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
            maxHealth = 10;
            costWood = 10;
            costStone = 0;
        }


        currentHealth = 0.01f;
        //isConstruction = true;
        reqWood = costWood;
        reqStone = costStone;
        isConstruction = true;
    }

    #region Methods

    public void SetBuildingModel(int stage)    // 0-5    0 = Construction Start, 5 = Construction Complete 
    {
        if (stage == 0)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            gameObject.transform.GetChild(5).gameObject.SetActive(false);

        }
        if (stage == 1)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            gameObject.transform.GetChild(5).gameObject.SetActive(false);
        }
        if (stage == 2)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            gameObject.transform.GetChild(5).gameObject.SetActive(false);
        }
        if (stage == 3)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.transform.GetChild(3).gameObject.SetActive(true);
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            gameObject.transform.GetChild(5).gameObject.SetActive(false);
        }
        if (stage == 4)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.GetChild(4).gameObject.SetActive(true);
            gameObject.transform.GetChild(5).gameObject.SetActive(false);
        }
        if (stage == 5)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            gameObject.transform.GetChild(5).gameObject.SetActive(true);
        }
    }

    public void ConstructionComplete()
    {
        this.gameObject.tag = originalTag;
        isConstruction = false;
        if (this.gameObject.tag == "WoodcutterHut")
        {
            foreach (Transform child in transform)
            {
                if (child.name == "WoodLogs")
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
        if (this.gameObject.tag == "Bonfire")
        {
            foreach (Transform child in transform)
            {
                if (child.name == "Point Light" || child.name == "Flame" || child.name == "Revealer")
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
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

    public bool waitForRemainingResource()
    {
        if (moreResourcesNeeded() == false && (currentWood + currentStone) != (costWood + costStone))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool moreResourcesNeeded()        
    {
        if (reqWood + reqStone > 0)
        {
            return true;
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

    public void ReqResourcePlus(string resource)
    {
        if (resource == "Wood")
        {
            reqWood += 1;
        }
        if (resource == "Stone")
        {
            reqStone += 1;
        }
    }

    #endregion


    void Update()
    {
        
    }
}
