using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoyDeadBox : MonoBehaviour
{

    private BoxCollider boxCol;
    private CapsuleCollider CapCol;
    private SphereCollider SphCol;
    private SphereCollider SphCol2;
    private GameObject deadBox;

    void Awake()
    {
        boxCol = GetComponent<BoxCollider>();
        CapCol = GetComponent<CapsuleCollider>();
        SphCol = transform.Find("AttackRange").GetComponent<SphereCollider>();
        SphCol2 = transform.Find("DetectionSphere").GetComponent<SphereCollider>();
        deadBox = transform.Find("DeadBox").gameObject;

        boxCol.enabled = false;
        CapCol.enabled = false;
        SphCol.enabled = false;
        SphCol2.enabled = false;

        deadBox.SetActive(true);
    }

    void Update()
    {
        
    }
}
