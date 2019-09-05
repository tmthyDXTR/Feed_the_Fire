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
    public Collider constructionTarget;
    private float distanceToTarget;
    public bool targetReached;

    private float workTime = 0.0f;
    public int workSpeed = 1;
    public int animationSpeed = 2;

    public int searchRadius = 30;
    public LayerMask searchLayer;

    NavMeshAgent m_NavMeshAgent;
    Animator m_Animator;
    AudioSource m_AudioSource;
    ConstructionManager construction;
    UnitInfo unitInfo;

    #endregion


    void Awake()
    {
        unitInfo = GetComponent<UnitInfo>();
        construction = GameObject.Find("ConstructionManager").GetComponent<ConstructionManager>();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        m_Animator.speed = animationSpeed;
        m_AudioSource = GetComponent<AudioSource>();
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
            //Debug.Log("Found Target Collider");
            return target;
        }
        else
        {
            //Debug.Log("No Target Collider found");
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

    public Collider SearchConstruction()
    {
        // Check if there is a construction
        if (construction.constructionList.Count > 0)
        {
            target = construction.constructionList[0].GetComponent<Collider>();
            constructionTarget = target;
            return constructionTarget;
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
            if (target != null)
            {
                unitInfo.target = target;
            }
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
                //Debug.Log("Target reached");
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


    #region Ressource Methods

    public void StoreWood()
    {
        workTime += Time.deltaTime;
        if (workTime >= 1)
        {
            if (target != null)
            {
                UnitInfo unit = GetComponent<UnitInfo>();
                WoodLogs storage = target.GetComponent<WoodLogs>();
                if (storage != null && storage.currentAmount < storage.woodCapacity)
                {
                    storage.StoreWood();
                    unit.invWood -= 1;
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
                UnitInfo unit = GetComponent<UnitInfo>();
                WoodLogs storage = target.GetComponent<WoodLogs>();
                if (storage != null && storage.currentAmount > 0)
                {
                    storage.CollectWood();
                    unit.invWood += 1;
                    ResourceBank.RemoveWoodFromStock(1);
                    workTime -= (int)workTime;
                }
            }
        }
    }

    public void StoreWoodForConstruction()
    {
        workTime += Time.deltaTime;
        if (workTime >= 1)
        {
            if (constructionTarget != null)
            {
                UnitInfo unit = GetComponent<UnitInfo>();
                BuildingInfo building = constructionTarget.GetComponent<BuildingInfo>();
                if (building != null && building.wood < building.costWood)
                {
                    unit.invWood -= 1;
                    building.wood += 1;
                    workTime -= (int)workTime;
                }
            }
        }
    }

    public void CollectWoodForConstruction()
    {
        {
            workTime += Time.deltaTime;
            if (workTime >= 1)
            {
                if (constructionTarget != null)
                {
                    UnitInfo unit = GetComponent<UnitInfo>();
                    BuildingInfo building = constructionTarget.GetComponent<BuildingInfo>();
                    WoodLogs storage = target.GetComponent<WoodLogs>();
                    if (storage != null && storage.currentAmount > 0)
                    {
                        storage.CollectWood();
                        unit.invWood += 1;
                        building.reqWood -= 1;
                        ResourceBank.RemoveWoodFromStock(1);
                        workTime -= (int)workTime;
                    }
                }
            }
        }
    }

    #endregion


    #region Job Methods

    public void ChopWood()
    {
        m_Animator.SetBool("IsWalking", false);
        m_Animator.SetBool("IsLumbering", true);
        m_Animator.speed = 1;
        workTime += Time.deltaTime;        
        if (workTime >= 12 / workSpeed)
        {
            if (target != null)
            {
                transform.LookAt(target.transform);
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

    public void ConstructBuilding()
    {
        BuildingInfo building = constructionTarget.GetComponent<BuildingInfo>();
        m_NavMeshAgent.isStopped = true;
        m_Animator.SetBool("IsWalking", false);
        m_Animator.SetBool("IsLumbering", true);
        m_Animator.speed = 1;
        workTime += Time.deltaTime;
        transform.LookAt(constructionTarget.transform);
        if (workTime >= 12 / workSpeed)
        {
            if (constructionTarget != null)
            {
                if (building != null)
                {
                    building.GainHealth();
                    workTime -= (int)workTime;
                }
            }
        }
        //-- Set Building Model during Construction --//
        if ((int)building.currentHealth <= building.maxHealth * 0.25)
        {
            building.SetBuildingModel(1);
        }
        if ((int)building.currentHealth <= building.maxHealth * 0.50 && (int)building.currentHealth > building.maxHealth * 0.25)
        {
            building.SetBuildingModel(2);
        }
        if ((int)building.currentHealth <= building.maxHealth * 0.75 && (int)building.currentHealth > building.maxHealth * 0.50)
        {
            building.SetBuildingModel(3);
        }
        if ((int)building.currentHealth <= building.maxHealth * 1 && (int)building.currentHealth > building.maxHealth * 0.75)
        {
            building.SetBuildingModel(4);
        }

        if (building.currentHealth == building.maxHealth)
        {
            building.ConstructionComplete();
            building.SetBuildingModel(5);
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


    #region Misc Methods

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

    public bool CheckConstruction(string reason) // Check reason "Collect" or "Store"
    {
        BuildingInfo info = constructionTarget.gameObject.GetComponent<BuildingInfo>();
        if (reason == "Collect")
        {
            if (info.reqWood > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (reason == "Store")
        {
            if (info.wood < info.costWood)
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

    private void AttackEnd()
    {
        m_AudioSource.Play();
    }

    #endregion 

}


