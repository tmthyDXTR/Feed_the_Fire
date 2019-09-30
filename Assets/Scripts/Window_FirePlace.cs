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
    

    void Awake()
    {
        sliderMax = gameHandler.fireLifeFull;
        sliderNewMax = sliderMax;

        slider = transform.Find("Slider").GetComponent<Slider>();
        slider.maxValue = gameHandler.fireLifeFull;
        slider.value = gameHandler.fireLifeMax;

        slider.onValueChanged.AddListener(delegate { UpdateSliderText(); });

        sliderText = transform.Find("SliderValue/SliderValueText").GetComponent<Text>();
        sliderText.text = slider.value.ToString();
        //transform.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    private void UpdateSliderText()
    {
        //Debug.Log("SliderValue Changed");
        sliderText.text = slider.value.ToString();
        gameHandler.fireLifeMax = (int)slider.value;
    }


}
