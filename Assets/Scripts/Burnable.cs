using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Burnable : MonoBehaviour
{
    public bool isBurning = false;
    public bool isTargeted = false;
    private float burnTime = 0.0f;
    public static float burnSpeed = 20f; //-- Seconds to lose 1 Life
    [SerializeField] private GameObject burnEffectObj;
    private SelectionManager selectionManager;


    public EventHandler OnBurning;

    void Awake()
    {
        isBurning = false;
        selectionManager = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();

    }

    void Update()
    {
        if (isBurning == true)
        {
            burnTime += Time.deltaTime;
            if (burnTime >= burnSpeed)
            {
                selectionManager.selection.Remove(this.gameObject);
                Destroy(gameObject);
                Destroy(burnEffectObj);
            }
        }
    }

    public void AddBurnEffect()
    {
        GameObject burnEffect = Instantiate(Resources.Load("BurnEffect")) as GameObject;
        burnEffectObj = burnEffect;
        burnEffect.transform.position = new Vector3(transform.position.x, transform.position.y +0.78f, transform.position.z);
       
    }
}
