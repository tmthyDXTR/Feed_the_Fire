using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public GameObject infoPanel;
    public GameObject originalInfoPanel = null;
    public bool selected = false;



    public void OpenInfo()
    {
        Debug.Log("Info opened");
        if (infoPanel != null)
        {
            infoPanel.SetActive(true);
        }

    }
    public void CloseInfo()
    {
        Debug.Log("Info closed");
        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }
    }


    void Awake()
    {
        selected = false;
        if (infoPanel != null)
        {
            CloseInfo();
        }

        if (gameObject.layer == 15) // Buildings Layer
        {
            BuildingInfo buildingInfo = gameObject.GetComponent<BuildingInfo>();
            infoPanel = GameObject.Find("Window_Construction");

        }

        if (gameObject.layer == 17) // PlayerUnits Layer
        {
            WorkerAI workerInfo = gameObject.GetComponent<WorkerAI>();
            infoPanel = GameObject.Find("Window_Worker");
            Debug.Log("Window_Worker found");

        }


        //if (this.gameObject.CompareTag("Bonfire"))
        //{

        //}

    }

    void Update()
    {
        
    }
}
