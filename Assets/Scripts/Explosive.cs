using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject damageBoxPrefab;
    public float damage;
    public float radius;

    [SerializeField] public bool isActive = false;
    

    void Awake()
    {
        
    }

    public void Explode()
    {
        if (!isActive)
        {
            isActive = true;
            GameObject explosionEffect = Instantiate(explosionPrefab,
                this.transform.position,
                Quaternion.identity) as GameObject;
            GameObject damageBox = Instantiate(damageBoxPrefab,
                this.transform.position,
                Quaternion.identity) as GameObject;
            DamageBox box = damageBox.GetComponent<DamageBox>();
            box.damage = damage;
            damageBox.GetComponent<SphereCollider>().radius = radius;
            Destroy(this.gameObject);
        }
    }
}
