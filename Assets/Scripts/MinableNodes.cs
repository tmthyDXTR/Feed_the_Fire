using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinableNodes : MonoBehaviour
{
    public List<Collider> minableNodesList = new List<Collider>();
    public List<Collider> shroomGrowList = new List<Collider>();
    public List<Collider> selectionNodes = new List<Collider>();



    public List<Collider> GetMinableNodesList()
    {
        return minableNodesList;
    }
    public List<Collider> GetSelectionNodes()
    {
        return selectionNodes;
    }


    public void RegisterNode()
    {

    }

    public void DeregisterNode()
    {

    }

    void Start()
    {

    }


    void Update()
    {
        //minableNodesList.RemoveAll(Collider => Collider == null);
    }
}
