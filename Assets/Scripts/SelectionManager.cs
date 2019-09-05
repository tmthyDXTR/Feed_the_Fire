using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelectionManager : MonoBehaviour
{
    public bool objectSelected = false;
    public bool isActive = true;

    public List<GameObject> selection = new List<GameObject>();

    private ObjectPlacement objectPlacement;
    private SelectableObject selectableObject;

    private Transform canvas;

    void Awake()
    {
        canvas = GameObject.Find("Canvas").transform;

        objectPlacement = Camera.main.GetComponent<ObjectPlacement>();
    }

    void Update()
    {
        // Selection Detection


        #region BuildingSystem Active
        //Check if BuildingSystem active
        if (objectPlacement.currentObject != null)
        {
            if (objectPlacement.currentObject.tag == "MiningArea")
            {
                // Mining Area Selection Info
            }
            else
            {
                // Building Construction Selection Info
                Debug.Log("Selected Building to construct");
                SetActive(false);
                DeselectAll();
                Select(objectPlacement.currentObject.gameObject);
            }
        }
        else
        {
            SetActive(true);
        }
        #endregion


        #region Object Selection

        if (isActive == true)
        {
            if (objectPlacement.currentObject == null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Left Click at: " + GetWorldPoint());    
                    
                    RaycastHit rayHit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out rayHit))
                    {
                        if (rayHit.collider != null)
                        {
                            Debug.Log("Clicked on: " + rayHit.collider);

                            // Check if Collider GameObject is a Selectable Object
                            if (rayHit.collider.gameObject.GetComponent<SelectableObject>() != null)
                            {                                
                                DeselectAll();
                                Select(rayHit.collider.gameObject);
                                                            
                            }
                        }
                    }
                                                         
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    Debug.Log("Right Click at: " + GetWorldPoint());
                    DeselectAll();
                }
            }
        }
        else
        {
            return;
        }
        
        #endregion

    }
    public void SetActive(bool toggle)
    {
        if (toggle == true)
        {
            isActive = true;
        }
        else if (toggle == false)
        {
            isActive = false;
        }
    }


    public void Select(GameObject obj)
    {
        if (obj.GetComponent<SelectableObject>() != null)
        {
            selectableObject = obj.GetComponent<SelectableObject>();
            selectableObject.isSelected = true;
            if (selectableObject.infoWindow != null)
            {
                selectableObject.OpenCloseInfo();
            }
            else
            {
                Debug.Log("No Window! Now Instantiating");
                GameObject firePlaceWindow = Instantiate(Resources.Load("Window_FirePlace")) as GameObject;
                firePlaceWindow.transform.SetParent(canvas);
                firePlaceWindow.transform.localPosition = new Vector3(-200, -200, 0);

                selectableObject = obj.GetComponent<SelectableObject>();
                selectableObject.infoWindow = firePlaceWindow;
            }
        }
        selection.Add(obj);
        objectSelected = true;
    }

    public void Deselect(GameObject obj)
    {        
        if (obj.GetComponent<SelectableObject>() != null)
        {
            selectableObject = obj.GetComponent<SelectableObject>();
            selectableObject.isSelected = false;
            selectableObject.OpenCloseInfo();
        }
        if (selection.Count == 0)
        {
            objectSelected = false;
        }
        selection.Remove(obj);
    }

    public void DeselectAll()
    {
        foreach (GameObject obj in selection)
        {
            if (obj.GetComponent<SelectableObject>() != null)
            {
                selectableObject = obj.GetComponent<SelectableObject>();
                selectableObject.isSelected = false;
                selectableObject.OpenCloseInfo();
            }
        }
        selection.Clear();
        objectSelected = false;
    }

    Vector3 GetWorldPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}
