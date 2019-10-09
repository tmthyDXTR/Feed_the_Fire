using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class TreeNodes : MonoBehaviour
{
    public int woodAmount = 24;
    public int currentAmount;
    public GameObject stumpPrefab;
    public GameObject treeFallPrefab;
    public GameObject saplingPrefab;
    public bool isMinable = false;
    public float saplingDropRate = 0.1f;
    bool isDead;
    CapsuleCollider capsuleCollider;

    void Awake()
    {
        // Setting the current health when the enemy first spawns.
        isMinable = false;
        currentAmount = woodAmount;
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {        

    }

    private void DestroyGameObject()
    {
        Destroy(this.gameObject);
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

    public void Death()
    {
        isDead = true;
        GameObject stump = Instantiate(stumpPrefab, new Vector3(
            this.transform.position.x,
            this.transform.position.y,
            this.transform.position.z), Quaternion.identity) as GameObject;
        stump.transform.SetParent(GameObject.Find("StumpNodes").transform);
        Vector3 rotationVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        GameObject treeFall = Instantiate(treeFallPrefab, new Vector3(
            this.transform.position.x,
            this.transform.position.y+2f,
            this.transform.position.z), Quaternion.Euler(rotationVector)) as GameObject;
        treeFall.transform.SetParent(GameObject.Find("StumpNodes").transform);


        // Chance to drop Tree Saplings
        if (Random.value <= saplingDropRate)
        {
            GameObject sapling = Instantiate(saplingPrefab, new Vector3(
            this.transform.position.x,
            this.transform.position.y + 0.5f,
            this.transform.position.z), Quaternion.identity) as GameObject;
            sapling.transform.SetParent(stump.transform);
            stump.GetComponent<GrowShroom>().hasSaplingDrop = true;
            Debug.Log("A Tree left behind a sapling to regrow");
        }

        MinableNodes minableNodes = this.transform.parent.parent.GetComponent<MinableNodes>();
        if (minableNodes.minableNodesList.Contains(this.capsuleCollider))
        {
            minableNodes.minableNodesList.Remove(this.capsuleCollider);
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyInfo>() != null)
        {
            if (other.gameObject.name == "BigBoy")
            {
                Death();
            }
            if (other.tag == "Corpse")
            {
                Death();
            }
        }
        //if (other.tag == "DamageBox")
        //{
        //    Debug.Log("Tree HIT Damage Box");
        //    Burnable burnable = GetComponent<Burnable>();
        //    burnable.isBurning = true;
        //    burnable.AddBurnEffect();
        //}
    }
}