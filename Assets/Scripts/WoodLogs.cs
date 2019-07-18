using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodLogs : MonoBehaviour
{
    public int woodCapacity = 10;
    public int currentAmount;
    bool isDead;
    CapsuleCollider capsuleCollider;

    void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        UpdateModel();
        UpdateTag();
    }

    public void UpdateModel()
    {        
        foreach (Transform child in transform)
            if (child.name == currentAmount.ToString())
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        
    }

    private void UpdateTag()
    {
        if (currentAmount == woodCapacity)
        {
            transform.tag = "StorageFull";
        }
        if (currentAmount == 0)
        {
            transform.tag = "StorageEmpty";
        }
        if (currentAmount > 0 && currentAmount < woodCapacity)
        {
            transform.tag = "Storage";
        }
    }

    private void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }

    public void TakeDamage()
    {
        if (isDead)
            return;

        currentAmount -= 1;

        //if (currentAmount <= 0)
        //{
        //    Death();
        //}
    }

    public void StoreWood()
    {
        currentAmount += 1;
    }

    void Death()
    {
        isDead = true;
        Destroy(gameObject);
    }
}
