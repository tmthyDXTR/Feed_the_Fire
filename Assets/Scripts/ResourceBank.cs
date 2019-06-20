using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ResourceBank
{

    public static EventHandler OnFireLifeChanged;
    public static EventHandler OnWoodStockChanged;
    public static EventHandler OnStoneStockChanged;
    private static int fireLife = 100;
    private static int stoneStock;
    private static int woodStock;

    //-- Wood --//

    public static int GetFireLife()
    {
        return fireLife;
    }

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

    public static void AddWoodToFire(int amount)
    {
        fireLife += amount;
        if (OnFireLifeChanged != null) OnFireLifeChanged(null, EventArgs.Empty);
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

    public static int GetStoneStock()
    {
        return stoneStock;
    }

   
}