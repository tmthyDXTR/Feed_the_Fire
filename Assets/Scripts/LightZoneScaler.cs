using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LightZoneScaler : MonoBehaviour
{
    [SerializeField] private CapsuleCollider collider;
    [SerializeField] private Projector projector;
    GameHandler gameHandler;
    GameStats gameStats;

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
        gameStats = GameObject.Find("Game").GetComponent<GameStats>();
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();

        collider = GetComponent<CapsuleCollider>();
        projector = transform.Find("Projector").transform.GetChild(0).GetComponent<Projector>();

        gameHandler.OnFireLifeChanged += delegate (object sender, EventArgs e)
        {
            CheckLightLevel();
        };

        CheckLightLevel();
    }

    private void OnDestroy()
    {
        gameHandler.OnFireLifeChanged -= delegate (object sender, EventArgs e)
        {
            CheckLightLevel();
        };
    }

    private void CheckLightLevel()
    {
        fireLifePercent = (float)gameStats.fireLife / (float)gameStats.fireLifeFull;
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
        collider = GetComponent<CapsuleCollider>();
        projector = transform.Find("Projector").transform.GetChild(0).GetComponent<Projector>();
        if (lightLevel == 4)
        {
            if (collider != null)
            {
                collider.radius = col_4;
            }
            if (projector != null)
            {
                projector.fieldOfView = proj_4;
            }
        }
        else if (lightLevel == 3)
        {
            if (collider != null)
            {
                collider.radius = col_3;
            }
            if (projector != null)
            {
                projector.fieldOfView = proj_3;
            }
        }
        else if (lightLevel == 2)
        {
            if (collider != null)
            {
                collider.radius = col_2;
            }
            if (projector != null)
            {
                projector.fieldOfView = proj_2;
            }
        }
        else if (lightLevel == 1)
        {
            if (collider != null)
            {
                collider.radius = col_1;
            }
            if (projector != null)
            {
                projector.fieldOfView = proj_1;
            }
        }
        Debug.Log("Fire: " + gameStats.fireLife + "-" + "Light Level: " + lightLevel);
    }
}
