using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Window_Building : MonoBehaviour
{
    public BuildingInfo buildingInfo;
    public GameObject selectedObject;

    private GameObject workerButton;

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


    void Start()
    {
        selectionManager = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        //buildingInfo = selectionManager.selection[0].transform.GetComponent<BuildingInfo>();
        if (buildingInfo == null)
        {
            if (selectionManager.selection.Count != 0)
            {
                buildingInfo = selectionManager.selection[0].GetComponent<BuildingInfo>();
                selectedObject = selectionManager.selection[0];
                if (selectedObject.tag == "ResidentialHouse")
                {
                    workerButton = this.transform.Find("WorkerButton").gameObject;
                    workerButton.SetActive(true);
                }
            }
        }
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
            transform.Find("Info_1").GetComponent<Text>().text = buildingInfo.gameObject.tag + " " + buildingInfo.originalTag;
            transform.Find("Info_2").GetComponent<Text>().text = "Wood cost " + buildingInfo.costWood;
            transform.Find("Info_3").GetComponent<Text>().text = "Building Wood " + buildingInfo.wood;
            transform.Find("Info_4").GetComponent<Text>().text = "Health " + buildingInfo.currentHealth + "/" + buildingInfo.maxHealth;
            transform.Find("Info_5").GetComponent<Text>().text = "---";

        }

    }
}
