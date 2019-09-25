using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortCuts : MonoBehaviour
{
    private SelectionManager selection;
    public GameObject cheatPanel;

    void Start()
    {
        selection = GetComponent<SelectionManager>();
        cheatPanel = GameObject.Find("CheatPanel");
        cheatPanel.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GameObject hero = GameObject.Find("Hero");
            if (hero != null)
            {
                if (hero.GetComponent<SelectableObject>() != null)
                {
                    if (!hero.GetComponent<SelectableObject>().isSelected)
                    {
                        selection.DeselectAll();
                        selection.Select(hero);
                    }
                }
            }            
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (cheatPanel.activeSelf == false)
            {
                cheatPanel.SetActive(true);
            }
            else
            {
                cheatPanel.SetActive(false);
            }
        }
    }
}
