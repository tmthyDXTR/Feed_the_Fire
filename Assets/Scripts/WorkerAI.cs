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
        Idle,
        MovingToJobNode,
        WorkingOnJob,
        MovingToStorage,
        LoadToStorage,
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
        Debug.Log("Colliders: " + nodeColliders.Length);
        
        float bestDistance = 999999.0f;
                               
        foreach (Collider node in nodeColliders)
        {
            float distance = Vector3.Distance(node.ClosestPoint(nextNode), transform.position);
            if (node.gameObject.tag != "WorkActive")
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
        Debug.Log("Colliders: " + nodeColliders.Length);

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

         
    void Awake()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        //Visualize Search Sphere
        searchSphere = transform.Find("SearchSphere");
        searchSphere.localScale += new Vector3(sphereRadius, sphereRadius, sphereRadius);

        //Set Idle State
        state = State.Idle;

        //Set Inventory Amount
        inventoryAmount = 0;

        gameObject.tag = "Unemployed";
    }


    void Update()
    {

        if (this.gameObject.tag == "Woodcutter")
        {
            switch (state)
            {
                case State.Idle:
                    sphereLayerMask = LayerMask.GetMask("TreeNodes");
                    m_NavMeshAgent.SetDestination(GetClosestInactiveNodeVector());

                    Target = GetTarget();
                    Target.gameObject.tag = "WorkActive";


                    m_Animator.SetBool("IsWalking", true);
                    state = State.MovingToJobNode;
                    break;

                case State.MovingToJobNode:
                    if (m_NavMeshAgent.pathPending)
                    {
                        distanceToNode = Vector3.Distance(transform.position, GetClosestInactiveNodeVector());
                    }
                    else
                    {
                        distanceToNode = m_NavMeshAgent.remainingDistance;
                    }

                    if (distanceToNode <= m_NavMeshAgent.stoppingDistance)
                    {
                        m_Animator.SetBool("IsWalking", false);
                        state = State.WorkingOnJob;
                    }
                    break;

                case State.WorkingOnJob:
                    if (inventoryAmount < inventorySize)
                    {
                        m_Animator.SetBool("IsLumbering", true);
                        workTime += Time.deltaTime;
                        transform.LookAt(m_NavMeshAgent.destination);
                        if (workTime >= 12 / workSpeed)
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
                    }
                    else
                    {
                        if (Target != null)
                        {
                            Target.gameObject.tag = "WorkInactive";

                        }
                        m_Animator.SetBool("IsLumbering", false);
                        sphereLayerMask = LayerMask.GetMask("StorageNodes");
                        m_NavMeshAgent.SetDestination(GetClosestInactiveNodeVector());


                        m_Animator.SetBool("IsWalking", true);
                        state = State.MovingToStorage;

                    }
                    break;

                case State.MovingToStorage:
                    if (m_NavMeshAgent.pathPending)
                    {
                        distanceToNode = Vector3.Distance(transform.position, GetClosestInactiveNodeVector());
                    }
                    else
                    {
                        distanceToNode = m_NavMeshAgent.remainingDistance;
                    }

                    if (distanceToNode <= m_NavMeshAgent.stoppingDistance)
                    {
                        m_Animator.SetBool("IsWalking", false);
                        state = State.LoadToStorage;
                    }
                    //Debug.Log("Distance to Storage Node: " + distanceToStorageNode);
                    break;

                case State.LoadToStorage:
                    if (inventoryAmount > 0)
                    {
                        workTime += Time.deltaTime;
                        if (workTime >= 1)
                        {
                            inventoryAmount -= (int)workTime;
                            ResourceBank.AddWoodToStock((int)workTime);

                            workTime -= (int)workTime;
                        };
                    }
                    else
                    {
                        state = State.Idle;
                    }
                    break;
            }
        }


        if (this.gameObject.tag == "Stonecutter")
        {
            switch (state)
            {
                case State.Idle:
                    sphereLayerMask = LayerMask.GetMask("StoneNodes");
                    m_NavMeshAgent.SetDestination(GetClosestInactiveNodeVector());

                    Target = GetTarget();
                    Target.gameObject.tag = "WorkActive";


                    m_Animator.SetBool("IsWalking", true);
                    state = State.MovingToJobNode;
                    break;

                case State.MovingToJobNode:
                    if (m_NavMeshAgent.pathPending)
                    {
                        distanceToNode = Vector3.Distance(transform.position, GetClosestInactiveNodeVector());
                    }
                    else
                    {
                        distanceToNode = m_NavMeshAgent.remainingDistance;
                    }

                    if (distanceToNode <= m_NavMeshAgent.stoppingDistance)
                    {
                        m_Animator.SetBool("IsWalking", false);
                        state = State.WorkingOnJob;
                    }
                    break;

                case State.WorkingOnJob:
                    if (inventoryAmount < inventorySize)
                    {
                        m_Animator.SetBool("IsLumbering", true);
                        workTime += Time.deltaTime;
                        transform.LookAt(m_NavMeshAgent.destination);
                        if (workTime >= 12 / workSpeed)
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
                    else
                    {
                        if (Target != null)
                        {
                            Target.gameObject.tag = "WorkInactive";

                        }
                        m_Animator.SetBool("IsLumbering", false);
                        sphereLayerMask = LayerMask.GetMask("StorageNodes");
                        m_NavMeshAgent.SetDestination(GetClosestInactiveNodeVector());


                        m_Animator.SetBool("IsWalking", true);
                        state = State.MovingToStorage;

                    }
                    break;

                case State.MovingToStorage:
                    if (m_NavMeshAgent.pathPending)
                    {
                        distanceToNode = Vector3.Distance(transform.position, GetClosestInactiveNodeVector());
                    }
                    else
                    {
                        distanceToNode = m_NavMeshAgent.remainingDistance;
                    }

                    if (distanceToNode <= m_NavMeshAgent.stoppingDistance)
                    {
                        m_Animator.SetBool("IsWalking", false);
                        state = State.LoadToStorage;
                    }
                    //Debug.Log("Distance to Storage Node: " + distanceToStorageNode);
                    break;

                case State.LoadToStorage:
                    if (inventoryAmount > 0)
                    {
                        workTime += Time.deltaTime;
                        if (workTime >= 1)
                        {
                            inventoryAmount -= (int)workTime;
                            ResourceBank.AddStoneToStock((int)workTime);

                            workTime -= (int)workTime;
                        };
                    }
                    else
                    {
                        state = State.Idle;
                    }
                    break;
            }
        }
    }
}