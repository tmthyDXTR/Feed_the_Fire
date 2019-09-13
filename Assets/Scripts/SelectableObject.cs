using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    public bool isSelected = false;
    [SerializeField] public GameObject infoWindow;


    void Awake()
    {

        if (this.gameObject.layer == 17) // PlayerUnits Layer
        {
            infoWindow = GameObject.Find("Window_Worker");
        }
        else if (this.gameObject.layer == 15) // Buildings Layer
        {
            if (this.gameObject.name == "FirePlace")
            {
                infoWindow = GameObject.Find("Window_FirePlace");
            }
            else if (this.gameObject.name == "FoodStorage")
            {
                infoWindow = GameObject.Find("Window_FoodStorage");
            }            
            else
            {
                infoWindow = GameObject.Find("Window_Building");
            }
        }
        else
        {
            return;
        }
    }

    void Update()
    {
        
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
                Destroy(infoWindow);
            }
        }
        else
        {
            return;
        }
    }
}
