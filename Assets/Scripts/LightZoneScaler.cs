using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LightZoneScaler : MonoBehaviour
{
    [SerializeField] private CapsuleCollider collider;
    [SerializeField] private Projector projector;

    float col_1 = 4.5f;
    float col_2 = 6.33f;
    float col_3 = 8.14f;
    float col_4 = 10f;

    float proj_1 = 50.43f;
    float proj_2 = 66.66f;
    float proj_3 = 80.5f;
    float proj_4 = 92.5f;
    private float fireLifePercent;
    [SerializeField] private int lightLevel;




    void Awake()
    {
        collider = GetComponent<CapsuleCollider>();
        projector = transform.Find("Projector").transform.GetChild(0).GetComponent<Projector>();

        ResourceBank.OnFireLifeChanged += delegate (object sender, EventArgs e)
        {
            CheckLightLevel();
        };

        CheckLightLevel();
    }

    private void CheckLightLevel()
    {
        fireLifePercent = (float)ResourceBank.fireLife / (float)ResourceBank.fireLifeFull;
        //if (fireLifePercent >= 0.75f)
        //{
        //    UpdateLightZone();
        //}
        if (fireLifePercent < 0.75f && fireLifePercent >= 0.5f)
        {
            lightLevel = 3;
            UpdateLightZone(lightLevel);
        }
        else if (fireLifePercent < 0.5f && fireLifePercent >= 0.25f)
        {
            lightLevel = 2;
            UpdateLightZone(lightLevel);
        }
        else if (fireLifePercent < 0.25f && fireLifePercent > 0f)
        {
            lightLevel = 1;
            UpdateLightZone(lightLevel);
        }
        else
        {
            lightLevel = 4;
            UpdateLightZone(lightLevel);
        }
    }

    private void UpdateLightZone(int lightLevel)
    {
        if (lightLevel == 4)
        {
            collider.radius = col_4;
            projector.fieldOfView = proj_4;
        }
        else if (lightLevel == 3)
        {
            collider.radius = col_3;
            projector.fieldOfView = proj_3;
        }
        else if (lightLevel == 2)
        {
            collider.radius = col_2;
            projector.fieldOfView = proj_2;
        }
        else if (lightLevel == 1)
        {
            collider.radius = col_1;
            projector.fieldOfView = proj_1;
        }
        Debug.Log("Fire: " + ResourceBank.fireLife + "-" + "Light Level: " + lightLevel);
    }
}
