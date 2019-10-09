using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class gameStats
{

    //public static EventHandler OnFireLifeChanged;

    //public static EventHandler OnWoodStockChanged;
    public static EventHandler OnStoneStockChanged;
    //public static EventHandler OnFoodStockChanged;
    //public static EventHandler OnSporesStockChanged;
    //public static EventHandler OnHousingChanged;
    public static EventHandler OnEnemyKilled;

    

    public static EventHandler OnWorkerKilled;

    public static EventHandler OnNewGameStarted;
    public static EventHandler OnGameStart;

    public static int fireLifeFull = 20;
    public static int fireLifeMax = 20; //-- Slider Control
    public static int fireLife = 12;
    public static int stoneStock = 0;
    public static int woodStock = 18;
    public static int foodStock = 0;
    public static int workerFoodCost = 1;
    public static int sporesStock = 0;
    public static int housingMax = 0;
    public static int housingCurrent = 0;
    public static float burnTime = 0.0f;
    public static float burnSpeed = 30f; //-- Seconds to lose 1 Fire Life
    public static int omenSpawned = 0;
    public static int enemiesKilled = 0;
    public static int workerLost = 0;

    public static void ResetGame()
    {
        fireLifeFull = 20;
        fireLifeMax = 20; //-- Slider Control
        fireLife = 12;
        stoneStock = 0;
        woodStock = 18;
        foodStock = 0;
        workerFoodCost = 1;
        sporesStock = 0;
        housingMax = 0;
        housingCurrent = 0;
        burnTime = 0.0f;
        burnSpeed = 30f; //-- Seconds to lose 1 Fire Life

        enemiesKilled = 0;
        workerLost = 0;        
        if (OnNewGameStarted != null) OnNewGameStarted(null, EventArgs.Empty);

    }
   
    public static void GameStart()
    {
        fireLife = 12;
        burnTime = 0.0f;
        Debug.Log("GameStart");
        if (OnGameStart != null) OnGameStart(null, EventArgs.Empty);

    }

    public static void AddKillCounter(int amount)
    {

        enemiesKilled += amount;
        if (OnEnemyKilled != null) OnEnemyKilled(null, EventArgs.Empty);
    }
    public static void AddWorkerKillCounter(int amount)
    {

        workerLost += amount;
        if (OnWorkerKilled != null) OnWorkerKilled(null, EventArgs.Empty);
    }






    //-- Stone --//

    public static void AddStoneToStock(int amount)
    {
        stoneStock += amount;
        if (OnStoneStockChanged != null) OnStoneStockChanged(null, EventArgs.Empty);
    }

    public static void RemoveStoneFromStock(int amount)
    {
        stoneStock -= amount;
        if (OnStoneStockChanged != null) OnStoneStockChanged(null, EventArgs.Empty);
    }

    public static int GetStoneStock()
    {
        return stoneStock;
    }

}