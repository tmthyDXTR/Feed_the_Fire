using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    

    private SelectionManager selection;
    private TextMeshProUGUI tmpName;
    private TextMeshProUGUI tmpInfo;
    public string text;
    public string info;
    void Start()
    {
        selection = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        tmpName = this.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        tmpName.text = text;
        tmpInfo = this.transform.Find("Info").GetChild(0).GetComponent<TextMeshProUGUI>();
        tmpInfo.text = info;
    }


}
