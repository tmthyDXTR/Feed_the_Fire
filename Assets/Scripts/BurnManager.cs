using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnManager : MonoBehaviour
{
    public List<GameObject> burnList = new List<GameObject>();

    MinableNodes mineableNodes;


    public void RegisterBurn(GameObject objToBurn)
    {
        burnList.Add(objToBurn);
        mineableNodes.shroomGrowList.Remove(objToBurn.GetComponent<Collider>());
    }

    public void DeregisterBurn(GameObject objToBurn)
    {
        burnList.Remove(objToBurn);
    }

    void Awake()
    {
        mineableNodes = GameObject.Find("MinableNodes").GetComponent<MinableNodes>();
    }

    void Update()
    {
        
    }
}
