using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Window_Building : MonoBehaviour
{
    public BuildingInfo buildingInfo;
    public GameObject selectedObject;

    private SelectionManager selectionManager;

    private Building building;
    private enum Building
    {
        WoodcutterHut,
        ResidentialHouse,
        Tavern,
        Bonfire,
        WoodLogs,
    }


    void Awake()
    {
        selectionManager = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
    }

    void Update()
    {      
        UpdateBuildingName();       
    }


    private void UpdateBuildingWindow()
    {
        UpdateBuildingName();
    }

    private void UpdateBuildingName()
    {
        if (buildingInfo != null)
        {
            transform.Find("Info_1").GetComponent<Text>().text = buildingInfo.gameObject.tag;
        }

    }
}
