using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SingleStorage : MonoBehaviour
{
    public bool isFull = false;

    [SerializeField] private Storage storage;
    [SerializeField] public StoredItem storedItem;
    GameHandler gameHandler;


    void Awake()
    {
        //storedItem = StoredItem.Empty;
        storage = this.transform.parent.GetComponent<Storage>();
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();

    }

    void Update()
    {
   
    }

    public void AddItem(StoredItem item)
    {
        if (item == StoredItem.Shrooms)
        {
            storage.stockShrooms += 1;
            storage.stored += 1;
            gameHandler.AddFoodToStock(1);
            isFull = true;
            GameObject obj = Instantiate(Resources.Load("Shrooms")) as GameObject;
            obj.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y - 0.8f, this.transform.position.z);
            obj.transform.SetParent(this.transform);
            obj.transform.Find("Particle System").gameObject.SetActive(false);
        }
        else if (item == StoredItem.Spores)
        {
            storage.stockSpores += 1;
            storage.stored += 1;
            gameHandler.AddSporesToStock(1);
            isFull = true;
            GameObject obj = Instantiate(Resources.Load("Vial_Spores")) as GameObject;
            obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z);
            obj.transform.SetParent(this.transform);
        }
    }

    public void RemoveItem(StoredItem item)
    {
        if (item == StoredItem.Spores)
        {
            storage.stockSpores -= 1;
            storage.stored -= 1;
            isFull = false;
            gameHandler.RemoveSporesFromStock(1);
            Destroy(this.transform.GetChild(1).gameObject);
        }
        if (item == StoredItem.Shrooms)
        {
            storage.stockShrooms -= 1;
            storage.stored -= 1;
            isFull = false;
            //gameHandler.RemoveFoodFromStock(1);
            Destroy(this.transform.GetChild(1).gameObject);
        }
    }
}


public enum StoredItem
{
    Empty,
    Shrooms,
    Spores,
}
