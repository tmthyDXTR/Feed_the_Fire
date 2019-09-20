using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeHandlerBigBoy : MonoBehaviour
{
    private BigBoyController util;
    private SphereCollider attackRangeCollider;

    void Awake()
    {
        util = transform.parent.gameObject.GetComponent<BigBoyController>();
        attackRangeCollider = GetComponent<SphereCollider>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hero")
        {
            util.inRangeTargets.Add(other);
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hero")
        {
            util.inRangeTargets.Remove(other);
        }
        
    }
}
