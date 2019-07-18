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
            //if (gameObject.tag == "Storage" || gameObject.tag == "StorageFull" || gameObject.tag == "StorageEmpty")
            //{

            //}
            //if (gameObject.tag == "ResidentialHouse")
            //{

            //}
            //else
            //{
            //    infoPanel = GameObject.Find("Window_Construction");
            //}
        }

        if (gameObject.layer == 13) // SafePlaceNodes Layer
        {
            BuildingInfo buildingInfo = gameObject.GetComponent<BuildingInfo>();

        }

        if (gameObject.layer == 17) // PlayerUnits Layer
        {
            WorkerAI workerInfo = gameObject.GetComponent<WorkerAI>();
            infoPanel = GameObject.Find("Window_Worker");
            Debug.Log("Window_Worker found");

        }      
    }

    void Update()
    {
        
    }
}
