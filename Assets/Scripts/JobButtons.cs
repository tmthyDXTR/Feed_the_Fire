using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class JobButtons : MonoBehaviour
{
    JobManager jobManager;
    GameHandler gameHandler;
    Text builderButton;
    Text lightWardenButton;
    Text woodcutterButton;
    Text shroomerButton;
    Text unemployedButton;
    void Start()
    {
        jobManager = GameObject.Find("Workers").GetComponent<JobManager>();
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        builderButton = this.transform.Find("Button_AddRemoveBuilder").GetChild(0).GetComponent<Text>();
        lightWardenButton = this.transform.Find("Button_AddRemoveLightWarden").GetChild(0).GetComponent<Text>();
        woodcutterButton = this.transform.Find("Button_AddRemoveWoodcutter").GetChild(0).GetComponent<Text>();
        shroomerButton = this.transform.Find("Button_AddRemoveShroomer").GetChild(0).GetComponent<Text>();
        unemployedButton = this.transform.Find("Button_Unemployed").GetChild(0).GetComponent<Text>();

        UpdateJobButtons();
        jobManager.OnJobChanged += delegate (object sender, EventArgs e)
        {
            //Debug.Log("OnJobChanged event");
            UpdateJobButtons();
        };
        gameHandler.OnHousingChanged += delegate (object sender, EventArgs e)
        {
            //Debug.Log("OnHousingChanged event");
            UpdateJobButtons();
        };
    }

    private void UpdateJobButtons()
    {
        builderButton.text = jobManager.builderCount.ToString() + " - Builder";
        lightWardenButton.text = jobManager.lighWardenCount.ToString() + " - Light Warden";
        woodcutterButton.text = jobManager.woodcutterCount.ToString() + " - Woodcutter";
        shroomerButton.text = jobManager.shroomerCount.ToString() + " - Shroomer";
        unemployedButton.text = jobManager.unemployedCount.ToString() + " - Unemployed";
    }

    private void OnDestroy()
    {
        jobManager.OnJobChanged -= delegate (object sender, EventArgs e)
        {
            //Debug.Log("OnJobChanged event");
            UpdateJobButtons();
        };
        gameHandler.OnHousingChanged -= delegate (object sender, EventArgs e)
        {
            //Debug.Log("OnHousingChanged event");
            UpdateJobButtons();
        };
    }
}
