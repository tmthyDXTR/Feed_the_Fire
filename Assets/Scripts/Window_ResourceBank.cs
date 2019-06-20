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
            "Fire Life: " + ResourceBank.GetFireLife();
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
    

    private void Awake()
    {
        UpdateFireLifeTextObject();

        ResourceBank.OnFireLifeChanged += delegate (object sender, EventArgs e)
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
    }

    

}
