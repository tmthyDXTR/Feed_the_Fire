using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Window_Tree : MonoBehaviour
{
    public TreeInfo tree;
    public GameObject selectedObject;

    private SelectionManager selectionManager;


    private void UpdateInfo()
    {
        UpdateNameText();

    }


    private void UpdateNameText()
    {
        if (tree != null)
        {
            transform.Find("Info_1").GetComponent<Text>().text = tree.name;
        }
    }



    void Start()
    {
        selectionManager = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        //buildingInfo = selectionManager.selection[0].transform.GetComponent<BuildingInfo>();
        if (tree == null)
        {
            if (selectionManager.selection.Count != 0)
            {
                tree = selectionManager.selection[0].GetComponent<TreeInfo>();
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
