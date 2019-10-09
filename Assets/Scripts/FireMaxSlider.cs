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
        fireSlider.maxValue = gameStats.fireLifeFull;
        fireSlider.value = gameStats.fireLifeFull;
    }

    void Update()
    {
        gameStats.fireLifeMax = (int)fireSlider.value;
    }
}
