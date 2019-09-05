using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    public List<GameObject> constructionList = new List<GameObject>();


    public void RegisterConstruction(GameObject construction)
    {
        constructionList.Add(construction);
    }

    public void DeregisterConstruction(GameObject construction)
    {
        constructionList.Remove(construction);
    }

    void Awake()
    {

    }

    void Update()
    {
        
    }
}
