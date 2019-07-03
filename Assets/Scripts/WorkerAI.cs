﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class WorkerAI : MonoBehaviour
{

    #region Variables

    public LayerMask groundLayer;

    public Vector3 nextNode;
    public Collider nextNodeCollider;
    public Collider Target;

    private float workTime = 0.0f;
    public int workSpeed = 1;
    public int inventorySize = 1;
    public int inventoryAmount = 0;
    public int inventoryWood = 0;
    public int inventoryStone = 0;
    public int animationSpeed = 2;

    private State state;
    private enum State
    {
        JustSpawned,
        Idle,
        MovingToJobNode,
        WorkingOnJob,
        MovingToStorage,
        LoadToStorage,
        LoadToConstruction,
        TakingFromStorage,
        IdleAtFire,
        MovingToFire,
        ConstructionWork,
    }

    private float distanceToNode;
    private float distanceToNearestNode = float.MaxValue;

    //SphereCaster for closest JobNode Search
    private Transform searchSphere;
    public Vector3 sphereOrigin;
    public float sphereRadius;
    public LayerMask sphereLayerMask;

    NavMeshAgent m_NavMeshAgent;
    Animator m_Animator;

    #endregion


    #region Target Search Methods

    // Target Search Method
    private Vector3 GetClosestInactiveNodeVector(string nodeTag)
    {
        sphereOrigin = transform.position;
        Collider[] nodeColliders = Physics.OverlapSphere(sphereOrigin, sphereRadius, sphereLayerMask);
        //Debug.Log("Found Node Colliders: " + nodeColliders.Length);

        float bestDistance = 999999.0f;
        
        foreach (Collider node in nodeColliders)
        {
            float distance = Vector3.Distance(node.ClosestPoint(nextNode), transform.position);
            if (node.gameObject.CompareTag(nodeTag))
            {                
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    nextNode = node.ClosestPoint(nextNode);                    
                }
            }
        }                
        return nextNode;        
    }

    private Collider GetTarget(string nodeTag)
    {
        sphereOrigin = transform.position;
        Collider[] nodeColliders = Physics.OverlapSphere(sphereOrigin, sphereRadius, sphereLayerMask);

        float bestDistance = 999999.0f;

        foreach (Collider node in nodeColliders)
        {
            float distance = Vector3.Distance(node.ClosestPoint(nextNode), transform.position);
            if (node.gameObject.CompareTag("WorkActive"))
            {
                continue;
            }
            if (node.gameObject.CompareTag(nodeTag))
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    Target = node;
                }
        }
        Debug.Log("Found Inactive Target Collider");
        return Target;
    }

    private Collider GetNodeCollider(string nodeTag)
    {
        sphereOrigin = transform.position;
        Collider[] nodeColliders = Physics.OverlapSphere(sphereOrigin, sphereRadius, sphereLayerMask);
        Debug.Log("Colliders: " + nodeColliders.Length);

        float bestDistance = 999999.0f;

        foreach (Collider node in nodeColliders)
        {
            float distance = Vector3.Distance(node.ClosestPoint(nextNode), transform.position);

            if (node.gameObject.CompareTag(nodeTag))
            {
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    nextNodeCollider = node;
                }
            }

                
        }
        return nextNodeCollider;
    }

    private int GetInactiveNodesCount()
    {
        sphereOrigin = transform.position;
        Collider[] nodeColliders = Physics.OverlapSphere(sphereOrigin, sphereRadius, sphereLayerMask);
        int inactiveNodeCounter = 0;

        foreach (Collider node in nodeColliders)
        {
            
            if (node.gameObject.tag =="WorkInactive" || node.gameObject.tag == "Construction")
            {
                inactiveNodeCounter += 1;                                
            }
        }
        Debug.Log("Found Inactive Node Colliders: " + inactiveNodeCounter);
        return inactiveNodeCounter;
    }

    #endregion


    #region GoTo Methods

    void GoToNearestTreeNode()
    {
        m_NavMeshAgent.isStopped = false;
        sphereLayerMask = LayerMask.GetMask("TreeNodes");
        m_NavMeshAgent.SetDestination(GetClosestInactiveNodeVector("WorkInactive"));
        Target = GetTarget("WorkInactive");
        Target.gameObject.tag = "WorkActive";
        m_Animator.SetBool("IsWalking", true);
        m_Animator.SetBool("IsLumbering", false);
        m_NavMeshAgent.stoppingDistance = 3;
    }

    void GoToNearestStoneNode()
    {
        m_NavMeshAgent.isStopped = false;
        sphereLayerMask = LayerMask.GetMask("StoneNodes");
        m_NavMeshAgent.SetDestination(GetClosestInactiveNodeVector("WorkInactive"));
        Target = GetTarget("WorkInactive");
        Target.gameObject.tag = "WorkActive";
        m_Animator.SetBool("IsWalking", true); 
        m_Animator.SetBool("IsLumbering", false);
        m_NavMeshAgent.stoppingDistance = 3;
    }

    void GoToNearestStorage()
    {
        m_NavMeshAgent.isStopped = false;
        sphereLayerMask = LayerMask.GetMask("Buildings");
        m_NavMeshAgent.SetDestination(GetClosestInactiveNodeVector("Storage"));
        Target = GetTarget("Storage");
        m_Animator.SetBool("IsLumbering", false);
        m_Animator.SetBool("IsWalking", true);
        m_NavMeshAgent.stoppingDistance = 3;
    }

    void GoToNearestFire()
    {
        m_NavMeshAgent.isStopped = false;
        sphereLayerMask = LayerMask.GetMask("SafePlaceNodes");
        m_NavMeshAgent.SetDestination(GetClosestInactiveNodeVector("WorkInactive"));
        Target = GetTarget("WorkInactive");
        m_Animator.SetBool("IsWalking", true);
        m_Animator.SetBool("IsLumbering", false);
    }

    #endregion


    #region Job Methods

    void MineWood()
    {
        m_Animator.SetBool("IsLumbering", true);
        m_Animator.speed = 1;
        workTime += Time.deltaTime;
        transform.LookAt(m_NavMeshAgent.destination);
        if (workTime >= 12 / workSpeed)
        {
            // Try and find a Nodes Resource script on the gameobject hit.
            TreeNode treeNode = Target.GetComponent<TreeNode>();
            // If the Node Resource script component exists...
            if (treeNode != null)
            {
                // ... the Node should lose resources.
                treeNode.TakeDamage();
                inventoryAmount += 1;
                inventoryWood += 1;
                Debug.Log("Wood mined");
                workTime -= (int)workTime;
            }
            
        }
    }

    void MineStone()
    {
        m_Animator.SetBool("IsLumbering", true);
        m_Animator.speed = 1;
        workTime += Time.deltaTime;
        transform.LookAt(m_NavMeshAgent.destination);
        if (workTime >= 12 / workSpeed)
        {
            // Try and find an Nodes Resource script on the gameobject hit.
            StoneNode stoneAmount = Target.GetComponent<StoneNode>();
            // If the Node Resource component exists...
            if (stoneAmount != null)
            {
                // ... the Node should lose resources.
                stoneAmount.TakeDamage();
                inventoryAmount += 1;
                inventoryStone += 1;
                Debug.Log("Stone mined");
            }
            workTime -= (int)workTime;
        }        
    }

    void ConstructBuilding()
    {
        m_Animator.SetBool("IsLumbering", true);
        m_Animator.speed = 1;
        workTime += Time.deltaTime;
        transform.LookAt(m_NavMeshAgent.destination);
        if (workTime >= 12 / workSpeed)
        {
            // Try and find an Buildings Info script on the gameobject to construct.
            BuildingInfo currentHealth = Target.GetComponent<BuildingInfo>();
            // If the Building Info component exists...
            if (currentHealth != null)
            {
                // ... the Building should "gain health".
                currentHealth.GainHealth();
                Debug.Log("Health gained");                
            }
            workTime -= (int)workTime;
        }
        
    }

    #endregion


    #region Resource Methods

    void TakeResourceForConstruction(string resource)
    {
        BuildingInfo reqResources = nextNodeCollider.GetComponent<BuildingInfo>();

        if (resource == "Wood")
        {
            workTime += Time.deltaTime;
            transform.LookAt(m_NavMeshAgent.destination);
            if (workTime >= 1)
            {
                if (ResourceBank.GetWoodStock() > 0)
                {
                    inventoryAmount += (int)workTime;
                    inventoryWood += (int)workTime;
                    ResourceBank.RemoveWoodFromStock((int)workTime);
                    reqResources.ReqResourceMinus("Wood");
                    workTime -= (int)workTime;
                }
                
            }
        }
        if (resource == "Stone")
        {
            workTime += Time.deltaTime;
            transform.LookAt(m_NavMeshAgent.destination);
            if (workTime >= 1)
            {
                if (ResourceBank.GetStoneStock() > 0)
                {
                    inventoryAmount += (int)workTime;
                    inventoryStone += (int)workTime;
                    ResourceBank.RemoveStoneFromStock((int)workTime);
                    reqResources.ReqResourceMinus("Stone");
                    workTime -= (int)workTime;
                }
                
            }
        }
    }

    void TakeResource(string resource)
    {
        if (resource == "Wood")
        {
            workTime += Time.deltaTime;
            if (workTime >= 1)
            {
                inventoryAmount += (int)workTime;
                inventoryWood += (int)workTime;
                ResourceBank.RemoveWoodFromStock((int)workTime);
                workTime -= (int)workTime;
            }
        }
        if (resource == "Stone")
        {
            workTime += Time.deltaTime;
            if (workTime >= 1)
            {
                inventoryAmount += (int)workTime;
                inventoryStone += (int)workTime;
                ResourceBank.RemoveStoneFromStock((int)workTime);
                workTime -= (int)workTime;
            }
        }
    }

    void PutWoodInFire()
    {
        workTime += Time.deltaTime;
        if (workTime >= 1)
        {
            inventoryAmount -= (int)workTime;
            inventoryWood -= (int)workTime;
            ResourceBank.AddWoodToFire((int)workTime);
            workTime -= (int)workTime;
        }
            
    }

    void StoreResource(string resource)
    {
        if (resource == "Wood")
        {
            workTime += Time.deltaTime;
            if (workTime >= 1)
            {
                inventoryAmount -= (int)workTime;
                inventoryWood -= (int)workTime;
                ResourceBank.AddWoodToStock((int)workTime);
                workTime -= (int)workTime;
            }
                
        }
        if (resource == "Stone")
        {
            workTime += Time.deltaTime;
            if (workTime >= 1)
            {
                inventoryAmount -= (int)workTime;
                inventoryStone -= (int)workTime;
                ResourceBank.AddStoneToStock((int)workTime);
                workTime -= (int)workTime;
            }
                
        }
    }

    #endregion



    void Awake()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        m_Animator.speed = animationSpeed;

        //Visualize Search Sphere
        searchSphere = transform.Find("SearchSphere");
        searchSphere.localScale += new Vector3(sphereRadius, sphereRadius, sphereRadius);

        //Set Spawn Behaviour                
        GoToNearestFire();
        state = State.MovingToFire;

        //Set Inventory Amount
        inventoryAmount = 0;

        gameObject.tag = "Unemployed";
    }


    void Update()
    {        
        switch (state)
        {
            #region State IdleAtFire

            case State.IdleAtFire:
                Debug.Log("State: IdleAtFire");                
                if (this.gameObject.tag == "Unemployed" || this.gameObject.tag == "LightWarden")
                {
                    Debug.Log("State: IdleAtFire");
                    inventorySize = 1;
                    m_NavMeshAgent.isStopped = true;
                    m_Animator.SetBool("IsWalking", false);
                    m_Animator.SetBool("IsLumbering", false);
                    transform.LookAt(m_NavMeshAgent.destination);


                    if (this.gameObject.tag == "LightWarden" && ResourceBank.GetFireLife() < ResourceBank.GetFireLifeMax())
                    {
                        inventorySize = 1;
                        sphereLayerMask = LayerMask.GetMask("Buildings");
                        if (GetNodeCollider("Storage") != null)
                        {
                            GoToNearestStorage();
                            state = State.MovingToStorage;
                        }                        
                    }
                }

                if (this.gameObject.tag == "Builder")
                {
                    inventorySize = 5;
                    sphereLayerMask = LayerMask.GetMask("Buildings");
                    GetNodeCollider("Construction");
                    if (GetInactiveNodesCount() != 0)
                    {
                        GoToNearestStorage();
                        state = State.MovingToStorage;
                    }
                }

                if (this.gameObject.tag == "Woodcutter")
                {
                    inventorySize = 1;
                    sphereLayerMask = LayerMask.GetMask("TreeNodes");
                    if (GetInactiveNodesCount() != 0)
                    {
                        GoToNearestTreeNode();
                        state = State.MovingToJobNode;
                    }                                     
                }

                if (this.gameObject.tag == "Stonecutter")
                {
                    inventorySize = 1;
                    sphereLayerMask = LayerMask.GetMask("StoneNodes");
                    if (GetInactiveNodesCount() != 0)
                    {
                        GoToNearestStoneNode();
                        state = State.MovingToJobNode;
                    }                         
                }
                break;

            #endregion

            #region State MovingToJobNode

            case State.MovingToJobNode:
                Debug.Log("State: MovingToJobNode");
                if (m_NavMeshAgent.pathPending)
                {
                    distanceToNode = Vector3.Distance(transform.position, Target.transform.position);
                }
                else
                {
                    distanceToNode = m_NavMeshAgent.remainingDistance;
                }
                Debug.Log("MovingToJobNode - Distance to Job Node: " + distanceToNode);
                if (distanceToNode <= m_NavMeshAgent.stoppingDistance)
                {
                    Debug.Log("Job Node reached");
                    m_Animator.SetBool("IsWalking", false);
                    if (this.gameObject.tag == "Builder" && Target.tag == "Storage")
                    {
                        state = State.LoadToConstruction;
                    }
                    if (this.gameObject.tag == "Builder" && Target.tag != "Storage")
                    {
                        state = State.ConstructionWork;
                    }
                    else
                    {
                        state = State.WorkingOnJob;
                    }
                }                
                break;

            #endregion

            #region State ConstructionWork

            case State.ConstructionWork:
                Debug.Log("State: ConstructionWork");
                if (this.gameObject.tag == "Builder")
                {
                    m_NavMeshAgent.isStopped = true;
                    sphereLayerMask = LayerMask.GetMask("Buildings");
                    Debug.Log("Constructing Building");
                    ConstructBuilding();

                    BuildingInfo building = Target.GetComponent<BuildingInfo>();
                    Debug.Log("Building Info Script found");
                    if (building.currentHealth == building.maxHealth)
                    {
                        building.ConstructionComplete();

                        if (GetInactiveNodesCount() != 0)
                        {
                            Target = GetTarget("Storage");
                            GoToNearestStorage();
                            state = State.MovingToStorage;
                        }
                        else
                        {
                            GoToNearestFire();
                            state = State.MovingToFire;
                        }
                    }
                }

                break;

            #endregion

            #region State WorkingOnJob

            case State.WorkingOnJob:
                Debug.Log("State: WorkingOnJob");
                if (inventoryAmount < inventorySize)
                {
                    if (this.gameObject.tag == "Woodcutter")
                    {
                        m_NavMeshAgent.isStopped = true;
                        Debug.Log("Mining Wood");
                        MineWood();
                    }

                    if (this.gameObject.tag == "Stonecutter")
                    {
                        m_NavMeshAgent.isStopped = true;
                        Debug.Log("Mining Stone");
                        MineStone();
                    }                 
                    
                }
                
                else
                {
                    Debug.Log("Job Done");
                    m_Animator.speed = animationSpeed;

                    if (this.gameObject.tag == "Woodcutter" || this.gameObject.tag == "Stonecutter")
                    {
                        if (Target != null)
                        {
                            Target.gameObject.tag = "WorkInactive";
                        }
                        GoToNearestStorage();
                        state = State.MovingToStorage;
                    }
                }
                /*                                                                                                      
                    
                    m_Animator.speed = 1;
                    workTime += Time.deltaTime;
                    transform.LookAt(m_NavMeshAgent.destination);
                    if (workTime >= 12 / workSpeed)
                    {
                        //Temporary fix- should mine last jobs loot -->
                        if (this.gameObject.tag == "Unemployed")
                        {
                            // Try and find an EnemyHealth script on the gameobject hit.
                            TreeNode woodAmount = GetNodeCollider().GetComponent<TreeNode>();
                            // If the EnemyHealth component exist...
                            if (woodAmount != null)
                            {
                                // ... the enemy should take damage.
                                woodAmount.TakeDamage();
                                inventoryAmount += 1;
                            }
                            workTime -= (int)workTime;
                        }  */                                                      
                        
                break;

            #endregion

            #region State MovingToStorage

            case State.MovingToStorage:
                Debug.Log("State: MovingToStorage");
                m_Animator.SetBool("IsWalking", true);
                if (m_NavMeshAgent.pathPending)
                {
                    distanceToNode = Vector3.Distance(transform.position, Target.transform.position);
                }
                else
                {
                    distanceToNode = m_NavMeshAgent.remainingDistance;
                }
                Debug.Log("MovingToStorage - Distance to Storage Node: " + distanceToNode);
                if (distanceToNode <= m_NavMeshAgent.stoppingDistance)
                {
                    m_Animator.SetBool("IsWalking", false);
                    m_Animator.SetBool("IsLumbering", false);

                    if (this.gameObject.tag == "Unemployed")
                    {
                        if (inventoryAmount > 0)
                        {
                            state = State.LoadToStorage;
                        }
                        else
                        {
                            GoToNearestFire();
                            state = State.MovingToFire;
                        }                                                                
                    }

                    if (this.gameObject.tag == "LightWarden" || this.gameObject.tag == "Builder")
                    {                        
                        state = State.TakingFromStorage;
                    }

                    if (this.gameObject.tag == "Stonecutter" || this.gameObject.tag == "Woodcutter")
                    {
                        state = State.LoadToStorage;
                    }                     
                }
                break;

            #endregion

            #region State TakingFromStorage

            case State.TakingFromStorage:
                Debug.Log("State: TakingFromStorage");
                if (this.gameObject.tag == "LightWarden" ||
                    this.gameObject.tag == "Unemployed")
                {
                    if (inventoryAmount < inventorySize)
                    {
                        TakeResource("Wood");
                    }
                    else
                    {
                        Debug.Log("Taking complete");
                        if (this.gameObject.tag == "Unemployed" || this.gameObject.tag == "LightWarden")
                        {
                            GoToNearestFire();
                            state = State.MovingToFire;
                        }
                    }
                }

                if (this.gameObject.tag == "Builder")
                {
                    BuildingInfo reqResources = nextNodeCollider.GetComponent<BuildingInfo>();
                    Debug.Log("Required Wood for Construction: " + reqResources.GetReqResource("Wood"));
                    Debug.Log("Required Stone for Construction: " + reqResources.GetReqResource("Stone"));
                    
                    if (inventoryAmount < inventorySize)
                    {
                        if (reqResources.moreResourcesNeeded() == true)
                        {
                            if (reqResources.GetReqResource("Wood") > 0)
                            {
                                TakeResourceForConstruction("Wood");
                            }
                            if (reqResources.GetReqResource("Stone") > 0)
                            {
                                TakeResourceForConstruction("Stone");
                            }
                        }                        
                    }                
                    
                    if (inventoryAmount == inventorySize || reqResources.moreResourcesNeeded() == false)
                    {
                        Debug.Log("Taking Resources complete");
                        Target = nextNodeCollider;
                        m_NavMeshAgent.SetDestination(Target.gameObject.transform.position);                       
                        m_Animator.SetBool("IsWalking", true);
                        m_NavMeshAgent.stoppingDistance = 3;

                        state = State.MovingToJobNode;
                    }
                }                                             
                break;

            #endregion

            #region State LoadToStorage

            case State.LoadToStorage:
                Debug.Log("State: LoadingToStorage");
                if (inventoryAmount > 0)
                {
                    workTime += Time.deltaTime;
                    if (workTime >= 1)
                    {
                        //Temporary fix- should mine last jobs loot -->
                        if (this.gameObject.tag == "Unemployed")
                        {
                            inventoryAmount -= (int)workTime;
                            inventoryWood -= (int)workTime;
                            if (Target.gameObject.layer == 14) //-- If Target is Storage
                            {
                                ResourceBank.AddWoodToStock((int)workTime);
                            }
                            if (Target.gameObject.layer == 13) //-- If Target is FirePlace
                            {
                                ResourceBank.AddWoodToFire((int)workTime);
                            }
                            workTime -= (int)workTime;
                        }
                        if (this.gameObject.tag == "LightWarden")
                        {
                            PutWoodInFire();
                        }
                        if (this.gameObject.tag == "Woodcutter")
                        {
                            StoreResource("Wood");
                        }
                        if (this.gameObject.tag == "Stonecutter")
                        {
                            StoreResource("Stone");
                        }
                    }
                    
                }
                else
                {
                    Debug.Log("Loading complete");
                    if (this.gameObject.tag == "Unemployed")
                    {
                        GoToNearestFire();
                        m_NavMeshAgent.stoppingDistance = 5;
                        state = State.MovingToFire;
                    }
                    if (this.gameObject.tag == "LightWarden")
                    {                        
                        state = State.IdleAtFire;
                    }
                    if (this.gameObject.tag == "Woodcutter")
                    {
                        sphereLayerMask = LayerMask.GetMask("TreeNodes");
                        if (GetInactiveNodesCount() != 0)
                        {
                            GoToNearestTreeNode();
                            state = State.MovingToJobNode;
                        }
                        else
                        {
                            GoToNearestFire();
                            m_NavMeshAgent.stoppingDistance = 5;
                            state = State.MovingToFire;
                        }
                        
                       
                    }

                    if (this.gameObject.tag == "Stonecutter")
                    {
                        sphereLayerMask = LayerMask.GetMask("StoneNodes");
                        if (GetInactiveNodesCount() != 0)
                        {
                            GoToNearestStoneNode();
                            state = State.MovingToJobNode;
                        }
                        else
                        {
                            GoToNearestFire();
                            m_NavMeshAgent.stoppingDistance = 5;
                            state = State.MovingToFire;
                        }
                    }
                }
                break;

            #endregion

            #region State LoadToConstruction

            case State.LoadToConstruction:
                Debug.Log("State: LoadToConstruction");
                if (inventoryAmount > 0)
                {
                    BuildingInfo currentResources = nextNodeCollider.GetComponent<BuildingInfo>();

                    workTime += Time.deltaTime;
                    if (workTime >= 1)
                    {
                        if (inventoryWood > 0)
                        {
                            inventoryAmount -= (int)workTime;
                            inventoryWood -= (int)workTime;
                            currentResources.CurrentResourcePlus("Wood");
                            workTime -= (int)workTime;
                        }
                        
                                 
                    }
                    workTime += Time.deltaTime;
                    if (workTime >= 1)
                    {
                        if (inventoryStone > 0)
                        {
                            inventoryAmount -= (int)workTime;
                            inventoryStone -= (int)workTime;
                            currentResources.CurrentResourcePlus("Stone");
                            workTime -= (int)workTime;
                        }
                    }
                        
                }
                else
                {
                    BuildingInfo moreResources = nextNodeCollider.GetComponent<BuildingInfo>();
                    if (moreResources.moreResourcesNeeded() == true)
                    {
                        GoToNearestStorage();
                        state = State.MovingToStorage;
                    }
                    else
                    {
                        m_Animator.SetBool("IsLumbering", true);
                        state = State.WorkingOnJob;
                    }
                }
                break;

            #endregion

            #region State MovingToFire

            case State.MovingToFire:
                Debug.Log("State: MovingToFire");
                if (this.gameObject.tag == "LightWarden")
                {
                    m_NavMeshAgent.stoppingDistance = 3;
                }

                if (m_NavMeshAgent.pathPending)
                {
                    distanceToNode = Vector3.Distance(transform.position, Target.transform.position);
                }
                else
                {
                    distanceToNode = m_NavMeshAgent.remainingDistance;
                }
                Debug.Log("MovingToFire - Distance to FirePlace: " + distanceToNode);
                
                if (distanceToNode <= m_NavMeshAgent.stoppingDistance)
                {
                    Debug.Log("Fire reached");
                    m_Animator.SetBool("IsWalking", false);

                    if (this.gameObject.tag == "Unemployed" && inventoryWood > 0 && 
                        ResourceBank.GetFireLife() < ResourceBank.GetFireLifeMax() || 
                        this.gameObject.tag == "LightWarden" && inventoryWood > 0 && 
                        ResourceBank.GetFireLife() < ResourceBank.GetFireLifeMax())
                        {
                        state = State.LoadToStorage;
                        }

                    else
                    {
                        state = State.IdleAtFire;
                    }                   
                }
                break;

                #endregion
        }
    }
}


                