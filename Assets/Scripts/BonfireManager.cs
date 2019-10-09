using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BonfireManager : MonoBehaviour
{
    public List<GameObject> bonfireList = new List<GameObject>();
    public EventHandler OnBonfireAmountChanged;

    private HeroInfo hero;
    void Awake()
    {
        hero = GameObject.Find("Hero").GetComponent<HeroInfo>();
    }

    public void AddBonfire(GameObject bonfire)
    {
        bonfireList.Add(bonfire);
        if (OnBonfireAmountChanged != null) OnBonfireAmountChanged(null, EventArgs.Empty);

        //OnBonfireAmountChanged?.Invoke(null, EventArgs.Empty);
    }
}
