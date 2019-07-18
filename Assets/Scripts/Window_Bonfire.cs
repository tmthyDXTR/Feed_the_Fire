using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_Bonfire : MonoBehaviour
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
                Debug.Log("Update FirePlace Info");
                //UpdateInfo();


                if (selectedObject.tag != "Bonfire")
                {
                    gameObject.SetActive(false);
                }
            }
            //else
            //{
            //    selectedObject = null;
            //}
        }
    }
}
