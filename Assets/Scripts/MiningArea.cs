using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningArea : MonoBehaviour
{
    [SerializeField] public int sphereRadius;
    public List<Collider> miningNodes = new List<Collider>();

    [SerializeField] private MinableNodes minableNodes;
    private ChangeColour changeColour;

    private LayerMask sphereLayerMask;
    public Material originalMaterial;
    public Material newMaterial;
    private SphereCollider collider;

    void Awake()
    {
        collider = GetComponent<SphereCollider>();
        collider.radius = sphereRadius;
        minableNodes = GameObject.Find("MinableNodes").GetComponent<MinableNodes>();        
    }


    public void SetNodesToMinable(string nodeLayer)
    {
        sphereRadius = (int)collider.radius;
        sphereLayerMask = LayerMask.GetMask(nodeLayer);
        int nodeCounter = 0;
        Collider[] nodeColliders = Physics.OverlapSphere(transform.position, sphereRadius, sphereLayerMask);
        foreach (Collider node in nodeColliders)
        {
            if (node.gameObject.tag == "WorkActive")
            {
                node.gameObject.tag = "WorkInactive";
                nodeCounter += 1;
                minableNodes.minableNodesList.Add(node);
            }                            
        }
        Debug.Log(nodeCounter + " " + nodeLayer + " set to Minable");
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "WorkActive" || other.tag == "WorkInactive")
        {
            changeColour = other.GetComponent<ChangeColour>();
            if (!minableNodes.minableNodesList.Contains(other))
            {
                minableNodes.selectionNodes.Add(other);
                changeColour.ChangeToMinableMat();
            }
            
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.tag == "WorkActive" || other.tag == "WorkInactive")
        {
            changeColour = other.GetComponent<ChangeColour>();
            changeColour.ChangeToOriginalMat();
            minableNodes.selectionNodes.Remove(other);
        }
    }

    public void ShowMinableNodes()
    {
        foreach (Collider node in minableNodes.minableNodesList)
        {
            if (node != null)
            {
                changeColour = node.GetComponent<ChangeColour>();
                if (changeColour != null)
                {
                    changeColour.ChangeToMinableMat();
                }
            }            
        }
        //foreach (Collider node in miningNodes)
        //{
        //    changeColour = node.GetComponent<ChangeColour>();
        //    if (changeColour != null)
        //    {
        //        changeColour.ChangeToMinableMat();
        //    }
        //}
    }

    public void HideMinableNodes()
    {
        foreach (Collider node in minableNodes.minableNodesList)
        {
            if (node != null)
            {
                changeColour = node.GetComponent<ChangeColour>();
                changeColour.ChangeToOriginalMat();
            }            
        }
        foreach (Collider node in minableNodes.selectionNodes)
        {
            if (node != null)
            {
                changeColour = node.GetComponent<ChangeColour>();
                changeColour.ChangeToOriginalMat();
            }            
        }
        minableNodes.selectionNodes.Clear();
    }



    void Update()
    {

    }
   
    
}
