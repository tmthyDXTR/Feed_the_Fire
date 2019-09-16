using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZombie : MonoBehaviour
{
    public float radius;
    public List<Collider> inRangeTargets = new List<Collider>();
    public Collider target;
    public Collider bonfire;
    private bool hasTarget;
    private Vector3 spawnPosition; // Roaming center
    UnityEngine.AI.NavMeshAgent m_NavMeshAgent;
    Animator m_Animator;

    private UnitInfo unit;

    [SerializeField] private State state;
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
        m_NavMeshAgent.SetDestination(RandomNavmeshLocation(radius));
        m_Animator.SetBool("IsWalking", true);

        state = State.Roaming;
    }

    void Update()
    {
        switch (state)
        {
            case State.Idling:
                //Debug.Log("State: Idling");
                break;

            case State.Roaming:
                //Debug.Log("State: Roaming");
                GameObject fire = GameObject.Find("FirePlace");
                target = fire.GetComponent<Collider>();
                MoveToTarget(target.GetComponent<Collider>());

                if (hasTarget == true)
                {
                    state = State.MovingToTarget;
                }
                
                break;

            case State.MovingToTarget:
                //Debug.Log("State: MovingToTarget");
                // Check if Target reached
                TargetCheck();
                if (TargetReached() == true)
                {
                    state = State.AttackingTarget;
                }
                if (hasTarget == false || target == null)
                {
                    DeRegisterTarget(target);
                    state = State.Roaming;
                }
                if (bonfire != null)
                {
                    state = State.Scared;
                }
                break;

            case State.Scared:
                //Debug.Log("State: Scared");
                m_NavMeshAgent.SetDestination(spawnPosition);
                m_Animator.SetBool("IsWalking", true);
                if (TargetReached() == true)
                {
                    state = State.Roaming;
                }
                break;

            case State.AttackingTarget:
                //Debug.Log("State: AttackingTarget");
                if (target != null)
                {
                    m_Animator.SetBool("IsAttacking", true);
                    transform.LookAt(target.transform.position);
                }
                else if (target == null)
                {
                    //Check if Target is dead or null                    
                    state = State.Roaming;
                }
                if (bonfire != null)
                {
                    state = State.Scared;
                }
                break;
        }
    }

    //// Animation Event
    //private void AttackEnd()
    //{
    //    // Send Damange to Target
    //    unit = target.transform.GetComponent<UnitInfo>();
    //    unit.TakeDamage(this.gameObject, 10);
    //}

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

    void MoveToTarget(Collider target)
    {
        m_NavMeshAgent.SetDestination(target.transform.position);
        m_Animator.SetBool("IsAttacking", false);
        m_Animator.SetBool("IsWalking", true);
    }

    void TargetCheck()
    {
        if (inRangeTargets.Count >= 1)
        {
            //Debug.Log("Unit in Range");
            hasTarget = true;
            GetNearestTarget();
            foreach (Collider possibleTarget in inRangeTargets)
            {
                if (possibleTarget == null)
                {
                    DeRegisterTarget(possibleTarget);
                }
            }
        }
        else
        {
            //Debug.Log("No Unit in Range");
            hasTarget = false;
            target = null;
        }
    }

    private Collider GetNearestTarget()
    {
        float bestDistance = 999999.0f;
        foreach (Collider possibleTarget in inRangeTargets)
        {
            if (possibleTarget == null)
            {
                DeRegisterTarget(possibleTarget);
            }
            float distance = Vector3.Distance(possibleTarget.ClosestPoint(possibleTarget.transform.position), transform.position);
            if (distance < bestDistance)
            {
                bestDistance = distance;
                target = possibleTarget;
            }
        }
        return target;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 17) // Player Unit layer
        {
            //Debug.Log("Player unit entered collider sphere");
            RegisterTarget(other);
        }
        if (other.gameObject.layer == 13) // Bonfires layer 
        {
            //Debug.Log("Bonfire entered collider sphere");
            DeRegisterTarget(target);
            bonfire = other;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 17) // Player Unit layer
        {
            //Debug.Log("Player unit exited collider sphere");
            DeRegisterTarget(other);
        }
        if (other.gameObject.layer == 13) // Bonfires layer 
        {
            //Debug.Log("Bonfire exited collider sphere");
            bonfire = null;
        }
    }

    public void RegisterTarget(Collider other)
    {
        inRangeTargets.Add(other);
    }

    public void DeRegisterTarget(Collider other)
    {
        inRangeTargets.Remove(other);
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
                    m_Animator.SetBool("IsWalking", true);
                    m_Animator.SetBool("IsAttacking", false);
                }
            }
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
