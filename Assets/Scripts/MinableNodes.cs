using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinableNodes : MonoBehaviour
{
    public List<Collider> minableNodesList = new List<Collider>();



    public List<Collider> GetMinableNodesList()
    {
        return minableNodesList;
    }


    void Start()
    {

    }


    void Update()
    {
        minableNodesList.RemoveAll(Collider => Collider == null);
    }
}
