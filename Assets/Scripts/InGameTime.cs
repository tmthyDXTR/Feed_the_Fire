using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameTime : MonoBehaviour
{
    private TextMeshProUGUI timeText;
    private float startTime;

    void Awake()
    {
        startTime = Time.time;
        timeText = transform.Find("TimeText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (timeText == null)
        {
            timeText = transform.Find("TimeText").GetComponent<TextMeshProUGUI>();

        }
        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = ((int)(t % 60)).ToString();

        if (seconds.Length < 2)
        {
            timeText.text = minutes + ":0" + seconds;
        }
        else
        {
            timeText.text = minutes + ":" + seconds;
        }
    }
}
