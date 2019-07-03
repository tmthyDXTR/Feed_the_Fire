using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningArea : MonoBehaviour
{



    [SerializeField] private int sphereRadius = 15;
    public Collider[] miningNodes;
    private LayerMask sphereLayerMask;


    public void SetNodesToMinable(string nodeLayer)
    {

        sphereLayerMask = LayerMask.GetMask(nodeLayer);
        int nodeCounter = 0;
        Collider[] nodeColliders = Physics.OverlapSphere(transform.position, sphereRadius, sphereLayerMask);
        foreach (Collider node in nodeColliders)
        {
            if (node.gameObject.tag == "WorkActive")
            {
                node.gameObject.tag = "WorkInactive";
                nodeCounter += 1;
            }
        }
        Debug.Log(nodeCounter + " " + nodeLayer + " set to Minable");
    }


    void Awake()
    {
        
    }

    void Update()
    {

    }
   
    
}
