using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLight : MonoBehaviour
{
    public Light lt;
    public SimpleFogOfWar.FogOfWarInfluence fow;
    public SphereCollider collider;
    public float originalRange = 150f;
    public float min = 0.95f;
    public float max = 1.03f;
    private float minFlicker;
    private float maxFlicker;

    void Awake()
    {
        lt = GetComponent<Light>();
        lt.range = originalRange;
        
        fow = GetComponent<SimpleFogOfWar.FogOfWarInfluence>();

        collider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        UpdateLightRange();
        UpdateViewDistance();
        UpdateCollider();
    }

    void UpdateCollider()
    {
        collider.radius = (float)10 *
            ((float)ResourceBank.fireLife / (float)ResourceBank.fireLifeFull);
        if (collider.radius >= originalRange)
        {
            lt.range = originalRange;
        }
        else if(ResourceBank.fireLife < 5)
        {
            collider.enabled = false;
        }
        else if(ResourceBank.fireLife >= 5)
        {
            collider.enabled = true;
        }
    }

    void UpdateLightRange()
    {
        //lt.range = 18 + (float)originalRange *
        //    ((float)ResourceBank.fireLife / (float)ResourceBank.fireLifeFull);
        //if (lt.range >= originalRange)
        //{
        //    lt.range = originalRange;
        //}

        lt.range = (float)originalRange * ((float)ResourceBank.fireLife / (float)ResourceBank.fireLifeFull);
    }

    void UpdateViewDistance()
    {
        fow.ViewDistance = lt.range + 50;
        if (fow.ViewDistance >= originalRange)
        {
            fow.ViewDistance = originalRange;
        }
    }
}