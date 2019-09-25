using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    public float health;
    public float currentHealth;
    public string job;
    public int invMax = 1;
    public int invWood = 0;
    public int invStone = 0;
    public int invShroom = 0;
    public int invSpores = 0;
    public int invFire = 0;
    public bool isDead = false;
    public Collider target;


    private WorkerUnitAI unitAI;
    private EnemyRoam attacker; // Temp Fix

    void Awake()
    {
        unitAI = GetComponent<WorkerUnitAI>();
    }

    void Update()
    {

    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        //Debug.Log(name + " took " + amount + "Damage");

        CreateDamagePopUp(amount, this.transform.position);
        if (currentHealth <= 0)
        {
            Death();
        }
    }
    private void CreateDamagePopUp(float amount, Vector3 position)
    {
        Transform popUpCanvas = GameObject.Find("PopUpCanvas").transform;
        GameObject damagePopUp = Instantiate(Resources.Load("DamagePopUp")) as GameObject;
        damagePopUp.transform.SetParent(popUpCanvas);
        if (damagePopUp.GetComponent<PopUp>() != null)
        {
            Debug.Log("PopUp Script found");
            damagePopUp.GetComponent<PopUp>().text = amount.ToString();
            damagePopUp.transform.position = position + new Vector3(0, 4f, 0); ;
        }

    }

    private void Death()
    {
        GameObject bloodSplatter = Instantiate(Resources.Load("PS_BloodSplatter")) as GameObject;
        bloodSplatter.transform.position = this.transform.position + new Vector3(0, 0.4f, 0);
        bloodSplatter.transform.GetChild(0).localScale = new Vector3(0.25f, 0.25f, 0.25f);
        isDead = true;
        JobManager jobManager = GameObject.Find("Workers").GetComponent<JobManager>();
        if (jobManager != null)
        {
            jobManager.GetWorkerCounts();
        }
        Destroy(this.gameObject);
    }
}
