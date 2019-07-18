using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_ResidentialHouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (gameObject.activeSelf == true)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
