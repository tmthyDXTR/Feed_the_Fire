using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Window_Construction : MonoBehaviour
{
    private SelectionManager selectionManager;

    private void UpdateInfo()
    {
        
    }

 


    void Awake()
    {
        if (gameObject.activeSelf == true)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        
    }
}
