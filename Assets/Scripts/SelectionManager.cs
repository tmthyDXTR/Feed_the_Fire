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

        //ResourceBank.OnSelectionManagerDisabled += delegate (object sender, EventArgs e)
        //{
        //    SetActive(false);
        //};
        //ResourceBank.OnSelectionManagerEnabled += delegate (object sender, EventArgs e)
        //{
        //    SetActive(true);
        //};
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
        //else
        //{
        //    if (isActive != true)
        //    {
        //        SetActive(true);
        //    }
        //}
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
                    if (selection.Count != 0 && selection[0].tag == "Hero")
                    {
                        
                    }
                    else
                    {
                        Debug.Log("Right Click at: " + GetWorldPoint());
                        DeselectAll();
                        ProjectorManager projectorManager = GameObject.Find("SelectionManager").GetComponent<ProjectorManager>();
                        foreach (GameObject projector in projectorManager.projectorList)
                        {
                            projector.transform.GetChild(0).GetComponent<Projector>().enabled = false;
                        }
                    }                    
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
            Debug.Log("SelectionManager Enabled");
            isActive = true;
        }
        else if (toggle == false)
        {
            Debug.Log("SelectionManager Disabled");
            isActive = false;
        }
    }


    public void Select(GameObject obj)
    {
        if (obj.GetComponent<SelectableObject>() != null)
        {
            selectableObject = obj.GetComponent<SelectableObject>();
            selectableObject.isSelected = true;
            if (selectableObject.gameObject.layer == 17 && selectableObject.gameObject.tag == "Worker")
            {
                GameObject windowWorker = Instantiate(Resources.Load("Window_Worker")) as GameObject;
                windowWorker.transform.SetParent(canvas);
                windowWorker.transform.localPosition = new Vector3(-200, -200, 0);

                //selectableObject = obj.GetComponent<SelectableObject>();
                selectableObject.infoWindow = windowWorker;

                //selectableObject.OpenCloseInfo();
            }
            else if (selectableObject.gameObject.layer == 17 && selectableObject.gameObject.tag == "Hero")
            {
                GameObject windowBuilding = Instantiate(Resources.Load("Window_Hero")) as GameObject;
                windowBuilding.transform.SetParent(canvas);
                windowBuilding.transform.localPosition = new Vector3(-200, -200, 0);

                //selectableObject = obj.GetComponent<SelectableObject>();
                selectableObject.infoWindow = windowBuilding;

                //selectableObject.OpenCloseInfo();
            }
            else if (selectableObject.gameObject.layer == 15 && selectableObject.name != "FirePlace" && selectableObject.name != "FoodStorage" && selectableObject.tag != "UnlitBonfire")
            {
                GameObject windowBuilding = Instantiate(Resources.Load("Window_Building")) as GameObject;          
                windowBuilding.transform.SetParent(canvas);
                windowBuilding.transform.localPosition = new Vector3(-200, -200, 0);

                //selectableObject = obj.GetComponent<SelectableObject>();
                selectableObject.infoWindow = windowBuilding;

                //selectableObject.OpenCloseInfo();
            }
            else if (selectableObject.tag == "UnlitBonfire")
            {
                GameObject windowUnlitBonfire = Instantiate(Resources.Load("Window_Bonfire")) as GameObject;
                windowUnlitBonfire.transform.SetParent(canvas);
                windowUnlitBonfire.transform.localPosition = new Vector3(-200, -200, 0);

                //selectableObject = obj.GetComponent<SelectableObject>();
                selectableObject.infoWindow = windowUnlitBonfire;

                //selectableObject.OpenCloseInfo();
            }
            else if (selectableObject.name == "FirePlace")
            {
                //Debug.Log("No Window! Now Instantiating");
                ProjectorManager projectorManager = GameObject.Find("SelectionManager").GetComponent<ProjectorManager>();
                foreach (GameObject projector in projectorManager.projectorList)
                {
                    projector.transform.GetChild(0).GetComponent<Projector>().enabled = true;
                }


                GameObject firePlaceWindow = Instantiate(Resources.Load("Window_FirePlace")) as GameObject;
                firePlaceWindow.transform.SetParent(canvas);
                firePlaceWindow.transform.localPosition = new Vector3(-200, -200, 0);

                //selectableObject = obj.GetComponent<SelectableObject>();
                selectableObject.infoWindow = firePlaceWindow;
            }
            else if (selectableObject.name == "FoodStorage")
            {
                //Debug.Log("No Window! Now Instantiating");
                GameObject firePlaceWindow = Instantiate(Resources.Load("Window_FoodStorage")) as GameObject;
                firePlaceWindow.transform.SetParent(canvas);
                firePlaceWindow.transform.localPosition = new Vector3(-200, -200, 0);

                //selectableObject = obj.GetComponent<SelectableObject>();
                selectableObject.infoWindow = firePlaceWindow;
            }
            else if (selectableObject.tag == "Stump" || selectableObject.tag == "ShroomGrow")
            {
                GameObject stumpWindow = Instantiate(Resources.Load("Window_Stump")) as GameObject;
                stumpWindow.transform.SetParent(canvas);
                stumpWindow.transform.localPosition = new Vector3(-200, -200, 0);

                //selectableObject = obj.GetComponent<SelectableObject>();
                selectableObject.infoWindow = stumpWindow;
            }
            else if (selectableObject.gameObject.layer == 19 && selectableObject.tag == "Enemy")
            {
                GameObject windowEnemy = Instantiate(Resources.Load("Window_Enemy")) as GameObject;
                windowEnemy.transform.SetParent(canvas);
                windowEnemy.transform.localPosition = new Vector3(-200, -200, 0);

                //selectableObject = obj.GetComponent<SelectableObject>();
                selectableObject.infoWindow = windowEnemy;

                //selectableObject.OpenCloseInfo();
            }
            else if (selectableObject.gameObject.layer == 19 && selectableObject.tag == "DeadTree")
            {
                GameObject windowDeadTree = Instantiate(Resources.Load("Window_Tree")) as GameObject;
                windowDeadTree.transform.SetParent(canvas);
                windowDeadTree.transform.localPosition = new Vector3(-200, -200, 0);

                //selectableObject = obj.GetComponent<SelectableObject>();
                selectableObject.infoWindow = windowDeadTree;

                //selectableObject.OpenCloseInfo();
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
