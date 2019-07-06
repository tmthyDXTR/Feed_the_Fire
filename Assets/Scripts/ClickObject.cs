using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : MonoBehaviour
{
    public GameObject selectedObject;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit rayHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out rayHit, Mathf.Infinity))
            {
                if (rayHit.transform != null)
                {
                    selectedObject = rayHit.transform.gameObject;
                    PrintName(rayHit.transform.gameObject);
                }
            }
        }        
    }

    private void PrintName(GameObject obj)
    {
        print(obj.name);
    }

}
