using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorManager : MonoBehaviour
{
    public List<GameObject> projectorList = new List<GameObject>();


    internal void Reset()
    {
        projectorList.Clear();
    }
}
