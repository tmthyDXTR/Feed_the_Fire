using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Window_Hero : MonoBehaviour
{
    public HeroInfo hero;
    private HeroController heroController;
    public GameObject selectedObject;
    private Button consumeFireButton;
    private GameStats gameStats;

    private SelectionManager selectionManager;


    private void UpdateInfo()
    {
        UpdateText();

    }


    private void UpdateText()
    {
        if (hero != null)
        {
            transform.Find("Info_2").GetComponent<Text>().text = hero.power.ToString();
            transform.Find("Info_3").GetComponent<Text>().text = "Health: " + hero.currentHealth.ToString() + " / " + hero.health.ToString();
            transform.Find("Info_4").GetComponent<Text>().text = "1 DmgAdd: " + "*" + heroController.PowerMultiplicator().ToString() + " + " + hero.currentHealth;

        }
    }



    void Start()
    {
        gameStats = GameObject.Find("Game").GetComponent<GameStats>();
        heroController = GameObject.Find("Hero").GetComponent<HeroController>();
        consumeFireButton = transform.Find("ConsumeButton").GetComponent<Button>();

        selectionManager = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        //buildingInfo = selectionManager.selection[0].transform.GetComponent<BuildingInfo>();
        if (hero == null)
        {
            if (selectionManager.selection.Count != 0)
            {
                hero = selectionManager.selection[0].GetComponent<HeroInfo>();
                selectedObject = selectionManager.selection[0];
            }
        }
    }

    void Update()
    {
        if (selectedObject != null)
        {                    
            UpdateInfo();

            if (heroController.canConsumeFire)
            {
                consumeFireButton.interactable = true;
            }
            else
            {
                consumeFireButton.interactable = false;
            }            
        }                                        
    }
}
