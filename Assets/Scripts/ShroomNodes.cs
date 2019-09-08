using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShroomNodes : MonoBehaviour
{
    public int shroomAmount = 1;
    public int currentAmount;
    public int sporesStock = 0;
    //public GameObject stumpPrefab;

    public bool isMinable = false;
    bool isDead;
    CapsuleCollider capsuleCollider;
    GrowShroom growShroom;

    void Awake()
    {
        // Setting the current health when the enemy first spawns.
        isMinable = false;
        currentAmount = shroomAmount;
        capsuleCollider = GetComponent<CapsuleCollider>();
        growShroom = GetComponent<GrowShroom>();
    }

    void Update()
    {

    }

    private void DestroyGameObject()
    {
        Destroy(this.transform.GetChild(1).gameObject);
    }

    public void TakeDamage()
    {
        // If the enemy is dead...
        if (isDead)
            // ... no need to take damage so exit the function.
            return;

        // Reduce the current health by the amount of damage sustained.
        currentAmount -= 1;

        // If the current health is less than or equal to zero...
        if (currentAmount <= 0)
        {
            // ... the enemy is dead.
            Death();
        }
    }

    public void SetMinable()
    {
        if (isMinable == false)
        {
            isMinable = true;
        }
    }

    void Death()
    {
        //isDead = true;
        growShroom.hasSpores = false;
        growShroom.hasShrooms = false;
        growShroom.hasSporeDrop = true;
        sporesStock += 1;
        //Instantiate(stumpPrefab, new Vector3(
        //    this.transform.position.x,
        //    this.transform.position.y,
        //    this.transform.position.z), Quaternion.identity);
        Destroy(this.transform.GetChild(1).gameObject);

        GameObject spores = Instantiate(Resources.Load("Vial_Spores")) as GameObject;
        spores.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        spores.transform.SetParent(this.transform);
    }
}