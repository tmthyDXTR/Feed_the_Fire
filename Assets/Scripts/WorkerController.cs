using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class WorkerController : MonoBehaviour
{

    #region Variables

    public LayerMask groundLayer;
    public Collider target;
    private float distanceToTarget;
    public bool targetReached;

    private float workTime = 0.0f;
    public int workSpeed = 1;
    public int animationSpeed = 2;

    public int searchRadius = 30;
    public LayerMask searchLayer;

    NavMeshAgent m_NavMeshAgent;
    Animator m_Animator;

    #endregion


    void Awake()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        m_Animator.speed = animationSpeed;
    }


    #region Search Methods

    public Collider SearchTarget(string targetTag, LayerMask targetLayer)
    {
        searchLayer = targetLayer;
        Collider[] colliderHits = Physics.OverlapSphere(transform.position, searchRadius, searchLayer);
        List<Collider> targets = new List<Collider>();
        float bestDistance = 999999.0f;
        foreach (Collider collider in colliderHits)
        {
            float distance = Vector3.Distance(collider.transform.position, transform.position);
            if (collider.gameObject.CompareTag(targetTag))
            {
                targets.Add(collider);
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    target = collider;
                }
            }                
        }
        if (targets.Count > 0)
        {
            Debug.Log("Found Target Collider");
            return target;
        }
        else
        {
            Debug.Log("No Target Collider found");
            return null;
        }        
    }

    public Collider SearchStorage(string reason, LayerMask storageLayer) // reason: "Collect" or "Store"?
    {
        searchLayer = storageLayer;
        Collider[] colliderHits = Physics.OverlapSphere(transform.position, searchRadius, searchLayer);
        List<Collider> targets = new List<Collider>();
        float bestDistance = 999999.0f;
        foreach (Collider collider in colliderHits)
        {
            float distance = Vector3.Distance(collider.transform.position, transform.position);
            // Check Storage Search Reason: Store - search for not full
            if (reason == "Store")
            {
                if (collider.transform.tag == "StorageFull")
                {
                    continue;
                }
                if (collider.gameObject.CompareTag("Storage") || collider.gameObject.CompareTag("StorageEmpty"))
                {
                    targets.Add(collider);
                    if (distance < bestDistance)
                    {
                        bestDistance = distance;
                        target = collider;
                    }
                }
            }
            // Check Storage Search Reason: Collect - search for not empty
            if (reason == "Collect")
            {
                if (collider.transform.tag == "StorageEmpty")
                {
                    continue;
                }
                if (collider.gameObject.CompareTag("Storage") || collider.gameObject.CompareTag("StorageFull"))
                {
                    targets.Add(collider);
                    if (distance < bestDistance)
                    {
                        bestDistance = distance;
                        target = collider;
                    }
                }
            }            
        }
        if (targets.Count > 0)
        {
            return target;
        }
        else
        {
            return null;
        }
    }

    #endregion


    #region Movement Methods

    public void MoveToTarget()
    {
        if (target != null)
        {
            m_Animator.SetBool("IsLumbering", false);
            m_Animator.speed = 2;
            m_NavMeshAgent.isStopped = false;
            m_NavMeshAgent.SetDestination(target.transform.position);
            m_Animator.SetBool("IsWalking", true);
            targetReached = false;
        }
        else
        {
            Debug.Log("Can not Move to Target - Target null");
        }
    }

    private void CheckTargetReached()
    {
        if (target != null)
        {
            if (m_NavMeshAgent.pathPending)
            {
                distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            }
            else
            {
                distanceToTarget = m_NavMeshAgent.remainingDistance;
            }
            if (distanceToTarget <= m_NavMeshAgent.stoppingDistance)
            {
                Debug.Log("Target reached");
                m_NavMeshAgent.isStopped = true;
                m_Animator.SetBool("IsWalking", false);
                this.transform.LookAt(target.transform);
                targetReached = true;
            }
        }
        else
        {
            Debug.Log("Can not Check Target Distance - Target null");
        }
    }

    public bool TargetReached()
    {
        CheckTargetReached();
        if (targetReached == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion


    #region Misc Methods

    public bool CheckTarget()
    {
        if (target.gameObject.tag == "WorkActive" || target == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void SetTargetActive()
    {
        if (target != null)
        {
            target.gameObject.tag = "WorkActive";
        }
    }

    public void SetTargetInactive()
    {
        if (target != null)
        {
            target.gameObject.tag = "WorkInactive";
        }
    }

    public void ChopWood()
    {
        m_Animator.SetBool("IsWalking", false);
        m_Animator.SetBool("IsLumbering", true);
        m_Animator.speed = 1;
        workTime += Time.deltaTime;
        transform.LookAt(target.transform);
        if (workTime >= 12 / workSpeed)
        {
            if (target != null)
            {
                // Try and find a Nodes Resource script on the gameobject hit.
                TreeNodes tree = target.GetComponent<TreeNodes>();
                UnitInfo info = GetComponent<UnitInfo>();
                // If the Node Resource script component exists...
                if (tree != null)
                {
                    // ... the Node should lose resources.
                    tree.TakeDamage();
                    info.invWood += 1;
                    Debug.Log("Wood chopped");
                    workTime -= (int)workTime;
                }
            }
        }
    }

    public bool CheckStorage(string reason) // Check reason "Collect" or "Store"
    {
        WoodLogs storage = target.GetComponent<WoodLogs>();
        if (reason == "Store")
        {            
            if (storage != null && storage.currentAmount < storage.woodCapacity)
            {
                return true;
            }
            else if (storage != null && storage.currentAmount == storage.woodCapacity)
            {
                return false;
            }
            else
            {
                return false;
            }
        }
        if (reason == "Collect")
        {
            if (storage != null && storage.currentAmount > 0)
            {
                return true;
            }           
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void StoreWood()
    {
        workTime += Time.deltaTime;
        if (workTime >= 1)
        {
            if (target != null)
            {
                UnitInfo info = GetComponent<UnitInfo>();
                WoodLogs storage = target.GetComponent<WoodLogs>();
                if (storage != null && storage.currentAmount < storage.woodCapacity)
                {
                    storage.StoreWood();
                    info.invWood -= 1;
                    ResourceBank.AddWoodToStock(1);
                    workTime -= (int)workTime;
                }
            }
        }
    }

    public void CollectWood()
    {
        workTime += Time.deltaTime;
        if (workTime >= 1)
        {
            if (target != null)
            {
                UnitInfo info = GetComponent<UnitInfo>();
                WoodLogs storage = target.GetComponent<WoodLogs>();
                if (storage != null && storage.currentAmount > 0)
                {
                    storage.CollectWood();
                    info.invWood += 1;
                    ResourceBank.RemoveWoodFromStock(1);
                    workTime -= (int)workTime;
                }
            }
        }
    }

    public void GrowFire()
    {
        UnitInfo info = GetComponent<UnitInfo>();
        workTime += Time.deltaTime;
        if (ResourceBank.fireLife < ResourceBank.fireLifeMax)
        {
            if (workTime >= 1)
            {
                info.invWood -= 1;
                ResourceBank.AddWoodToFire(1);
                workTime -= (int)workTime;
            }
        }
    }

    #endregion 

}


