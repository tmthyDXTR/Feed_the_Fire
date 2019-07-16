using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    public List<Collider> colliders = new List<Collider>();


    void Start()
    {
        
    }


    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.layer == 15 || c.gameObject.layer == 14 || c.gameObject.layer == 13 || c.gameObject.layer == 10 || c.gameObject.layer == 9)
        {
            colliders.Add(c);
        }
    }
    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.layer == 15 || c.gameObject.layer == 14 || c.gameObject.layer == 13 || c.gameObject.layer == 10 || c.gameObject.layer == 9)
        {
            colliders.Remove(c);
        }
    }

}
