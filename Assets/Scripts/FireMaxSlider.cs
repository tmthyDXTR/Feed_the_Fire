using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireMaxSlider : MonoBehaviour
{
    public Slider fireSlider;

    void Awake()
    {
        fireSlider.wholeNumbers = true;
        fireSlider.minValue = 0;
        fireSlider.maxValue = gameHandler.fireLifeFull;
        fireSlider.value = gameHandler.fireLifeFull;
    }

    void Update()
    {
        gameHandler.fireLifeMax = (int)fireSlider.value;
    }
}
