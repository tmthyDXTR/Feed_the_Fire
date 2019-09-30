using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Window_ResourceBank : MonoBehaviour
{

    public GameStats gameStats;
    GameHandler gameHandler;


    void Start()
    {
        gameStats = GameObject.Find("Game").GetComponent<GameStats>();
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();


        //-- Test Stock Init for Job Debugging --//
        UpdateWoodResourceTextObject();
        UpdateHousingTextObject();
        UpdateFireLifeTextObject();
        UpdateFoodStockTextObject();
        UpdateSporesStockTextObject();



        gameHandler.OnFireLifeChanged += delegate (object sender, EventArgs e)
        {
            UpdateFireLifeTextObject();
        };

        gameHandler.OnWoodStockChanged += delegate (object sender, EventArgs e)
        {
            UpdateWoodResourceTextObject();
        };

        gameHandler.OnHousingChanged += delegate (object sender, EventArgs e)
        {
            UpdateHousingTextObject();
        };

        gameHandler.OnFoodStockChanged += delegate (object sender, EventArgs e)
        {
            UpdateFoodStockTextObject();
        };

        gameHandler.OnSporesStockChanged += delegate (object sender, EventArgs e)
        {
            UpdateSporesStockTextObject();
        };

    }
    private void UpdateFireLifeTextObject()
    {
        transform.GetChild(0).Find("FireLife").GetComponent<Text>().text =
            "Fire Life: " + gameStats.fireLife + " / " + gameStats.fireLifeFull;
    }

    private void UpdateWoodResourceTextObject()
    {
        transform.GetChild(0).Find("WoodStock").GetComponent<Text>().text =
            "Wood: " + gameStats.woodStock;
    }

    private void UpdateHousingTextObject()
    {
        transform.GetChild(0).Find("Housing").GetComponent<Text>().text =
            "House: " + gameStats.housingCurrent + " / " + gameStats.housingMax;
    }

    private void UpdateFoodStockTextObject()
    {
        transform.GetChild(0).Find("FoodStock").GetComponent<Text>().text =
            "Food: " + gameStats.foodStock;
    }

    private void UpdateSporesStockTextObject()
    {
        transform.GetChild(0).Find("SporesStock").GetComponent<Text>().text =
            "Spores: " + gameStats.sporesStock;
    }


    

    private void OnDestroy()
    {
        Debug.Log("window dead");
        gameHandler.OnFireLifeChanged -= delegate (object sender, EventArgs e)
        {
            UpdateFireLifeTextObject();
        };
        gameHandler.OnWoodStockChanged -= delegate (object sender, EventArgs e)
        {
            UpdateWoodResourceTextObject();
        };

        gameHandler.OnHousingChanged -= delegate (object sender, EventArgs e)
        {
            UpdateHousingTextObject();
        };

        gameHandler.OnFoodStockChanged -= delegate (object sender, EventArgs e)
        {
            UpdateFoodStockTextObject();
        };

        gameHandler.OnSporesStockChanged -= delegate (object sender, EventArgs e)
        {
            UpdateSporesStockTextObject();
        };

    }



    void Update()
    {
        
        //UpdateFireLifeTextObject();
    }

}
