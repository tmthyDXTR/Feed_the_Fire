using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Window_Construction : MonoBehaviour
{
    [SerializeField] private BuildingInfo buildingInfo;
    public GameObject selectedObject;
    public Selection selection;

    private void UpdateInfo()
    {
        UpdateBuildingName();
        UpdateProgress();
        UpdateCostWood();
        UpdateCostStone();

    }

    private void UpdateBuildingName()
    {
        buildingInfo = selectedObject.GetComponent<BuildingInfo>();
        transform.Find("BuildingName").GetComponent<Text>().text = selectedObject.tag + "-" + buildingInfo.originalTag;
    }

    private void UpdateProgress()
    {
        buildingInfo = selectedObject.GetComponent<BuildingInfo>();
        transform.Find("Progress").GetComponent<Text>().text =
            "Progress: " + (int)buildingInfo.currentHealth + "/" + buildingInfo.maxHealth;
    }

    private void UpdateCostWood()
    {
        buildingInfo = selectedObject.GetComponent<BuildingInfo>();
        transform.Find("CostWood").GetComponent<Text>().text =
            "Cost Wood: " + buildingInfo.costWood;
    }

    private void UpdateCostStone()
    {
        buildingInfo = selectedObject.GetComponent<BuildingInfo>();
        transform.Find("CostStone").GetComponent<Text>().text =
            "Cost Stone: " + buildingInfo.costStone;
    }


    void Awake()
    {
        if (gameObject.activeSelf == true)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (gameObject.activeSelf == true)
        {
            if (selection.selectedObjects[0] != null)
            {
                selectedObject = selection.selectedObjects[0];
                Debug.Log("Update Construction Info");
                UpdateInfo();
                if (selectedObject.tag != "Construction")
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                selectedObject = null;
            }
        }
    }
}
