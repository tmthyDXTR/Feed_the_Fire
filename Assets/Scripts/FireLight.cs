using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLight : MonoBehaviour
{
    public Light lt;
    public float originalRange = 250f;

    void Awake()
    {
        lt = GetComponent<Light>();
        originalRange = lt.range;

    }

    void Update()
    {
        lt.range = 18 + (float)originalRange *
            ((float)ResourceBank.GetFireLife() / (float)ResourceBank.fireLifeFull);

    }
}