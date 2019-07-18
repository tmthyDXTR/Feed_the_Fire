using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Window_Worker : MonoBehaviour
{
    [SerializeField] private WorkerAI workerAI;
    public GameObject selectedObject;
    public Selection selection;

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
        workerAI = selectedObject.GetComponent<WorkerAI>();
        transform.Find("UnitName").GetComponent<Text>().text = selectedObject.tag;
    }

    private void UpdateOccupationText()
    {
        workerAI = selectedObject.GetComponent<WorkerAI>();
        transform.Find("Occupation").GetComponent<Text>().text =
            "Occupation: " + workerAI.currentJob;
    }

    private void UpdateTargetText()
    {
        workerAI = selectedObject.GetComponent<WorkerAI>();
        transform.Find("Target").GetComponent<Text>().text =
            "Target: " + workerAI.Target;
    }

    private void UpdateInvWood()
    {
        workerAI = selectedObject.GetComponent<WorkerAI>();
        transform.Find("InvWood").GetComponent<Text>().text =
            "InvWood: " + workerAI.inventoryWood;
    }

    private void UpdateInvStone()
    {
        workerAI = selectedObject.GetComponent<WorkerAI>();
        transform.Find("InvStone").GetComponent<Text>().text =
            "InvStone: " + workerAI.inventoryStone;
    }



    void Awake()
    {
        workerAI = gameObject.GetComponent<WorkerAI>();

        if (gameObject.activeSelf == true)
        {

            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (gameObject.activeSelf == true)
        {
            
            if (selection.selectedObjects[0] != null && selection.selectedObjects[0].tag == "Worker")
            {
                //selection.selectedObjects.Clear();
                selectedObject = selection.selectedObjects[selection.selectedObjects.Count - 1];
                Debug.Log("Update Worker Info");
                UpdateInfo();
            }                      

            else
            {
                selectedObject = null;
            }
        }
    }
}
