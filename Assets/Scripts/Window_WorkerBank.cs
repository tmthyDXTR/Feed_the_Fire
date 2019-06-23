using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Window_WorkerBank : MonoBehaviour
{
    public JobManager jobManager;

    private void UpdateUnemployedTotalTextObject()
    {
        transform.Find("Unemployed").GetComponent<Text>().text =
            "Unemployed: " + jobManager.GetWorkerCount("Unemployed");
    }

    private void UpdateLightWardenTotalTextObject()
    {
        transform.Find("LightWarden").GetComponent<Text>().text =
            "LightWarden: " + jobManager.GetWorkerCount("LightWarden");
    }

    private void UpdateBuilderTotalTextObject()
    {
        transform.Find("Builder").GetComponent<Text>().text =
            "Builder: " + jobManager.GetWorkerCount("Builder");
    }

    private void UpdateWoodcutterTotalTextObject()
    {
        transform.Find("Woodcutter").GetComponent<Text>().text =
            "Woodcutter: " + jobManager.GetWorkerCount("Woodcutter");
    }

    private void UpdateStonecutterTotalTextObject()
    {
        transform.Find("Stonecutter").GetComponent<Text>().text =
            "Stonecutter: " + jobManager.GetWorkerCount("Stonecutter");
    }


    private void Awake()
    {

    }

    private void Update()
    {
        UpdateUnemployedTotalTextObject();
        UpdateLightWardenTotalTextObject();
        UpdateBuilderTotalTextObject();
        UpdateWoodcutterTotalTextObject();
        UpdateStonecutterTotalTextObject();
    }

}
