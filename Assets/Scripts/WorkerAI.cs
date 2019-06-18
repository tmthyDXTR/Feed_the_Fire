using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class WorkerAI : MonoBehaviour
{
    public LayerMask groundLayer;


    public Vector3 nextNode;
    public Collider nextNodeCollider;
    public Collider Target;

    public int workSpeed = 1;
    public int inventorySize = 1;
    public int inventoryAmount;

    private State state;
    private enum State
    {
        JustSpawned,
        Idle,
        MovingToJobNode,
        WorkingOnJob,
        MovingToStorage,
        LoadToStorage,
        IdleAtFire,
        MovingToFire,
    }

    private float distanceToNode;
    private float distanceToNearestNode = float.MaxValue;





    private float workTime = 0.0f;

    //SphereCaster for closest JobNode Search
    private Transform searchSphere;
    public Vector3 sphereOrigin;
    public float sphereRadius;
    public LayerMask sphereLayerMask;
    NavMeshAgent m_NavMeshAgent;
    Animator m_Animator;



    // Target Search Method
    private Vector3 GetClosestInactiveNodeVector()
    {

        sphereOrigin = transform.position;
        Collider[] nodeColliders = Physics.OverlapSphere(sphereOrigin, sphereRadius, sphereLayerMask);
        //Debug.Log("Found Node Colliders: " + nodeColliders.Length);

        float bestDistance = 999999.0f;
        
        foreach (Collider node in nodeColliders)
        {
            float distance = Vector3.Distance(node.ClosestPoint(nextNode), transform.position);
            if (node.gameObject.CompareTag("WorkInactive"))
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

    private Collider GetTarget()
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
            if (node.gameObject.CompareTag("WorkInactive"))
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    Target = node;
                }
        }
        Debug.Log("Found Inactive Target Collider");
        return Target;
    }


    private Collider GetNodeCollider()
    {
        sphereOrigin = transform.position;
        Collider[] nodeColliders = Physics.OverlapSphere(sphereOrigin, sphereRadius, sphereLayerMask);
        Debug.Log("Colliders: " + nodeColliders.Length);

        float bestDistance = 999999.0f;

        foreach (Collider node in nodeColliders)
        {
            float distance = Vector3.Distance(node.ClosestPoint(nextNode), transform.position);

            if (distance < bestDistance)
            {
                bestDistance = distance;
                nextNodeCollider = node;
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
            
            if (node.gameObject.tag =="WorkInactive")
            {
                inactiveNodeCounter += 1;                                
            }
        }
        Debug.Log("Found Inactive Node Colliders: " + inactiveNodeCounter);
        return inactiveNodeCounter;
    }

    void Start()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        //Visualize Search Sphere
        searchSphere = transform.Find("SearchSphere");
        searchSphere.localScale += new Vector3(sphereRadius, sphereRadius, sphereRadius);

        //Set Spawn Behaviour                
        sphereLayerMask = LayerMask.GetMask("SafePlaceNodes");
        m_NavMeshAgent.SetDestination(GetClosestInactiveNodeVector());
        Target = GetTarget();
        m_Animator.SetBool("IsWalking", true);

        state = State.MovingToFire;
        //Set Inventory Amount
        inventoryAmount = 0;

        gameObject.tag = "Unemployed";
    }


    void Update()
    {

        switch (state)
        {            
            case State.IdleAtFire:
                Debug.Log("State: IdleAtFire");
                if (this.gameObject.tag == "Unemployed")
                {
                    Debug.Log("State: IdleAtFire");
                    m_NavMeshAgent.isStopped = true;
                    m_Animator.SetBool("IsWalking", false);
                    m_Animator.SetBool("IsLumbering", false);
                    transform.LookAt(m_NavMeshAgent.destination);
                    
                }
                if (this.gameObject.tag == "Woodcutter")
                {
                    sphereLayerMask = LayerMask.GetMask("TreeNodes");
                    if (GetInactiveNodesCount() == 0)
                    {
                        m_NavMeshAgent.isStopped = true;
                        m_Animator.SetBool("IsWalking", false);
                        m_Animator.SetBool("IsLumbering", false);
                        transform.LookAt(m_NavMeshAgent.destination);
                    }
                    else
                    {
                        m_NavMeshAgent.isStopped = false;
                        sphereLayerMask = LayerMask.GetMask("TreeNodes");
                        m_NavMeshAgent.SetDestination(GetClosestInactiveNodeVector());
                        Target = GetTarget();
                        Target.gameObject.tag = "WorkActive";
                        m_Animator.SetBool("IsWalking", true);
                        m_NavMeshAgent.stoppingDistance = 3;

                        state = State.MovingToJobNode;
                    }                   
                }
                if (this.gameObject.tag == "Stonecutter")
                {
                    m_NavMeshAgent.isStopped = false;
                    sphereLayerMask = LayerMask.GetMask("StoneNodes");
                    m_NavMeshAgent.SetDestination(GetClosestInactiveNodeVector());
                    Target = GetTarget();
                    Target.gameObject.tag = "WorkActive";
                    m_Animator.SetBool("IsWalking", true);
                    m_NavMeshAgent.stoppingDistance = 3;

                    state = State.MovingToJobNode;
                }
                break;

            case State.MovingToJobNode:

                Debug.Log("State: MovingToJobNode");                            
                if (m_NavMeshAgent.pathPending)
                {
                    distanceToNode = Vector3.Distance(transform.position, GetClosestInactiveNodeVector());
                }
                else
                {
                    distanceToNode = m_NavMeshAgent.remainingDistance;
                }
                Debug.Log("MovingToJobNode - Distance to Job Node: " + distanceToNode);
                if (distanceToNode <= m_NavMeshAgent.stoppingDistance)
                {
                    Debug.Log("JobNode reached");
                    m_Animator.SetBool("IsWalking", false);
                    m_Animator.SetBool("IsLumbering", true);

                    state = State.WorkingOnJob;
                }
                break;

            case State.WorkingOnJob:
                Debug.Log("State: WorkingOnJob");
                if (inventoryAmount < inventorySize)
                {                                       
                    workTime += Time.deltaTime;
                    transform.LookAt(m_NavMeshAgent.destination);
                    if (workTime >= 12 / workSpeed)
                    {
                        if (this.gameObject.tag == "Woodcutter")
                        {
                            // Try and find an EnemyHealth script on the gameobject hit.
                            TreeNode woodAmount = GetNodeCollider().GetComponent<TreeNode>();
                            // If the EnemyHealth component exist...
                            if (woodAmount != null)
                            {
                                // ... the enemy should take damage.
                                woodAmount.TakeDamage((int)workTime);
                                inventoryAmount += 1;
                            }
                            workTime -= (int)workTime;
                        }
                        if (this.gameObject.tag == "Stonecutter")
                        {
                            // Try and find an EnemyHealth script on the gameobject hit.
                            StoneNode stoneAmount = GetNodeCollider().GetComponent<StoneNode>();
                            // If the EnemyHealth component exist...
                            if (stoneAmount != null)
                            {
                                // ... the enemy should take damage.
                                stoneAmount.TakeDamage((int)workTime);
                                inventoryAmount += 1;
                            }
                            workTime -= (int)workTime;
                        }
                            
                    }
                }
                else
                {
                    Debug.Log("Job Done");
                    if (Target != null)
                    {
                        Target.gameObject.tag = "WorkInactive";
                    }
                    sphereLayerMask = LayerMask.GetMask("StorageNodes");
                    m_NavMeshAgent.SetDestination(GetClosestInactiveNodeVector());
                    Target = GetTarget();
                    Target.gameObject.tag = "WorkInactive";
                    m_Animator.SetBool("IsWalking", true);
                    m_Animator.SetBool("IsLumbering", false);
                    state = State.MovingToStorage;
                }
                break;

            case State.MovingToStorage:
                Debug.Log("State: MovingToStorage");
                if (m_NavMeshAgent.pathPending)
                {
                    distanceToNode = Vector3.Distance(transform.position, GetClosestInactiveNodeVector());
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
                    state = State.LoadToStorage;
                }
                break;

            case State.LoadToStorage:
                Debug.Log("State: LoadingToStorage");
                if (inventoryAmount > 0)
                {
                    workTime += Time.deltaTime;
                    if (workTime >= 1)
                    {
                        if (this.gameObject.tag == "Unemployed")
                        {
                            inventoryAmount -= (int)workTime;
                            ResourceBank.AddWoodToStock((int)workTime);

                            workTime -= (int)workTime;
                        }

                        if (this.gameObject.tag == "Woodcutter")
                        {
                            inventoryAmount -= (int)workTime;
                            ResourceBank.AddWoodToStock((int)workTime);

                            workTime -= (int)workTime;
                        }
                        if (this.gameObject.tag == "Stonecutter")
                        {
                            inventoryAmount -= (int)workTime;
                            ResourceBank.AddStoneToStock((int)workTime);

                            workTime -= (int)workTime;
                        }
                    }
                    
                }
                else
                {
                    Debug.Log("Loading complete");
                    if (this.gameObject.tag == "Unemployed")
                    {
                        sphereLayerMask = LayerMask.GetMask("SafePlaceNodes");
                        m_NavMeshAgent.SetDestination(GetClosestInactiveNodeVector());
                        Target = GetTarget();
                        m_Animator.SetBool("IsWalking", true);
                        m_NavMeshAgent.stoppingDistance = 7;

                        state = State.MovingToFire;
                    }

                    if (this.gameObject.tag == "Woodcutter")
                    {
                        sphereLayerMask = LayerMask.GetMask("TreeNodes");
                        if (GetInactiveNodesCount() != 0)
                        {
                            Target = GetTarget();                                                      
                            m_NavMeshAgent.SetDestination(GetClosestInactiveNodeVector());
                            Target.gameObject.tag = "WorkActive";
                            m_Animator.SetBool("IsWalking", true);
                            m_NavMeshAgent.stoppingDistance = 3;

                            state = State.MovingToJobNode;
                        }
                        else
                        {
                            sphereLayerMask = LayerMask.GetMask("SafePlaceNodes");
                            Target = GetTarget();
                            m_NavMeshAgent.SetDestination(GetClosestInactiveNodeVector());
                            m_Animator.SetBool("IsWalking", true);
                            m_NavMeshAgent.stoppingDistance = 7;

                            state = State.MovingToFire;
                        }
                        
                       
                    }

                    if (this.gameObject.tag == "Stonecutter")
                    {
                        sphereLayerMask = LayerMask.GetMask("StoneNodes");
                        m_NavMeshAgent.SetDestination(GetClosestInactiveNodeVector());
                        Target = GetTarget();
                        Target.gameObject.tag = "WorkActive";
                        m_Animator.SetBool("IsWalking", true);

                        state = State.MovingToJobNode;
                    }
                }
                break;

            case State.MovingToFire:
                Debug.Log("State: MovingToFire");
                if (m_NavMeshAgent.pathPending)
                {
                    distanceToNode = Vector3.Distance(transform.position, GetClosestInactiveNodeVector());
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

                    state = State.IdleAtFire;
                }
                break;

        }
    }
}


                