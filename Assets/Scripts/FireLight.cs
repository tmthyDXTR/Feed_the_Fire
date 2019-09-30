using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLight : MonoBehaviour
{
    public Light lt;
    public SimpleFogOfWar.FogOfWarInfluence fow;
    public SphereCollider collider;
    public float originalRange;
    public float min = 0.95f;
    public float max = 1.03f;
    private float minFlicker;
    private float maxFlicker;
    GameStats gameStats;


    void Awake()
    {
        lt = GetComponent<Light>();
        lt.range = originalRange;
        
        fow = GetComponent<SimpleFogOfWar.FogOfWarInfluence>();

        collider = GetComponent<SphereCollider>();

        gameStats = GameObject.Find("Game").GetComponent<GameStats>();

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

        float newRange = (float)originalRange * ((float)gameStats.fireLife / (float)gameStats.fireLifeFull);
        lt.range = (float)originalRange * ((float)gameStats.fireLife / (float)gameStats.fireLifeFull);
        UpdateViewDistance();
    }

    void UpdateViewDistance()
    {
        fow.ViewDistance = lt.range + 40;
        //fow.ViewDistance = lt.range;
        if (fow.ViewDistance >= originalRange)
        {
            fow.ViewDistance = originalRange;
        }
    }
}