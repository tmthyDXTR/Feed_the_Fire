using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Window_Enemy : MonoBehaviour
{
    public EnemyInfo enemy;
    public GameObject selectedObject;

    private SelectionManager selectionManager;


    private void UpdateInfo()
    {
        UpdateText();

    }


    private void UpdateText()
    {
        if (enemy != null)
        {
            transform.Find("Info_1").GetComponent<Text>().text = enemy.name;
            transform.Find("Info_2").GetComponent<Text>().text = "Health: " + enemy.currentHealth.ToString();
        }
    }



    void Start()
    {
        selectionManager = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        //buildingInfo = selectionManager.selection[0].transform.GetComponent<BuildingInfo>();
        if (enemy == null)
        {
            if (selectionManager.selection.Count != 0)
            {
                enemy = selectionManager.selection[0].GetComponent<EnemyInfo>();
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
