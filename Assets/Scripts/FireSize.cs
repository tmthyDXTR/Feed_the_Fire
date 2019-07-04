using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSize : MonoBehaviour
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
        if ((float)ResourceBank.GetFireLife() >= ((float)ResourceBank.GetFireLifeMax() * 0.75f))
        {
            var main = ps.main;
            main.startSize = new ParticleSystem.MinMaxCurve(pSize, pSize);
        }
        // 75% > FireLife >= 50% -- Fire Level 3
        if (ResourceBank.GetFireLife() < (ResourceBank.GetFireLifeMax() * 0.75f) &&
            ResourceBank.GetFireLife() >= (ResourceBank.GetFireLifeMax() * 0.50f))
        {
            var main = ps.main;
            main.startSize = new ParticleSystem.MinMaxCurve((pSize*0.75f), (pSize * 0.75f));
        }
        // 50% > FireLife >= 25% -- Fire Level 2
        if (ResourceBank.GetFireLife() < (ResourceBank.GetFireLifeMax() * 0.50f) &&
            ResourceBank.GetFireLife() >= (ResourceBank.GetFireLifeMax() * 0.25f))
        {
            var main = ps.main;
            main.startSize = new ParticleSystem.MinMaxCurve((pSize * 0.5f), (pSize * 0.5f));
        }
        // 25% > FireLife > 0% -- Fire Level 1
        if (ResourceBank.GetFireLife() < (ResourceBank.GetFireLifeMax() * 0.25f) &&
            ResourceBank.GetFireLife() >= (ResourceBank.GetFireLifeMax() * 0.001f))
        {
            var main = ps.main;
            main.startSize = new ParticleSystem.MinMaxCurve((pSize * 0.25f), (pSize * 0.25f));
        }
        // FireLife = 0% -- Fire Level 0
        if (ResourceBank.GetFireLife() == 0)
        {
            var main = ps.main;
            main.startSize = new ParticleSystem.MinMaxCurve((pSize * 0.01f), (pSize * 0.02f));
        }
                     
    }
}