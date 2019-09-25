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

    private void UpdateHousingTextObject()
    {
        transform.Find("Housing").GetComponent<Text>().text =
            "House: " + ResourceBank.housingCurrent + " / " + ResourceBank.housingMax;
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
        UpdateHousingTextObject();
        UpdateFireLifeTextObject();
        UpdateFoodStockTextObject();
        UpdateSporesStockTextObject();



        ResourceBank.OnFireLifeChanged += delegate (object sender, EventArgs e)
        {            
            UpdateFireLifeTextObject();
        };

        ResourceBank.OnWoodStockChanged += delegate (object sender, EventArgs e)
        {
            UpdateWoodResourceTextObject();
        };

        ResourceBank.OnHousingChanged += delegate (object sender, EventArgs e)
        {
            UpdateHousingTextObject();
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
        //UpdateFireLifeTextObject();
    }

}
