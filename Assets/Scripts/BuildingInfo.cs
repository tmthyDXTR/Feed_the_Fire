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
    public int wood;
    public int stone;
    public string originalTag;
    public bool isConstruction;
    bool isDead;
    CapsuleCollider capsuleCollider;
    ConstructionManager construction;

    GameHandler gameHandler;


    void Awake()
    {
        construction = GameObject.Find("ConstructionManager").GetComponent<ConstructionManager>();
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();

        // Create According Building Stats
        if (this.gameObject.CompareTag("ResidentialHouse"))
        {
            originalTag = this.gameObject.tag;
            maxHealth = 10;
            costWood = 10;
            costStone = 0;
        }
        if (this.gameObject.CompareTag("WoodcutterHut"))
        {
            originalTag = this.gameObject.tag;
            maxHealth = 10;
            costWood = 8;
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
            maxHealth = 5;
            costWood = 5;
            costStone = 0;
        }


        currentHealth = 0.01f;
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
        //this.gameObject.tag = originalTag;
        isConstruction = false;
        construction.DeregisterConstruction(this.gameObject);
        if (originalTag == "WoodcutterHut")
        {
            this.gameObject.tag = originalTag;
            foreach (Transform child in transform)
            {
                if (child.name == "WoodLogs")
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
        if (originalTag == "ResidentialHouse")
        {
            this.gameObject.tag = originalTag;
            gameHandler.AddHousing(2);
        }

        if (originalTag == "Bonfire")
        {
            this.gameObject.tag = "UnlitBonfire";
            BonfireManager bonfire = GameObject.Find("BonfireManager").GetComponent<BonfireManager>();
            bonfire.AddBonfire(this.gameObject);


        }
    }

    public void GainHealth()
    {
        // If the building is dead...
        if (isDead)
            // ... no need to gain health so exit the function.
            return;

        // Increase the current health by the amount of Health gained.
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

    #endregion


    void Update()
    {
        
    }
}
