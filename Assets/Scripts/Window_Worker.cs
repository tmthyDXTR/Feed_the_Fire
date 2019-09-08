using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Window_Worker : MonoBehaviour
{
    public UnitInfo unitInfo;
    public GameObject selectedObject;

    private SelectionManager selectionManager;


    private void UpdateInfo()
    {
        UpdateUnitNameText();
        UpdateOccupationText();
        UpdateTargetText();
        UpdateInvWood();
        UpdateInvStone();
    }

    private void UpdateUnitNameText()
    {
        transform.Find("Info_1").GetComponent<Text>().text = selectedObject.tag;
    }

    private void UpdateOccupationText()
    {
        transform.Find("Info_2").GetComponent<Text>().text = unitInfo.job;
    }

    private void UpdateTargetText()
    {
        if (unitInfo.target != null)
        {
            transform.Find("Info_3").GetComponent<Text>().text =
            "Going to " + unitInfo.target.gameObject.name;
        }        
    }

    private void UpdateInvWood()
    {
        transform.Find("Info_4").GetComponent<Text>().text =
            "InvWood: " + unitInfo.invWood;
    }

    private void UpdateInvStone()
    {
        transform.Find("Info_5").GetComponent<Text>().text =
            "InvStone: " + unitInfo.invStone;
    }



    void Start()
    {
        selectionManager = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        //buildingInfo = selectionManager.selection[0].transform.GetComponent<BuildingInfo>();
        if (unitInfo == null)
        {
            if (selectionManager.selection.Count != 0)
            {
                unitInfo = selectionManager.selection[0].GetComponent<UnitInfo>();
                selectedObject = selectionManager.selection[0];
            }
        }
    }

    void Update()
    {
        if (selectedObject != null)
        {                    
            UpdateInfo();
        }                                        
    }
}
