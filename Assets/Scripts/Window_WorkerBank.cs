using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Window_WorkerBank : MonoBehaviour
{
    public JobManager jobManager;
    GameHandler gameHandler;

    void Start()
    {
        jobManager = GameObject.Find("Workers").GetComponent<JobManager>();
        UpdateJobsCounter();

        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        jobManager.OnJobChanged += delegate (object sender, EventArgs e)
        {
            //Debug.Log("OnJobChanged event");
            UpdateJobsCounter();
        };
        gameHandler.OnHousingChanged += delegate (object sender, EventArgs e)
        {
            //Debug.Log("OnHousingChanged event");
            UpdateJobsCounter();
        };
        
    }

    private void OnDestroy()
    {
        jobManager.OnJobChanged -= delegate (object sender, EventArgs e)
        {
            Debug.Log("OnJobChanged event");
            UpdateJobsCounter();
        };
        gameHandler.OnHousingChanged -= delegate (object sender, EventArgs e)
        {
            Debug.Log("OnJobChanged event");
            UpdateJobsCounter();
        };
    }

    private void UpdateUnemployedTotalTextObject()
    {
        transform.Find("Unemployed").GetComponent<TextMeshPro>().text =
            "Unemployed: " + jobManager.unemployedCount.ToString();
    }

    private void UpdateLightWardenTotalTextObject()
    {
        transform.Find("LightWarden").GetComponent<TextMeshPro>().text =
            "LightWarden: " + jobManager.lighWardenCount.ToString();
    }

    private void UpdateBuilderTotalTextObject()
    {
        transform.Find("Builder").GetComponent<TextMeshPro>().text =
            "Builder: " + jobManager.builderCount.ToString();
    }

    private void UpdateWoodcutterTotalTextObject()
    {
        transform.Find("Woodcutter").GetComponent<TextMeshPro>().text =
            "Woodcutter: " + jobManager.woodcutterCount.ToString();
    }

    private void UpdateStonecutterTotalTextObject()
    {
        transform.Find("Stonecutter").GetComponent<TextMeshPro>().text =
            "Stonecutter: " + jobManager.stonecutterCount.ToString();
    }

    private void UpdateShroomerTotalTextObject()
    {
        transform.Find("Shroomer").GetComponent<TextMeshPro>().text =
            "Shroomer: " + jobManager.shroomerCount.ToString();
    }

    public void UpdateJobsCounter()
    {
        UpdateUnemployedTotalTextObject();
        UpdateLightWardenTotalTextObject();
        UpdateBuilderTotalTextObject();
        UpdateWoodcutterTotalTextObject();
        UpdateStonecutterTotalTextObject();
        UpdateShroomerTotalTextObject();
    }

}
