using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public int fireLife;
    public int fireLifeFull;
    public int fireLifeMax;
    public float burnTime;
    public float burnSpeed;
    public int woodStock;
    public int foodStock;
    public int sporesStock;
    public int housingCurrent;
    public int housingMax;
    public int workerFoodCost;
    public int omenSpawned = 0;


    void Awake()
    {
        burnTime = gameStats.burnTime;
        burnSpeed = gameStats.burnSpeed;
        fireLife = gameStats.fireLife;
        fireLifeFull = gameStats.fireLifeFull;
        woodStock = gameStats.woodStock;
        foodStock = gameStats.foodStock;
        sporesStock = gameStats.sporesStock;
        housingCurrent = gameStats.housingCurrent;
        housingMax = gameStats.housingMax;
        fireLifeMax = gameStats.fireLifeMax;
        workerFoodCost = gameStats.workerFoodCost;
        omenSpawned = gameStats.omenSpawned;

        Debug.Log("ResourceBank -> Game Stats Initialized");
    }

    void Update()
    {
        //burnTime = ResourceBank.burnTime;
        //burnSpeed = ResourceBank.burnSpeed;
        //fireLife = ResourceBank.fireLife;
        //fireLifeFull = ResourceBank.fireLifeFull;
        //woodStock = ResourceBank.woodStock;
        //foodStock = ResourceBank.foodStock;
        //sporesStock = ResourceBank.sporesStock;
        //housingCurrent = ResourceBank.housingCurrent;
        //housingMax = ResourceBank.housingMax;
        //fireLifeMax = ResourceBank.fireLifeMax;

    }
}
