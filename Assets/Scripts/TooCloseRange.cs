using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooCloseRange : MonoBehaviour
{
    private BigBoyController util;
    private SphereCollider tooCloseRangeCollider;

    [SerializeField] private float reactionTime = 1.5f;
    [SerializeField] private float timer = 0.00f;
    [SerializeField] private bool isTooClose = false;

    void Awake()
    {
        util = transform.parent.gameObject.GetComponent<BigBoyController>();
        tooCloseRangeCollider = GetComponent<SphereCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hero")
        {
            util.tooCloseTargets.Add(other);
            if (!isTooClose)
            {
                isTooClose = true;

            }
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Hero")
        {
            if (isTooClose)
            {
                timer += Time.deltaTime;
                if (timer >= reactionTime)
                {
                    util.isTooClose = true;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hero")
        {
            util.tooCloseTargets.Remove(other);
            if (isTooClose)
            {
                util.isTooClose = false;
                isTooClose = false;
                timer -= timer;
            }
        }
    }
}
