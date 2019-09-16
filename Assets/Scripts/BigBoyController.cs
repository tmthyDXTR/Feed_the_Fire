using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoyController : MonoBehaviour
{
    private EnemyInfo info;
    private SphereCollider detectionSphere;
    private UnityEngine.AI.NavMeshAgent navAgent;
    private Animator animator;
    [SerializeField] private GameObject target;
    [SerializeField] private bool isAggro = false; // Debug

    public State state;
    [SerializeField] public enum State
    {
        Idling,
        Moving,
    }

    void Awake()
    {
        detectionSphere = this.transform.Find("DetectionSphere").GetComponent<SphereCollider>();
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        info = GetComponent<EnemyInfo>();
        state = State.Idling;
    }

    void Update()
    {
        //if (target != null)
        //{
        //    transform.LookAt(target.transform.position);            
        //}

        if (!isAggro)
        {
            if (info.currentHealth != info.health)
            {
                Debug.Log(info.name + " is aggro now");
                isAggro = true;
            }
        }

        switch (state)
        {
            case State.Idling:
                //If this guy is aggrod and has a target
                if (isAggro && target != null)
                {
                    //Play Shout and set Aggro - then move to it
                    StartCoroutine(ShoutThenAggro());
                }
                break;


            case State.Moving:
                //Check if Target reached
                MoveTo(target.transform.position);
                CheckTargetReached();
                break;
        }
        
    }

    private IEnumerator ShoutThenAggro()
    {
        animator.Play("Shout");
        yield return new WaitForSeconds(2f);
        state = State.Moving;
    }

    void CheckTargetReached()
    {
        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            navAgent.isStopped = true;
            if (animator.GetBool("IsWalking"))
            {
                animator.SetBool("IsWalking", false);
            }
        }
    }

    void MoveTo(Vector3 position)
    {
        navAgent.isStopped = false;
        navAgent.SetDestination(position);
        if (!animator.GetBool("IsWalking"))
        {
            animator.SetBool("IsWalking", true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hero")
        {
            target = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        //if (other.tag == "Hero")
        //{
        //    target = null;
        //}
    }

}
