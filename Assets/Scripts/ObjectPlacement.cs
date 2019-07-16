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
    private Transform currentObject;
    private MiningArea miningArea;
    [SerializeField] private FogOfWarManager fogOfWarManager;

    private float objectRotationSpeed = 100f;
    public bool hasPlaced;

    // Use this for initialization
    void Start()
    {
        
    }

    void Update()
    {
        if (currentObject != null && !hasPlaced)
        {
            Vector3 m = Input.mousePosition;
            m = new Vector3(m.x, m.y, transform.position.y);
            Vector3 p = GetComponent<Camera>().ScreenToWorldPoint(m);
            currentObject.position = new Vector3(p.x, 0, p.z);
            RotateCurrentObject();

            if (currentObject.tag == "MiningArea")
            {
                miningArea = currentObject.GetComponent<MiningArea>();
                if (miningArea != null)
                {
                    miningArea.ShowMinableNodes();
                }
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                if (currentObject.tag != "MiningArea")
                {
                    constructionWindow.SetActive(true);
                    if (currentObject.tag == "Storage")
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
                            hasPlaced = true;
                        }
                    }                    
                }

                if (currentObject.tag == "MiningArea")
                {                                                                            
                    miningArea.SetNodesToMinable("TreeNodes");
                    miningArea.SetNodesToMinable("StoneNodes");
                    miningArea.HideMinableNodes();
                    Destroy(currentObject.gameObject, 1);
                    hasPlaced = true;
                }


            }
            if (Input.GetMouseButtonDown(1))
            {
                if (placeableObject != null && currentObject.tag != "MiningArea")
                {                    
                    Destroy(currentObject.gameObject);
                    constructionWindow.SetActive(false);
                    hasPlaced = false;
                }
                if (currentObject.tag == "MiningArea" && currentObject != null)
                {
                    miningArea.HideMinableNodes();
                    Destroy(currentObject.gameObject);
                    hasPlaced = false;
                }
                //if (currentObject.tag == "Bonfire" && currentObject != null)
                //{
                //    FogOfWarManager.Instance.DeregisterRevealer(currentObject);
                //    Destroy(currentObject.gameObject);
                //    hasPlaced = false;
                //}
            }
        }
    }

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
        currentObject = ((GameObject)Instantiate(objectToBuild)).transform;
        placeableObject = currentObject.GetComponent<PlaceableObject>();               
    }


}
