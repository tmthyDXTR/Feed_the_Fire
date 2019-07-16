using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public List<GameObject> selectedObjects;
    private void Awake()
    {
        List<GameObject> selectedObjects = new List<GameObject>();
    }
}