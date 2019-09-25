using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox : MonoBehaviour
{
    public float damage;
    public float radius;
    public bool heroImmune = false;

    public enum Type
    {
        PhysDamage,
        FireAOE,
    }
    public Type type;
    public SphereCollider damageCollider;

    public List<Collider> inRangeTargets = new List<Collider>();

    void Start()
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
        // Tree burning stuff
        if (other.tag == "Tree")
        {
            if (type == Type.FireAOE)
            {
                Debug.Log("Tree HIT Fire AOE DamageBox");
                Burnable burnable = other.transform.parent.gameObject.GetComponent<Burnable>();
                burnable.isBurning = true;
                burnable.AddBurnEffect();
            }
            else
            {
                Debug.Log("Tree HIT Fire AOE DamageBox");
                TreeNodes tree = other.transform.parent.gameObject.GetComponent<TreeNodes>();
                tree.Death();                
            }
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
