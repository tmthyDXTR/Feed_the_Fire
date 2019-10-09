using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MiningTooltip : MonoBehaviour
{
    MinableNodes minable;
    Tooltip tooltip;
    private SelectionManager selection;
    private TextMeshProUGUI tmpName;
    private TextMeshProUGUI tmpInfo;
    public string text;
    public string info;
    void Start()
    {
        minable = GameObject.Find("MinableNodes").GetComponent<MinableNodes>();
        tooltip = this.GetComponent<Tooltip>();
        selection = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        tmpName = this.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        tmpName.text = text;
        tmpInfo = this.transform.Find("Info").GetChild(0).GetComponent<TextMeshProUGUI>();
        //tmpInfo.text = info;
    }

    void Update()
    {
        if (selection.selection[0].tag == "MiningArea")
        {
            tmpInfo.text = minable.minableNodesList.Count.ToString() + " + " + minable.selectionNodes.Count.ToString();

        }
        if (selection.selection[0].tag == "ShroomArea")
        {
            tmpInfo.text = minable.shroomGrowList.Count.ToString() + " + " + minable.selectionNodes.Count.ToString();

        }
        this.transform.position = Input.mousePosition;
    }



}
