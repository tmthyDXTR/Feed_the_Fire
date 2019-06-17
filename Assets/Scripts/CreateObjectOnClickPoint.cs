using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjectOnClickPoint : MonoBehaviour
{

    Ray ray;
    public RaycastHit hit;
    public GameObject gameObjectToPlace;
    public bool click;

    // Use this for initialization
    void Start()
    {
        click = false;



    }

    // Update is called once per frame
    void Update()
    {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {

            if (Input.GetMouseButtonDown(0) && click == false && gameObjectToPlace != null)
            {
                click = true;
                SpawnObject();
            }

        }    
    }
    void SpawnObject()
    {
        if (click == true)
        {
            click = false;
            Debug.Log("Spawn Object");
            GameObject obj = Instantiate(gameObjectToPlace, new Vector3(hit.point.x, 
                hit.point.y, hit.point.z), Quaternion.identity) as GameObject;
        }
        
    }


}
