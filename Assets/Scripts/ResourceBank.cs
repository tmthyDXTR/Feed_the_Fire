using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ResourceBank
{

    public static EventHandler OnWoodStockChanged;
    public static EventHandler OnStoneStockChanged;
    private static int stoneStock;
    private static int woodStock;


    public static void AddWoodToStock(int amount)
    {
        woodStock += amount;
        if (OnWoodStockChanged != null) OnWoodStockChanged(null, EventArgs.Empty);
    }

    public static int GetWoodStock()
    {
        return woodStock;
    }

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