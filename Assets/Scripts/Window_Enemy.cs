using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Window_Enemy : MonoBehaviour
{
    public UnitInfo unitInfo;
    public GameObject selectedObject;

    private SelectionManager selectionManager;


    private void UpdateInfo()
    {
        //UpdateTargetText();

    }


    //private void UpdateTargetText()
    //{
    //    if (unitInfo.target != null)
    //    {
    //        transform.Find("Info_3").GetComponent<Text>().text =
    //        "Going to " + unitInfo.target.gameObject.name;
    //    }        
    //}



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
