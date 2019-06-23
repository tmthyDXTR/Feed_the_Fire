using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    private PlaceableObject placeableObject;
    private Transform currentObject;
    private float objectRotationSpeed = 100f;
    public bool hasPlaced;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentObject != null && !hasPlaced)
        {
            Vector3 m = Input.mousePosition;
            m = new Vector3(m.x, m.y, transform.position.y);
            Vector3 p = GetComponent<Camera>().ScreenToWorldPoint(m);
            currentObject.position = new Vector3(p.x, 0, p.z);
            RotateCurrentObject();


            if (Input.GetMouseButtonDown(0))
            {
                if (IsLegalPosition())
                {
                    currentObject.tag = "Construction";
                    hasPlaced = true;
                }                         
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (placeableObject != null)
                {
                    Destroy(currentObject.gameObject);
                    hasPlaced = false;
                }
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
