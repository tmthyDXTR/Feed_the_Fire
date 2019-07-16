using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : MonoBehaviour
{
    public GameObject selectedObject;
    public Selection selection;
    public ClickableObject clickableObject;
    public LayerMask layer;
    private LayerMask layerBuildings;
    private LayerMask layerPlayerUnits;
    private LayerMask layerTreeNodes;
    private LayerMask layerStoneNodes;
    private LayerMask layerSafePlaceNodes;
    private LayerMask layerGround;
    ArrayList layerList = new ArrayList();

    private void Awake()
    {
        layerBuildings = LayerMask.GetMask("Buildings");
        layerPlayerUnits = LayerMask.GetMask("PlayerUnits");
        layerTreeNodes = LayerMask.GetMask("TreeNodes");
        layerStoneNodes = LayerMask.GetMask("StoneNodes");
        layerSafePlaceNodes = LayerMask.GetMask("SafePlaceNodes");
        //layerGround = LayerMask.GetMask("Ground");
        layerList.Add(layerBuildings);
        layerList.Add(layerPlayerUnits);
        layerList.Add(layerTreeNodes);
        layerList.Add(layerStoneNodes);
        layerList.Add(layerSafePlaceNodes);
        //layerList.Add(layerGround);
        foreach (var layer in layerList)
        {
            Debug.Log("layerList layer: " + layer);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObject != null)
            {
                ClickableObject clickableObject = selectedObject.GetComponent<ClickableObject>();
                clickableObject.selected = false;
                selection.selectedObjects.Remove(selectedObject);
                clickableObject.CloseInfo();
                selectedObject = null;
            }
            RaycastHit rayHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            foreach (LayerMask layer in layerList)
            {
                if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, layer))
                {
                    if (rayHit.transform != null)
                    {
                        selectedObject = rayHit.transform.gameObject;
                        PrintName(rayHit.transform.gameObject);
                        ClickableObject clickableObject = selectedObject.GetComponent<ClickableObject>();
                        if (clickableObject != null)
                        {
                            Debug.Log("clickableObject Script found");

                            if (clickableObject.selected != true)
                            {
                                clickableObject.selected = true;
                                selection.selectedObjects.Add(selectedObject);
                                clickableObject.OpenInfo();
                            }
                        }
                    }
                }
            }
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            ClickableObject clickableObject = selectedObject.GetComponent<ClickableObject>();
            if (clickableObject.selected != false)
            {
                clickableObject.selected = false;
                selection.selectedObjects.Remove(selectedObject);
                clickableObject.CloseInfo();
                selectedObject = null;

            }

        }


    }

    private void PrintName(GameObject obj)
    {
        print(obj.name);
    }

}
