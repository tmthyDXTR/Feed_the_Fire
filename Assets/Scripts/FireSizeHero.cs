using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSizeHero : MonoBehaviour
{
    public ParticleSystem ps;
    public float pSize = 1.0f;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();        
    }

    void Update()
    {
        // FireLife >= 75% -- Fire Level 4
        if ((float)ResourceBank.fireLife >= ((float)ResourceBank.fireLifeFull * 0.75f))
        {
            var main = ps.main;
            main.startSize = new ParticleSystem.MinMaxCurve(pSize, pSize);
        }
        // 75% > FireLife >= 50% -- Fire Level 3
        if (ResourceBank.fireLife < (ResourceBank.fireLifeFull * 0.75f) &&
            ResourceBank.fireLife >= (ResourceBank.fireLifeFull * 0.50f))
        {
            var main = ps.main;
            main.startSize = new ParticleSystem.MinMaxCurve((pSize*0.7f), (pSize * 0.7f));
        }
        // 50% > FireLife >= 25% -- Fire Level 2
        if (ResourceBank.fireLife < (ResourceBank.fireLifeFull * 0.50f) &&
            ResourceBank.fireLife >= (ResourceBank.fireLifeFull * 0.25f))
        {
            var main = ps.main;
            main.startSize = new ParticleSystem.MinMaxCurve((pSize * 0.4f), (pSize * 0.4f));
        }
        // 25% > FireLife > 0% -- Fire Level 1
        if (ResourceBank.fireLife < (ResourceBank.fireLifeFull * 0.25f) &&
            ResourceBank.fireLife >= (ResourceBank.fireLifeFull * 0.001f))
        {
            var main = ps.main;
            main.startSize = new ParticleSystem.MinMaxCurve((pSize * 0.15f), (pSize * 0.15f));
        }
        // FireLife = 0% -- Fire Level 0
        if (ResourceBank.fireLife == 0)
        {
            var main = ps.main;
            main.startSize = new ParticleSystem.MinMaxCurve((pSize * 0.01f), (pSize * 0.02f));
        }
                     
    }
}