using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInfo : MonoBehaviour
{
    public int power = 0;
    public float health;
    public float currentHealth;

    void Awake()
    {
        
    }

    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        //if (currentHealth <= 0f)
        //{
        //    Death();
        //}
    }

    private void Death()
    {
        //Destroy(this.transform.Find("HitBox").gameObject);
    }
}
