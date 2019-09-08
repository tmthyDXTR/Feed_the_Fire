using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] objects;
    private ObjectPlacement objectPlacement;
    private GameObject buildingWindow;

    // Start is called before the first frame update
    void Awake()
    {
        //buildingWindow = GameObject.Find("Window_Building");
        //buildingWindow.SetActive(false);
        objectPlacement = GetComponent<ObjectPlacement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        for (int i = 0; i <objects.Length; i++)
        {
            if (GUI.Button(new Rect(Screen.width/20,Screen.height/15 + Screen.height/12 * i,100,30), objects[i].name))
            {
                //buildingWindow.SetActive(true);
                objectPlacement.SetItem(objects[i]);
            }
        }
    }
}
