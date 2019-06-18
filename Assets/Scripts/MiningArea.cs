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

   

    #region Monobehaviour API

    void Awake()
    {
        sphereLayerMask = LayerMask.GetMask("TreeNodes");
        Collider[] nodeColliders = Physics.OverlapSphere(transform.position, sphereRadius, sphereLayerMask);
        foreach (Collider node in nodeColliders)
        {
            Debug.Log("Colider MINING AREA: " + node.ToString());
            if (node.gameObject.tag == "WorkActive") 
            {
                node.gameObject.tag = "WorkInactive";
            }
           /* else
            {
                node.gameObject.tag = "WorkActive";
            }*/
            
        }
    }

    void Update()
    {


    }
    #endregion
    /*
    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.tag == "WorkActive")
        {
            setMinable();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "WorkActive")
        {
            setMinable();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "WorkInactive")
        {
            setNotMinable();
        }
    }

    

    public void setMinable()
    {
        minable = true;
    }

    public void setNotMinable()
    {
        minable = false;
    }
    */
    
}
