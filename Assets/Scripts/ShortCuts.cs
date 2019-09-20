using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortCuts : MonoBehaviour
{
    private SelectionManager selection;

    void Awake()
    {
        selection = GetComponent<SelectionManager>();
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
    }
}
