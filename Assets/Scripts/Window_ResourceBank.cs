using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Window_ResourceBank : MonoBehaviour
{

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
