using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    public bool isSelected = false;
    [SerializeField] public GameObject infoWindow;

    // Info Panels and Tooltips
    private Window_Worker window_Unit;
    private Window_Building window_Building;
    private Window_FirePlace window_FirePlace;

    //[SerializeField] private GameObject windowBuilding;

    void Awake()
    {

        if (this.gameObject.layer == 17) // PlayerUnits Layer
        {
            infoWindow = GameObject.Find("Window_Worker");
            window_Unit = infoWindow.GetComponent<Window_Worker>();
        }
        else if (this.gameObject.layer == 15 && this.gameObject.name != "FirePlace") // Buildings Layer
        {            
            infoWindow = GameObject.Find("Window_Building");
            window_Building = infoWindow.GetComponent<Window_Building>();                   
        }
        else if (this.gameObject.layer == 15 && this.gameObject.name == "FirePlace") 
        {
            infoWindow = GameObject.Find("Window_FirePlace");
            //window_FirePlace = infoWindow.GetComponent<Window_FirePlace>();
        }
        else
        {
            return;
        }
    }

    void Update()
    {
        // Check if this Object is selected currently
        if (isSelected == true)
        {
            // If this Object is selected open its Info Window
            if (infoWindow != null)
            {
                if (this.gameObject.layer == 17) // PlayerUnits Layer
                {
                    window_Unit.unitInfo = GetComponent<UnitInfo>();
                    window_Unit.selectedObject = this.gameObject;
                }
                else if (this.gameObject.layer == 15 && this.gameObject.name != "FirePlace") // Buildings Layer
                {
                    window_Building.buildingInfo = GetComponent<BuildingInfo>();
                    window_Building.selectedObject = this.gameObject;
                }
            }
        }
        //else
        //{
        //    infoWindow.SetActive(false);
        //}
    }

    public void OpenCloseInfo()
    {
        if (isSelected == true)
        {
            if (infoWindow != null)
            {
                infoWindow.SetActive(true);
            }
        }
        else if (isSelected == false)
        {
            if (infoWindow != null)
            {
                infoWindow.SetActive(false);
            }
        }
        else
        {
            return;
        }
    }
}
