using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoam : MonoBehaviour
{
    public float radius;
    public List<Collider> inRangeTargets = new List<Collider>();
    public GameObject target;
    public Collider bonfire;
    public bool hasTarget;
    private Vector3 spawnPosition; // Roaming center
    UnityEngine.AI.NavMeshAgent m_NavMeshAgent;
    Animator m_Animator;

    private SphereCollider detectionSphere;
    private UnitInfo unit;
    [SerializeField] private float detectionRadius = 7;
    [SerializeField] private State state;

    public List<Collider> targetList = new List<Collider>();

    private enum State
    {
        Idling,
        Roaming,
        Scared,
        MovingToTarget,
        AttackingTarget
    }


    void Awake()
    {
        spawnPosition = transform.position;
        m_NavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        detectionSphere = this.transform.Find("DetectionSphere").GetComponent<SphereCollider>();
        detectionSphere.radius = detectionRadius;

        m_NavMeshAgent.SetDestination(RandomNavmeshLocation(radius));
        state = State.Roaming;
    }

    void Update()
    {
        switch (state)
        {
            case State.Idling:
                //Debug.Log("State: Idling");
                if (target != null)
                {
                    m_Animator.SetBool("IsWalking", true);
                    m_Animator.SetBool("IsAttacking", false);
                    //Get random point in spawn position radius and go there
                    Roaming();
                    state = State.Roaming;
                }
                break;

            case State.Roaming:
                //Debug.Log("State: Roaming");                                
                //If some unit enters collider radius add it as potential target
                GetNearestTarget();

                if (target != null)
                {
                    state = State.MovingToTarget;
                }
                break;

            case State.MovingToTarget:
                //Debug.Log("State: MovingToTarget");
                GetNearestTarget();

                MoveToTarget();
                // Check if Target reached                
                if (inRangeTargets.Contains(target.GetComponent<Collider>()))
                {
                    m_Animator.SetBool("IsWalking", false);
                    m_Animator.SetBool("IsAttacking", true);
                    state = State.AttackingTarget;
                }
                else if (targetList.Count > 0)
                {
                    m_Animator.SetBool("IsWalking", true);
                    m_Animator.SetBool("IsAttacking", false);
                    state = State.MovingToTarget;
                }
                
                else
                {
                    state = State.Roaming;
                }
                break;

            case State.AttackingTarget:
                //Debug.Log("State: AttackingTarget");
                if (target != null && inRangeTargets.Contains(target.GetComponent<Collider>()))
                {
                    transform.LookAt(target.transform.position);
                }
                else if (targetList.Count > 0)
                {
                    
                    for (int i = targetList.Count - 1; i > -1; i--)
                    {
                        if (targetList[i] == null)
                            targetList.RemoveAt(i);
                    }
                    target = targetList[0].gameObject;
                    m_Animator.SetBool("IsWalking", true);
                    m_Animator.SetBool("IsAttacking", false);
                    state = State.MovingToTarget;
                }
                else
                {
                    //Check if Target is dead or null                    
                    state = State.Roaming;
                }
                break;
        }
    }

    // Animation Event
    private void AttackEnd()
    {
        // Send Damange to Target
        Debug.Log(this.gameObject.name + " attacked " + target.gameObject.name);
        unit = target.transform.GetComponent<UnitInfo>();
        HeroInfo hero = target.transform.parent.GetComponent<HeroInfo>();
        if (unit != null)
        {
            unit.TakeDamage(5f);
        }
        if (hero != null)
        {
            hero.TakeDamage(5f);
        }
    }

    private bool TargetReached()
    {
        if (!m_NavMeshAgent.pathPending)
        {
            if (m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance)
            {
                if (!m_NavMeshAgent.hasPath || m_NavMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    m_Animator.SetBool("IsWalking", false);
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
        else
        {
            return false;
        }
    }

    void MoveToTarget()
    {
        if (target != null)
        {
            m_NavMeshAgent.SetDestination(target.transform.position); 
        }
    }
        
    void Roaming()
    {
        if (!m_NavMeshAgent.pathPending)
        {
            if (m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance)
            {
                if (!m_NavMeshAgent.hasPath || m_NavMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    m_NavMeshAgent.SetDestination(RandomNavmeshLocation(radius));
                }
            }
        }        
    }

    private void GetNearestTarget()
    {
        if (targetList.Count > 0)
        {
            Collider nearestTarget = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = transform.position;
            foreach (Collider target in targetList)
            {
                if (target == null)
                {
                    targetList.Remove(target);
                }
                float dist = Vector3.Distance(target.transform.position, currentPos);
                if (dist < minDist)
                {
                    nearestTarget = target;
                    minDist = dist;
                }
            }
            target = nearestTarget.gameObject;
        }
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += spawnPosition;
        UnityEngine.AI.NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    
}
