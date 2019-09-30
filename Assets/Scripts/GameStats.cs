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

    void Awake()
    {
        burnTime = gameHandler.burnTime;
        burnSpeed = gameHandler.burnSpeed;
        fireLife = gameHandler.fireLife;
        fireLifeFull = gameHandler.fireLifeFull;
        woodStock = gameHandler.woodStock;
        foodStock = gameHandler.foodStock;
        sporesStock = gameHandler.sporesStock;
        housingCurrent = gameHandler.housingCurrent;
        housingMax = gameHandler.housingMax;
        fireLifeMax = gameHandler.fireLifeMax;
        workerFoodCost = gameHandler.workerFoodCost;

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
