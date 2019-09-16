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

    private SelectionManager selectionManager;


    private void UpdateInfo()
    {
        UpdatePowerText();

    }


    private void UpdatePowerText()
    {
        if (hero != null)
        {
            transform.Find("Info_2").GetComponent<Text>().text =
            "Power: " + hero.firePower;
        }
    }



    void Start()
    {
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

            if (consumeFireButton.interactable == true)
            {
                if (heroController.canConsumeFire == false)
                {
                    consumeFireButton.interactable = false;
                }
            }
            if (consumeFireButton.interactable == false)
            {
                if (heroController.canConsumeFire == true)
                {
                    consumeFireButton.interactable = true;
                }
            }
        }                                        
    }
}
