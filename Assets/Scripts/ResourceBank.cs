﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ResourceBank
{

    public static EventHandler OnFireLifeChanged;
    public static EventHandler OnWoodStockChanged;
    public static EventHandler OnStoneStockChanged;
    public static int fireLifeMax = 20;
    public static int fireLife = 20;
    public static int stoneStock = 40;
    public static int woodStock = 40;
    public static float burnTime = 0.0f;
    public static float burnSpeed = 14f; //-- Seconds to lose 1 Fire Life

    //-- Fire --//

    public static void FireBurner()
    {
        if (fireLife > 0)
        {
            Debug.Log("Fire Burning");
            burnTime += Time.deltaTime;
            if (burnTime >= burnSpeed)
            {               
                RemoveFireLife();
                burnTime -= (int)burnTime;
                Debug.Log("1 Fire Life lost");
            }
        }
    }

    private static void RemoveFireLife()
    {
        fireLife -= 1;
        if (OnFireLifeChanged != null) OnFireLifeChanged(null, EventArgs.Empty);
    }

    public static int GetFireLife()
    {
        return fireLife;
    }

    public static int GetFireLifeMax()
    {
        return fireLifeMax;
    }

    public static void AddWoodToFire(int amount)
    {
        fireLife += amount;
        if (OnFireLifeChanged != null) OnFireLifeChanged(null, EventArgs.Empty);
    }


    //-- Wood --//

    public static void AddWoodToStock(int amount)
    {
        woodStock += amount;
        if (OnWoodStockChanged != null) OnWoodStockChanged(null, EventArgs.Empty);
    }

    public static void RemoveWoodFromStock(int amount)
    {
        woodStock -= amount;
        if (OnWoodStockChanged != null) OnWoodStockChanged(null, EventArgs.Empty);
    }    

    public static int GetWoodStock()
    {
        return woodStock;
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