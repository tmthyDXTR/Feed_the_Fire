using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInfo : MonoBehaviour
{
    public Tree tree;

    public string name;
    public float health;
    public float currentHealth;

    void Awake()
    {
        name = tree.name;
        health = tree.health;
        currentHealth = health;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Debug.Log(name + " died");
        Destroy(gameObject);
    }
}
