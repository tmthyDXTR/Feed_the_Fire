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
        sliderMax = ResourceBank.fireLifeMax;
        sliderNewMax = sliderMax;

        slider = transform.Find("Slider").GetComponent<Slider>();
        slider.maxValue = sliderMax;
        slider.value = sliderMax;

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
        ResourceBank.fireLifeMax = (int)slider.value;
    }
}
