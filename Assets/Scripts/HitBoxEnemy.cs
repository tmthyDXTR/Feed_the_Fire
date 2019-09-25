using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxEnemy : MonoBehaviour
{
    private EnemyInfo info;

    void Awake()
    {
        info = transform.parent.gameObject.GetComponent<EnemyInfo>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DamageBox")
        {
            if (info != null)
            {
                float damage = other.transform.gameObject.GetComponent<DamageBox>().damage;
                info.TakeDamage(damage);
                Debug.Log("explosion");
                    }
        }
    }
}
