using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningArea : MonoBehaviour
{

    #region Attributes

    private Collider other;
    public bool minable = false;
    [SerializeField] private int sphereRadius;
    private LayerMask sphereLayerMask;
    public Collider[] nodeColliders;
    #endregion

    private void SetNodesToMinable(string nodeLayer)
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

    }

    #region Monobehaviour API

    void Awake()
    {
        SetNodesToMinable("TreeNodes");
        SetNodesToMinable("StoneNodes");       
    }

    void Update()
    {


    }
    #endregion
    
    
}
