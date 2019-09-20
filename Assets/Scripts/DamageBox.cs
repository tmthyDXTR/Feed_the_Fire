using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox : MonoBehaviour
{
    public float damage;
    public float radius;
    public SphereCollider damageCollider;

    public List<Collider> inRangeTargets = new List<Collider>();

    void Awake()
    {
        damageCollider = GetComponent<SphereCollider>();
        damageCollider.isTrigger = true;
        damageCollider.radius = radius;
        StartCoroutine(WaitAndDestroy(0.02f));
    }

    //void OnEnable()
    //{        
    //    damageCollider = GetComponent<SphereCollider>();
    //    damageCollider.isTrigger = true;
    //    damageCollider.radius = radius;
    //    StartCoroutine(WaitAndDestroy(0.02f));
        
    //}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hero" || other.tag == "Enemy")
        {
            inRangeTargets.Add(other);
        }
    }

    IEnumerator WaitAndDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //Destroy(damageCollider);
        //if (this.transform.childCount > 0)
        //{
        //    foreach (Transform child in this.transform)
        //    {
        //        GameObject.Destroy(child.gameObject);
        //    }
        //}
        Destroy(this.gameObject);
        //damage = 0;
    }

}
