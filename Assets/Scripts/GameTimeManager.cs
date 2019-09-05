using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeManager : MonoBehaviour
{
    [SerializeField] private float gameSpeed = 1f;

    void Awake()
    {
        gameSpeed = Time.timeScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if (Time.timeScale < 16.0f)
            {
                Time.timeScale *= 2;
                gameSpeed = Time.timeScale;
                //Debug.Log(Time.timeScale);
            }
            else
            {
                Time.timeScale = 16f;
                gameSpeed = Time.timeScale;
            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            if (Time.timeScale != 0.5f)
            {
                Time.timeScale /= 2f;
                gameSpeed = Time.timeScale;
                //Debug.Log(Time.timeScale);                
            }                
        }
    }
}
