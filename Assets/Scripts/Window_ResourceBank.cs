using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Window_ResourceBank : MonoBehaviour
{
 


    private void UpdateFireLifeTextObject()
    {
        transform.Find("FireLife").GetComponent<Text>().text =
            "Fire Life: " + ResourceBank.GetFireLife() + " / " + ResourceBank.fireLifeFull;
    }

    private void UpdateWoodResourceTextObject()
    {
        transform.Find("WoodStock").GetComponent<Text>().text =
            "Wood: " + ResourceBank.GetWoodStock();
    }

    private void UpdateStoneResourceTextObject()
    {
        transform.Find("StoneStock").GetComponent<Text>().text =
            "Stone: " + ResourceBank.GetStoneStock();
    }

    private void UpdateFoodStockTextObject()
    {
        transform.Find("FoodStock").GetComponent<Text>().text =
            "Food: " + ResourceBank.foodStock;
    }

    private void UpdateSporesStockTextObject()
    {
        transform.Find("SporesStock").GetComponent<Text>().text =
            "Spores: " + ResourceBank.sporesStock;
    }


    private void Awake()
    {

        //-- Test Stock Init for Job Debugging --//
        UpdateWoodResourceTextObject();
        UpdateStoneResourceTextObject();
        UpdateFireLifeTextObject();
        UpdateFoodStockTextObject();
        UpdateSporesStockTextObject();



        ResourceBank.OnFireLifeChanged -= delegate (object sender, EventArgs e)
        {            
            UpdateFireLifeTextObject();
        };

        ResourceBank.OnWoodStockChanged += delegate (object sender, EventArgs e)
        {
            UpdateWoodResourceTextObject();
        };

        ResourceBank.OnStoneStockChanged += delegate (object sender, EventArgs e)
        {
            UpdateStoneResourceTextObject();
        };

        ResourceBank.OnFoodStockChanged += delegate (object sender, EventArgs e)
        {
            UpdateFoodStockTextObject();
        };

        ResourceBank.OnSporesStockChanged += delegate (object sender, EventArgs e)
        {
            UpdateSporesStockTextObject();
        };

    }

        

    void Update()
    {
        ResourceBank.FireBurner();
        UpdateFireLifeTextObject();
    }

}
