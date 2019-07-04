using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningArea : MonoBehaviour
{
    [SerializeField] private int sphereRadius = 30;
    public List<Collider> miningNodes = new List<Collider>();
    private LayerMask sphereLayerMask;

    public Material originalMaterial;
    public Material newMaterial;


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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WorkActive" || other.tag == "WorkInactive")
        {
            originalMaterial = other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material;
            miningNodes.Add(other);
            ChangeToMinableMat(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "WorkActive" || other.tag == "WorkInactive")
        {
            miningNodes.Remove(other);
            ChangeToOriginalMat(other);
        }
    }

    public void ChangeToMinableMat(Collider node)
    {
        
        node.gameObject.transform.GetChild(0).GetComponent<Renderer>().material = newMaterial;
    }

    public void ChangeToOriginalMat(Collider node)
    {
        node.gameObject.transform.GetChild(0).GetComponent<Renderer>().material = originalMaterial;
    }

    public void ShowMinableNodes()
    {

    }

    public void HideMinableNodes()
    {
        foreach (Collider node in miningNodes)
        {
            ChangeToOriginalMat(node);
        }
    }

    void Awake()
    {

    }

    void Update()
    {

    }
   
    
}
