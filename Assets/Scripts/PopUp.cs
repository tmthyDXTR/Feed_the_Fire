using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUp : MonoBehaviour
{
    public string text;

    void Start()
    {
        this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        Destroy(this.gameObject, 1f);
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }
}
