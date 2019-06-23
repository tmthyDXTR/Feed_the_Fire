using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ResourceBank
{

    public static EventHandler OnFireLifeChanged;
    public static EventHandler OnWoodStockChanged;
    public static EventHandler OnStoneStockChanged;
    private static int fireLifeMax = 20;
    private static int fireLife = 15;
    private static int stoneStock = 20;
    private static int woodStock = 20;
    private static float burnTime = 0.0f;
    private static float burnSpeed = 10f; //-- Fire Life per Second Lost

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