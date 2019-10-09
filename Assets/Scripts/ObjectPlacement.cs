using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    private BuildingInfo buildingInfo;
    private PlaceableObject placeableObject;
    private SelectableObject selectableObject;
    [SerializeField] private MiningArea miningArea;
    [SerializeField] private ShroomGrowingArea shroomGrowingArea;
    [SerializeField] private ConstructionManager constructionManager;
    private SelectionManager selectionManager;
    private GridManager grid;


    public Transform currentObject;
    public bool hasPlaced = false;
          

    public GameObject wallStart;
    public GameObject wallEnd;
    public GameObject wallPrefab;
    public GameObject wallPart;
    GameObject wallPoleCursor;
    public bool creatingWall;
    private BoxCollider m_BoxCollider;

    public float unsnapMouseDistance = 25;
    private Vector3 snapMousePos;
    public Transform snapTarget;
    private float objectRotationSpeed = 100f;    

    Vector3 mousePos;

    void Awake()
    {
        selectionManager = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        constructionManager = GameObject.Find("ConstructionManager").GetComponent<ConstructionManager>();
        grid = GameObject.Find("GridManager").GetComponent<GridManager>();
    }

    void Update()
    {
        // Check if an Object is selected in SelectionManager
        if (selectionManager.objectSelected == true)
        {
            if (currentObject != null)
            {
                Cursor.visible = false;
                mousePos = new Vector3(GetWorldPoint().x, 0f, GetWorldPoint().z);
                currentObject.position = grid.GetNearestPointOnGrid(mousePos);
                RotateCurrentObject();

                if (currentObject.tag == "MiningArea")
                {
                    miningArea = currentObject.GetComponent<MiningArea>();
                    if (miningArea != null)
                    {
                        miningArea.ShowMinableNodes();
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        miningArea.SetNodesToMinable("TreeNodes");
                        miningArea.SetNodesToMinable("StoneNodes");
                        miningArea.HideMinableNodes();
                        selectionManager.Deselect(currentObject.gameObject);
                        PlaceObject();
                        Destroy(currentObject.gameObject);
                        currentObject = null;
                    }
                    if (Input.GetMouseButtonDown(1))
                    {
                        selectionManager.Deselect(currentObject.gameObject);
                        miningArea.HideMinableNodes();
                        DeleteObject();
                    }
                }

                else if (currentObject.tag == "ShroomArea")
                {
                    shroomGrowingArea = currentObject.GetComponent<ShroomGrowingArea>();
                    if (shroomGrowingArea != null)
                    {
                        shroomGrowingArea.ShowMinableNodes();
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        shroomGrowingArea.SetNodesToMinable("StumpNodes");
                        shroomGrowingArea.HideMinableNodes();
                        selectionManager.Deselect(currentObject.gameObject);
                        PlaceObject();
                        Destroy(currentObject.gameObject);
                        currentObject = null;
                    }
                    if (Input.GetMouseButtonDown(1))
                    {
                        selectionManager.Deselect(currentObject.gameObject);
                        shroomGrowingArea.HideMinableNodes();
                        DeleteObject();
                    }
                }


                else if (currentObject.tag == "WoodcutterHut")
                {
                    buildingInfo = currentObject.GetComponent<BuildingInfo>();
                    if (Input.GetMouseButtonDown(0) && IsLegalPosition())
                    {
                        PlaceBuilding();
                    }
                    if (Input.GetMouseButtonDown(1))
                    {
                        DeleteObject();
                    }
                }
                else if (currentObject.tag == "ResidentialHouse")
                {
                    buildingInfo = currentObject.GetComponent<BuildingInfo>();
                    if (Input.GetMouseButtonDown(0) && IsLegalPosition())
                    {
                        PlaceBuilding();
                    }
                    if (Input.GetMouseButtonDown(1))
                    {
                        DeleteObject();
                    }
                }
                else if (currentObject.tag == "Bonfire")
                {
                    buildingInfo = currentObject.GetComponent<BuildingInfo>();
                    if (Input.GetMouseButtonDown(0) && IsLegalPosition())
                    {
                        PlaceBuilding();
                    }
                    if (Input.GetMouseButtonDown(1))
                    {
                        DeleteObject();
                    }
                }
                else if (currentObject.tag == "Tavern")
                {
                    buildingInfo = currentObject.GetComponent<BuildingInfo>();
                    if (Input.GetMouseButtonDown(0) && IsLegalPosition())
                    {                        
                        PlaceBuilding();
                    }
                    if (Input.GetMouseButtonDown(1))
                    {
                        DeleteObject();
                    }
                }
                else if (currentObject.tag == "StorageEmpty")
                {
                    Vector3 firePlace = GameObject.Find("FirePlace").transform.position;
                    currentObject.position += new Vector3(0, 0.6f, 0);
                    currentObject.transform.LookAt(new Vector3(firePlace.x, 0, firePlace.z));

                    //buildingInfo = currentObject.GetComponent<BuildingInfo>();
                    if (Input.GetMouseButtonDown(0) && IsLegalPosition())
                    {
                        PlaceObject();
                        selectionManager.Deselect(currentObject.gameObject);
                        currentObject = null;
                    }
                    if (Input.GetMouseButtonDown(1))
                    {
                        DeleteObject();
                    }
                }

            }
            else
            {
                return;
            }



            //if (currentObject.tag == "WallPole")
            //{
            //    //currentObject.position = GetWorldPoint();
            //    if (IsLegalPosition() && !creatingWall && Input.GetMouseButtonDown(0))
            //    {
            //        SetStart();
            //        Debug.Log("Set Wall Start");
            //    }
            //    else if (Input.GetMouseButtonDown(0))
            //    {
            //        AddWall();
            //        Debug.Log("Add Wall");
            //    }
            //    else
            //    {
            //        if (creatingWall)
            //        {
            //            Adjust();
            //        }
            //    }
            //    if (Input.GetMouseButtonDown(1))
            //    {
            //        Cursor.visible = true;
            //        creatingWall = false;
            //        Destroy(wallPart.gameObject);
            //    }
            //}

            // Buildings
        }
    }

    #region Wall Building Methods

    void SetStart()
    {
        creatingWall = true;
        wallStart.transform.position = new Vector3(GetWorldPoint().x, 0, GetWorldPoint().z);

        wallPart = (GameObject)Instantiate(wallPrefab, wallStart.transform.position, Quaternion.identity);
        snapTarget = wallPart.gameObject.transform.GetChild(0);
    }
    //void SetEnd()
    //{
    //    creatingWall = false;
    //    wallEnd.transform.position = wallStart.transform.position;
    //    //PlaceObject();
    //}
    void Adjust()
    {
        wallEnd.transform.position = new Vector3(GetWorldPoint().x, 0, GetWorldPoint().z);
        AdjustWall();
    }
    void AdjustWall()
    {
        wallStart.transform.LookAt(wallEnd.transform.position);
        wallEnd.transform.LookAt(wallStart.transform.position);
        wallPart.transform.LookAt(wallStart.transform.position);
        //currentObject.transform.LookAt(wallEnd.transform.position);
        float distance = Vector3.Distance(wallStart.transform.position, wallEnd.transform.position);
        wallPart.transform.position = wallStart.transform.position + wallStart.transform.forward;
        m_BoxCollider = wallPart.GetComponent<BoxCollider>();
        //if (distance > m_BoxCollider.size.z - 2)
        //{
        //    Debug.Log("Adding Wall");
        //    AddWall();
        //    //(GameObject)Instantiate(wallPrefab, wallStart.transform.position, Quaternion.identity);
        //}
    }
    void AddWall()
    {
        creatingWall = true;
        //if (snapTarget == null)
        //{
        //    snapTarget = wallPart.gameObject.transform.GetChild(0);
        //    wallPart = (GameObject)Instantiate(wallPrefab, snapTarget.transform.position, Quaternion.identity);

        //}
        snapTarget = wallPart.gameObject.transform.GetChild(0);
        wallStart.transform.position = snapTarget.transform.position;
        wallPart = (GameObject)Instantiate(wallPrefab, wallStart.transform.position, Quaternion.identity);
        
        //wallStart.transform.position += wallStart.transform.forward;
    }


    #endregion

    bool IsLegalPosition()
    {
        if (placeableObject.colliders.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void RotateCurrentObject()
    {
        if (Input.GetKey(KeyCode.Y))
            placeableObject.transform.Rotate(Vector3.up * objectRotationSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.X))
            placeableObject.transform.Rotate(-Vector3.up * objectRotationSpeed * Time.deltaTime);
    }

    public void SetItem(GameObject objectToBuild)
    {
        hasPlaced = false;
        currentObject = ((GameObject)Instantiate(objectToBuild, mousePos, Quaternion.identity)).transform;
        if (currentObject != null)
        {
            currentObject.transform.SetParent(GameObject.Find("Level").transform);
            placeableObject = currentObject.GetComponent<PlaceableObject>();
            selectionManager.Select(currentObject.gameObject);
        }
        
    }

    private void PlaceObject()
    {
        if (selectionManager == null)
        {
            selectionManager = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        }
        if (currentObject.GetComponent<WoodLogs>() != null)
        {
            currentObject.transform.SetParent(GameObject.Find("WoodStorage").transform);
            currentObject.GetComponent<WoodLogs>().isBuildingMode = false;
        }
        hasPlaced = true;
        Cursor.visible = true;
        selectionManager.isActive = true;
    }

    

    private void PlaceBuilding()
    {
        buildingInfo.SetBuildingModel(0);
        currentObject.tag = "Construction";
        if (constructionManager != null)
        {
            constructionManager.RegisterConstruction(currentObject.gameObject);
        }
        else
        {
            constructionManager = GameObject.Find("ConstructionManager").GetComponent<ConstructionManager>();
            constructionManager.RegisterConstruction(currentObject.gameObject);
        }
        currentObject = null;
        hasPlaced = true;
        Cursor.visible = true;
        selectionManager.isActive = true;
    }

    private void DeleteObject()
    {
        hasPlaced = false;
        selectionManager.Deselect(currentObject.gameObject);
        Destroy(currentObject.gameObject);
        currentObject = null;
        Cursor.visible = true;
        selectionManager.isActive = true;

    }

    Vector3 GetWorldPoint()
    {
        Ray ray = GetComponent<Camera>().ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    

}
