using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    private BuildingInfo buildingInfo;
    private PlaceableObject placeableObject;
    private ClickableObject clickableObject;
    public GameObject constructionWindow;
    public GameObject woodcutterWindow;
    public GameObject residentialHouseWindow;
    public GameObject bonfireWindow;

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

    public Transform currentObject;
    private MiningArea miningArea;

    private float objectRotationSpeed = 100f;
    public bool hasPlaced = false;

    private GridManager grid;

    Vector3 mousePos;

    void Awake()
    {
        grid = FindObjectOfType<GridManager>();
    }

    void Update()
    {
        if (currentObject != null && !hasPlaced)
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
            }

            if (currentObject.tag == "WallPole")
            {
                //currentObject.position = GetWorldPoint();
                if (IsLegalPosition() && !creatingWall && Input.GetMouseButtonDown(0))
                {
                    SetStart();
                    Debug.Log("Set Wall Start");
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    AddWall();
                    Debug.Log("Add Wall");
                }
                else
                {
                    if (creatingWall)
                    {
                        Adjust();
                    }
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse click position: " + GetWorldPoint());
                if (currentObject.tag != "MiningArea")
                {
                    //--Fix this, temp Window activation--//
                    constructionWindow.SetActive(true);
                    bonfireWindow.SetActive(true);

                    if (currentObject.tag == "WoodcutterHut")
                    {
                        if (IsLegalPosition())
                        {
                            buildingInfo = currentObject.GetComponent<BuildingInfo>();
                            clickableObject = currentObject.GetComponent<ClickableObject>();
                            clickableObject.infoPanel = GameObject.Find("Window_Construction");
                            woodcutterWindow.SetActive(true);
                            clickableObject.originalInfoPanel = GameObject.Find("Window_WoodcutterHut");
                            woodcutterWindow.SetActive(false);
                            buildingInfo.SetBuildingModel(0);
                            currentObject.tag = "Construction";
                            PlaceObject();
                        }
                    }      
                    if (currentObject.tag == "ResidentialHouse")
                    {
                        if (IsLegalPosition())
                        {
                            buildingInfo = currentObject.GetComponent<BuildingInfo>();
                            clickableObject = currentObject.GetComponent<ClickableObject>();
                            clickableObject.infoPanel = GameObject.Find("Window_Construction");
                            residentialHouseWindow.SetActive(true);
                            clickableObject.originalInfoPanel = GameObject.Find("Window_ResidentialHouse");
                            residentialHouseWindow.SetActive(false);
                            buildingInfo.SetBuildingModel(0);
                            currentObject.tag = "Construction";
                            PlaceObject();
                        }
                    }
                    if (currentObject.tag == "Bonfire")
                    {
                        if (IsLegalPosition())
                        {
                            buildingInfo = currentObject.GetComponent<BuildingInfo>();
                            clickableObject = currentObject.GetComponent<ClickableObject>();
                            clickableObject.infoPanel = GameObject.Find("Window_Construction");
                            residentialHouseWindow.SetActive(true);
                            clickableObject.originalInfoPanel = GameObject.Find("Window_Bonfire");
                            residentialHouseWindow.SetActive(false);
                            //buildingInfo.SetBuildingModel(0);
                            currentObject.tag = "Construction";
                            PlaceObject();
                        }
                    }                    
                }               

                if (currentObject.tag == "MiningArea")
                {                                                                            
                    miningArea.SetNodesToMinable("TreeNodes");
                    miningArea.SetNodesToMinable("StoneNodes");
                    miningArea.HideMinableNodes();
                    Destroy(currentObject.gameObject, 1);
                    PlaceObject();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Cursor.visible = true;
                if (placeableObject != null && currentObject.tag != "MiningArea")
                {                    
                    Destroy(currentObject.gameObject);
                    constructionWindow.SetActive(false);
                    hasPlaced = false;
                    if (currentObject.tag == "WallPole")
                    {
                        creatingWall = false;
                        Destroy(wallPart.gameObject);
                    }
                }
                if (currentObject.tag == "MiningArea" && currentObject != null)
                {
                    miningArea.HideMinableNodes();
                    Destroy(currentObject.gameObject);
                    hasPlaced = false;
                }
                currentObject = null;
                //if (currentObject.tag == "Bonfire" && currentObject != null)
                //{
                //    FogOfWarManager.Instance.DeregisterRevealer(currentObject);
                //    Destroy(currentObject.gameObject);
                //    hasPlaced = false;
                //}
            }
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
        placeableObject = currentObject.GetComponent<PlaceableObject>();               
    }

    private void PlaceObject()
    {
        hasPlaced = true;
        currentObject = null;
        Cursor.visible = true;
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
