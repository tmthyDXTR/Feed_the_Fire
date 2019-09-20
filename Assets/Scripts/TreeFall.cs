using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFall : MonoBehaviour
{
    private Rigidbody rigidbody;
    private CapsuleCollider collider;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();

    }

    void Update()
    {
        StartCoroutine(DeactivateCollider());
        StartCoroutine(DeactivateRigidbody());
        StartCoroutine(DestroyThis());
    }

    IEnumerator DeactivateCollider()
    {
        yield return new WaitForSeconds(8f);
        if (collider.enabled)
        {
            collider.enabled = false;
        }
    }
    IEnumerator DeactivateRigidbody()
    {
        yield return new WaitForSeconds(12f);
        if (rigidbody.detectCollisions)
        {
            rigidbody.detectCollisions = false;
        }
    }
    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(14f);
        Destroy(gameObject);
    }
}
