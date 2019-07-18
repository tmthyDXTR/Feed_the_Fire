using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_WoodLogs : MonoBehaviour
{
    public GameObject selectedObject;
    public Selection selection;

    void Awake()
    {
        {
            if (gameObject.activeSelf == true)
            {
                gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (gameObject.activeSelf == true)
        {
            if (selection.selectedObjects[0] != null)
            {
                selectedObject = selection.selectedObjects[0];
                Debug.Log("Update Wood Storage Info");
                //UpdateInfo();
                if (selectedObject.tag != "StorageEmpty" && selectedObject.tag != "Storage" && selectedObject.tag != "StorageFull")
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                selectedObject = null;
            }
        }
    }
}
