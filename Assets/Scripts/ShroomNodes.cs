using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShroomNodes : MonoBehaviour
{
    public int shroomAmount = 0;
    public int sporesAmount = 0;
    //public GameObject stumpPrefab;

    public bool isMinable = false;
    bool isDead;
    CapsuleCollider capsuleCollider;
    GrowShroom growShroom;

    void Awake()
    {
        // Setting the current health when the enemy first spawns.
        isMinable = false;
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

    public void Remove(string item)
    {
        // If the enemy is dead...
        if (isDead)
            // ... no need to take damage so exit the function.
            return;

        if (item == "Shrooms")
        {
            // Reduce the current health by the amount of damage sustained.
            shroomAmount -= 1;

            // If the current health is less than or equal to zero...
            if (shroomAmount <= 0)
            {
                // ... the enemy is dead.
                Death("Shrooms");
            }
        }
        else if (item == "Spores")
        {
            // Reduce the current health by the amount of damage sustained.
            sporesAmount -= 1;

            // If the current health is less than or equal to zero...
            if (sporesAmount <= 0)
            {
                // ... the enemy is dead.
                Death("Spores");
            }
        }
        
    }

    public void SetMinable()
    {
        if (isMinable == false)
        {
            isMinable = true;
        }
    }

    void Death(string item)
    {
        if (item == "Shrooms")
        {
            //isDead = true;
            growShroom.hasSpores = false;
            growShroom.hasShrooms = false;
            growShroom.hasSporesDrop = true;
            sporesAmount += 1;
            //Instantiate(stumpPrefab, new Vector3(
            //    this.transform.position.x,
            //    this.transform.position.y,
            //    this.transform.position.z), Quaternion.identity);
            Destroy(this.transform.GetChild(1).gameObject);

            GameObject spores = Instantiate(Resources.Load("Vial_Spores")) as GameObject;
            spores.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
            spores.transform.SetParent(this.transform);
        }
        else if (item == "Spores")
        {
            growShroom.hasSporesDrop = false;
            Destroy(this.transform.GetChild(1).gameObject);
        }
    }
}