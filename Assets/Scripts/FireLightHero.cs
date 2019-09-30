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
    public float originalViewDistance;

    private HeroInfo hero;

    void Awake()
    {
        lt = GetComponent<Light>();
        lt.range = originalRange;

        fow = GetComponent<SimpleFogOfWar.FogOfWarInfluence>();
        fow.ViewDistance = originalViewDistance;

        collider = GetComponent<SphereCollider>();
        hero = GameObject.Find("Hero").GetComponent<HeroInfo>();
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

        //float newRange = (float)originalRange * ((float)ResourceBank.fireLife / (float)ResourceBank.fireLifeFull);
        //lt.range = (float)originalRange * ((float)ResourceBank.fireLife / (float)ResourceBank.fireLifeFull);

        lt.range = (float)originalRange / 40 * hero.power;
        UpdateViewDistance();
    }

    void UpdateViewDistance()
    {
        fow.ViewDistance = (float)originalViewDistance / 30 * hero.power;
        if (fow.ViewDistance < 25)
        {
            fow.ViewDistance = 25;
        }
        if (fow.ViewDistance > 100)
        {
            fow.ViewDistance = 100;
        }
        //if (fow.ViewDistance >= originalViewDistance)
        //{
        //    fow.ViewDistance = originalViewDistance;
        //}
    }
}