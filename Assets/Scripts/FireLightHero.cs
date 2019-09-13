using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLightHero : MonoBehaviour
{
    public Light lt;
    public SimpleFogOfWar.FogOfWarInfluence fow;
    public SphereCollider collider;
    public float originalRange;
    public float min = 0.95f;
    public float max = 1.03f;
    private float minFlicker;
    private float maxFlicker;
    float originalViewDistance;


    void Awake()
    {
        lt = GetComponent<Light>();
        originalRange = lt.range;

        fow = GetComponent<SimpleFogOfWar.FogOfWarInfluence>();
        originalViewDistance = fow.ViewDistance;

        collider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        UpdateLightRange();
        //UpdateCollider();
    }

    //void UpdateCollider()
    //{
    //    collider.radius = (float)10 *
    //        ((float)ResourceBank.fireLife / (float)ResourceBank.fireLifeFull);
    //    if (collider.radius >= originalRange)
    //    {
    //        lt.range = originalRange;
    //    }
    //    else if(ResourceBank.fireLife < 5)
    //    {
    //        collider.enabled = false;
    //    }
    //    else if(ResourceBank.fireLife >= 5)
    //    {
    //        collider.enabled = true;
    //    }
    //}

    void UpdateLightRange()
    {
        //lt.range = 18 + (float)originalRange *
        //    ((float)ResourceBank.fireLife / (float)ResourceBank.fireLifeFull);
        //if (lt.range >= originalRange)
        //{
        //    lt.range = originalRange;
        //}

        float newRange = (float)originalRange * ((float)ResourceBank.fireLife / (float)ResourceBank.fireLifeFull);
        lt.range = (float)originalRange * ((float)ResourceBank.fireLife / (float)ResourceBank.fireLifeFull);
        UpdateViewDistance();
    }

    void UpdateViewDistance()
    {
        fow.ViewDistance = originalViewDistance * ((float)ResourceBank.fireLife / (float)ResourceBank.fireLifeFull);
        //fow.ViewDistance = lt.range;
        if (fow.ViewDistance >= originalViewDistance)
        {
            fow.ViewDistance = originalViewDistance;
        }
    }
}