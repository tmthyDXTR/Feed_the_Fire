using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Window_Worker : MonoBehaviour
{
    public UnitInfo unitInfo;
    public GameObject selectedObject;

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
        transform.Find("Info_2").GetComponent<Text>().text =
            "Occupation: " + unitInfo.job;
    }

    private void UpdateTargetText()
    {
        if (unitInfo.target != null)
        {
            transform.Find("Info_3").GetComponent<Text>().text =
            "Target: " + unitInfo.target.gameObject.name;
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



    void Awake()
    {        

    }

    void Update()
    {
        if (selectedObject != null)
        {                    
            UpdateInfo();
        }                                        
    }
}
