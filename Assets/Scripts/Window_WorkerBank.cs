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
            "Unemployed: " + jobManager.unemployedCount;
    }

    private void UpdateLightWardenTotalTextObject()
    {
        transform.Find("LightWarden").GetComponent<Text>().text =
            "LightWarden: " + jobManager.lighWardenCount;
    }

    private void UpdateBuilderTotalTextObject()
    {
        transform.Find("Builder").GetComponent<Text>().text =
            "Builder: " + jobManager.builderCount;
    }

    private void UpdateWoodcutterTotalTextObject()
    {
        transform.Find("Woodcutter").GetComponent<Text>().text =
            "Woodcutter: " + jobManager.woodcutterCount;
    }

    private void UpdateStonecutterTotalTextObject()
    {
        transform.Find("Stonecutter").GetComponent<Text>().text =
            "Stonecutter: " + jobManager.stonecutterCount;
    }

    public void UpdateJobsCounter()
    {
        UpdateUnemployedTotalTextObject();
        UpdateLightWardenTotalTextObject();
        UpdateBuilderTotalTextObject();
        UpdateWoodcutterTotalTextObject();
        UpdateStonecutterTotalTextObject();
    }

    private void Awake()
    {
        UpdateJobsCounter();
    }

    private void Update()
    {
        UpdateJobsCounter();
    }

}
