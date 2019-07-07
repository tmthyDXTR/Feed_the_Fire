using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningArea : MonoBehaviour
{
    [SerializeField] private int sphereRadius = 30;
    public List<Collider> miningNodes = new List<Collider>();

    [SerializeField] private MinableNodes minableNodes;
    [SerializeField] private ChangeColour changeColour;

    private LayerMask sphereLayerMask;
    public Material originalMaterial;
    public Material newMaterial;


    void Awake()
    {
        minableNodes = GameObject.Find("MinableNodes").GetComponent<MinableNodes>();        
    }


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

            minableNodes.minableNodesList.Add(other);
            changeColour.ChangeToMinableMat();
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.tag == "WorkActive" || other.tag == "WorkInactive")
        {
            changeColour = other.GetComponent<ChangeColour>();

            changeColour.ChangeToOriginalMat();
            minableNodes.minableNodesList.Remove(other);          
        }
    }

    public void ShowMinableNodes()
    {
        foreach (Collider node in minableNodes.GetMinableNodesList())
        {
            changeColour = node.GetComponent<ChangeColour>();
            if (changeColour != null)
            {
                changeColour.ChangeToMinableMat();
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
        foreach (Collider node in minableNodes.GetMinableNodesList())
        {
            changeColour = node.GetComponent<ChangeColour>();
            changeColour.ChangeToOriginalMat();
        }
        //foreach (Collider node in miningNodes)
        //{
        //    changeColour = node.GetComponent<ChangeColour>();
        //    changeColour.ChangeToOriginalMat();
        //}
    }

    

    void Update()
    {

    }
   
    
}
