using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeHandlerEnemy : MonoBehaviour
{
    private EnemyRoam util;
    private SphereCollider attackRangeCollider;

    void Awake()
    {
        util = transform.parent.gameObject.GetComponent<EnemyRoam>();
        attackRangeCollider = GetComponent<SphereCollider>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 17)
        {
            util.inRangeTargets.Add(other);
            
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 17)
        {
            util.inRangeTargets.Remove(other);
        }

    }
}
