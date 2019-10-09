using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Window_FirePlace : MonoBehaviour
{
    private Slider slider;
    private int sliderMax;
    private int sliderNewMax;

    private Text sliderText;
    private GameStats gameStats;
    private GameHandler gameHandler;
    void Awake()
    {
        gameStats = GameObject.Find("Game").GetComponent<GameStats>();

        sliderMax = gameStats.fireLifeFull;
        sliderNewMax = sliderMax;

        slider = transform.Find("Slider").GetComponent<Slider>();
        slider.maxValue = gameStats.fireLifeFull;
        slider.value = gameStats.fireLifeMax;

        //slider.onValueChanged.AddListener(delegate { UpdateSliderText(); });

        sliderText = transform.Find("SliderValue/SliderValueText").GetComponent<Text>();
        sliderText.text = slider.value.ToString();
        //transform.gameObject.SetActive(false);
        slider.onValueChanged.AddListener(UpdateSliderText);
    }
    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(UpdateSliderText);


    }

    void Update()
    {
        //gameStats.fireLifeMax = (int)slider.value;

    }

    private void UpdateSliderText(float i)
    {
        Debug.Log("SliderValue Changed");
        sliderText.text = slider.value.ToString();
        gameStats.fireLifeMax = (int)slider.value;
    }


}
