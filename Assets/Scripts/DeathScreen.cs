using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    Image image;
    private float targetAlpha = 1f;
    public float FadeRate;
    public float FadeRateText;
    [SerializeField] TextMeshProUGUI text;
    void Awake()
    {
        image = GetComponent<Image>();
        text = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {        
        Color curColor = image.color;
        Color textColor = text.color;

        float alphaDiff = Mathf.Abs(curColor.a - this.targetAlpha);
        if (alphaDiff > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, FadeRate * Time.deltaTime);
            textColor.a = Mathf.Lerp(curColor.a, targetAlpha, FadeRateText * Time.deltaTime);

            text.color = textColor;
            image.color = curColor;
        }             
    
    }
}
