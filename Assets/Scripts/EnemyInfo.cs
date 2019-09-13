using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log(name + " took " + amount + "Damage");
        if (currentHealth <= 0)
        {
            Death();
        }
    }
    private void Death()
    {
        Debug.Log(name + " died");
        Destroy(this.gameObject);
    }
    
}
