using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_WoodcutterHut : MonoBehaviour
{
    
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
