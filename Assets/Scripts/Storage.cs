using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public bool isFull = false;
    public bool isEmpty = true;
    public int storageRoom = 10;
    public int stored = 0;

    public int stockShrooms = 0;
    public int stockSpores = 0;


    void Start()
    {
        
    }

    void Update()
    {
        if (stored == storageRoom)
        {
            isFull = true;
            isEmpty = false;
        }
        else if(stored > 0 && stored != storageRoom)
        {
            isFull = false;
            isEmpty = false;
        }
        else
        {
            isFull = false;
            isEmpty = true;
        }
    }

    public void StoreShrooms()
    {
        stored += 1;
        stockShrooms += 1;
        AddResourceInStorage("Shrooms");
    }

    public void RemoveSpores()
    {
        stored -= 1;
        stockSpores -= 1;
        RemoveResourceFromStorage("Spores");
    }

    private void AddResourceInStorage(string resource)
    {
        foreach (Transform child in this.transform)
        {
            if (child.gameObject.GetComponent<SingleStorage>().isFull == true)
            {
                continue;
            }
            else
            {
                if (resource == "Shrooms")
                {
                    GameObject shrooms = Instantiate(Resources.Load("Shrooms")) as GameObject;
                    shrooms.transform.position = new Vector3(child.transform.position.x, child.transform.position.y - 0.8f, child.transform.position.z);
                    shrooms.transform.SetParent(child.transform);
                    shrooms.transform.Find("Particle System").gameObject.SetActive(false);
                    child.GetComponent<SingleStorage>().isFull = true;
                    break;
                }
            }
        }
    }

    private void RemoveResourceFromStorage(string resource)
    {
        foreach (Transform child in this.transform)
        {
            if (child.gameObject.GetComponent<SingleStorage>().isFull = false)
            {
                continue;
            }
            else if (resource == "Spores")
            {                
                if (child.GetChild(0).GetChild(0).name == "Vial_Spores")
                {
                    Destroy(child.GetChild(0).GetChild(0).gameObject);
                    child.GetComponent<SingleStorage>().isFull = false;
                    break;
                }
                
            }
        }
    }
}
