using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeHandler : MonoBehaviour
{
    private HeroController util;
    private SphereCollider attackRangeCollider;

    void Awake()
    {
        util = transform.parent.gameObject.GetComponent<HeroController>();
        attackRangeCollider = GetComponent<SphereCollider>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Explosive" || other.tag == "DeadTree")
        {
            util.inRangeTargets.Add(other);
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Explosive" || other.tag == "DeadTree")
        {
            util.inRangeTargets.Remove(other);
        }
    }
}
