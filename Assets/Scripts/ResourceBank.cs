using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ResourceBank
{

    public static EventHandler OnFireLifeChanged;
    public static EventHandler OnWoodStockChanged;
    public static EventHandler OnStoneStockChanged;
    public static EventHandler OnFoodStockChanged;
    public static EventHandler OnSporesStockChanged;
    public static EventHandler OnHousingChanged;
    public static int fireLifeFull = 20;
    public static int fireLifeMax = 20; //-- Slider Control
    public static int fireLife = 8;
    public static int stoneStock = 0;
    public static int woodStock = 18;
    public static int foodStock = 0;
    public static int sporesStock = 0;
    public static int housingNeeded = 0;
    public static int housingCurrent = 0;
    public static float burnTime = 0.0f;
    public static float burnSpeed = 30f; //-- Seconds to lose 1 Fire Life

    //-- Fire --//

    public static void FireBurner()
    {
        if (fireLife >= 15)
        {
            //Debug.Log("Fire Burning");
            burnTime += Time.deltaTime;
            if (burnTime >= burnSpeed)
            {               
                RemoveFireLife(1);
                Debug.Log("1 Fire Life lost - Burn Time: " + (int)burnTime);
                burnTime -= (int)burnTime;
            }
        }
        else if (fireLife >= 10 && fireLife < 15)
        {
            //Debug.Log("Fire Burning");
            burnTime += Time.deltaTime;
            if (burnTime >= burnSpeed * 1.22f)
            {
                RemoveFireLife(1);
                Debug.Log("1 Fire Life lost - Burn Time: " + (int)burnTime);
                burnTime -= (int)burnTime;
            }
        }
        else if (fireLife >= 5 && fireLife < 10)
        {
            //Debug.Log("Fire Burning");
            burnTime += Time.deltaTime;
            if (burnTime >= burnSpeed * 1.4f)
            {
                RemoveFireLife(1);
                Debug.Log("1 Fire Life lost - Burn Time: " + (int)burnTime);
                burnTime -= (int)burnTime;
            }
        }
        else if (fireLife > 0 && fireLife < 5)
        {
            //Debug.Log("Fire Burning");
            burnTime += Time.deltaTime;
            if (burnTime >= burnSpeed * 1.6f)
            {
                RemoveFireLife(1);
                Debug.Log("1 Fire Life lost - Burn Time: " + (int)burnTime);
                burnTime -= (int)burnTime;
            }
        }
    }

    public static void RemoveFireLife(int amount)
    {
        if (fireLife >= amount)
        {
            fireLife -= amount;
            if (OnFireLifeChanged != null) OnFireLifeChanged(null, EventArgs.Empty);
        }        
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

    //-- Food --//

    public static void AddFoodToStock(int amount)
    {
        foodStock += amount;
        if (OnFoodStockChanged != null) OnFoodStockChanged(null, EventArgs.Empty);
    }

    public static void RemoveFoodFromStock(int amount)
    {
        foodStock -= amount;
        if (OnFoodStockChanged != null) OnFoodStockChanged(null, EventArgs.Empty);
    }


    public static void AddSporesToStock(int amount)
    {
        sporesStock += amount;
        if (OnSporesStockChanged != null) OnSporesStockChanged(null, EventArgs.Empty);
    }

    public static void RemoveSporesFromStock(int amount)
    {
        sporesStock -= amount;
        if (OnSporesStockChanged != null) OnSporesStockChanged(null, EventArgs.Empty);
    }




    //-- Housing --//

    public static void AddHousing(int amount)
    {
        housingCurrent += amount;
        if (OnHousingChanged != null) OnHousingChanged(null, EventArgs.Empty);
    }

    public static void RemoveHousing(int amount)
    {
        housingCurrent -= amount;
        if (OnHousingChanged != null) OnHousingChanged(null, EventArgs.Empty);
    }

    public static int GetHousing()
    {
        return housingCurrent;
    }

    public static int GetHousingNeeded()
    {
        //foreach (Transform child in transform)
        //{
        //    Something(child.gameObject);
        //}

        return housingCurrent;
    }

}