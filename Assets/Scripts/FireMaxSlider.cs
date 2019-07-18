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
        fireSlider.maxValue = ResourceBank.fireLifeFull;
        fireSlider.value = ResourceBank.fireLifeFull;
    }

    void Update()
    {
        ResourceBank.fireLifeMax = (int)fireSlider.value;
    }
}
