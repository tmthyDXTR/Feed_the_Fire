using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    public Enemy enemy;
    [SerializeField] public string name;
    [SerializeField] public float health;
    [SerializeField] public float currentHealth;

    void Awake()
    {
        health = enemy.health;
        currentHealth = health;
        name = enemy.name;

    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(name + " took " + amount + "Damage");

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
        Debug.Log(name + " died");
        SelectionManager selectionManager = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        if (selectionManager.selection.Contains(this.gameObject))
        {
            selectionManager.DeselectAll();
            //selectionManager.selection.Clear();
        }
        if (this.gameObject.name == "DeadWoodDweller(Clone)")
        {
            gameStats.AddKillCounter(1);
            GameObject bloodSplatter = Instantiate(Resources.Load("PS_BloodSplatter")) as GameObject;
            bloodSplatter.transform.position = this.transform.position + new Vector3(0, 0.4f, 0);
            Destroy(this.gameObject);
        }
        if (this.gameObject.name == "DeadWoodOmen(Clone)")
        {
            gameStats.AddKillCounter(1);
            GameObject bloodSplatter = Instantiate(Resources.Load("PS_BloodSplatter")) as GameObject;
            bloodSplatter.transform.position = this.transform.position + new Vector3(0, 0.4f, 0);
            bloodSplatter.transform.localScale *= 2f;
            Destroy(this.gameObject);
        }
        if (this.gameObject.name == "BigBoy")
        {
            gameStats.AddKillCounter(1);

            GameObject bloodSplatter = Instantiate(Resources.Load("PS_BloodSplatter")) as GameObject;
            bloodSplatter.transform.position = this.transform.position + new Vector3(0, 0.4f, 0);

        }
    }
    
}
