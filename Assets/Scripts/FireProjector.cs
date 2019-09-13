using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjector : MonoBehaviour
{
    private ProjectorManager projectorManager;

    void Awake()
    {
        projectorManager = GameObject.Find("SelectionManager").GetComponent<ProjectorManager>();
        if (projectorManager.projectorList.Contains(this.gameObject) )
        {
            Debug.Log("Projector already in List");
        }
        else
        {
            projectorManager.projectorList.Add(this.gameObject);
        }
    }

    void Update()
    {
        
    }
}
