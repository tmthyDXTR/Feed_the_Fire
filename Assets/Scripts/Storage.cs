﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Storage : MonoBehaviour
{
    public bool isFull = false;
    public bool isEmpty = true;
    public int storageRoom = 10;
    public int stored = 0;

    public int stockShrooms = 0;
    public int stockSpores = 0;

    public SingleStorage[] singleStorage; 



    void Awake()
    {
        //ResourceBank.OnFireLifeChanged += delegate (object sender, EventArgs e)
        //{
        //    UpdateFireLifeTextObject();
        //};

        //singleStorage = GetComponentsInChildren<SingleStorage>();
        //foreach (SingleStorage store in singleStorage)
        //{
        //    store.OnShroomsAdded += delegate (object sender, EventArgs e)
        //    {
        //        Debug.Log("Shroom Added");
        //        stockShrooms += 1;
        //        stored += 1;
        //    };
        //}
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



    public void Store(StoredItem item)
    {
        foreach (Transform child in this.transform)
        {
            SingleStorage storage = child.gameObject.GetComponent<SingleStorage>();
            if (storage.isFull == true)
            {
                continue;
            }
            else
            {           
                storage.storedItem = item;
                storage.AddItem(item);
                break;                            
            }
        }
    }

    public void Collect(StoredItem item)
    {
        foreach (Transform child in this.transform)
        {
            SingleStorage storage = child.gameObject.GetComponent<SingleStorage>();
            if (storage.isFull == false)
            {
                continue;
            }
            else if (storage.storedItem == item)
            {
                //stored -= 1;
                //stockSpores -= 1;
                
                storage.storedItem = StoredItem.Empty;
                storage.RemoveItem(item);
                break;
            }
        }
    }
}