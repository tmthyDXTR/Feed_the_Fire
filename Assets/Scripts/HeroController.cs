using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public LayerMask moveClickLayer;
    private UnityEngine.AI.NavMeshAgent m_NavAgent;
    private SelectableObject selectable;
    private Animator m_Animator;
    private SelectionManager selectionManager;

    public Collider target;
    [SerializeField] private bool targetInRange = false;
    [SerializeField] private bool targetReached = true;
    private float distanceToTarget;

    private SphereCollider attackRange;

    [SerializeField] private Collider enemyTarget;
    private EnemyInfo enemyInfo;

    void Awake()
    {
        m_NavAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        selectable = GetComponent<SelectableObject>();
        m_Animator = this.transform.GetChild(0).GetComponent<Animator>();
        selectionManager = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();

        attackRange = this.transform.Find("AttackRange").GetComponent<SphereCollider>();
    }

    void Update()
    {
        if (selectable.isSelected == true)
        {
            if (selectionManager.isActive == true)
            {
                selectionManager.SetActive(false);
            }

            //selectionManager.SetActive(false);

            if (Input.GetMouseButtonDown(0))
            {
                DetectClickedObject();
            }

        }

        if (target != null)
        {
            CheckTargetReached();
        }

    }

    private void DetectClickedObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Enemy")))
        {
            if (hit.transform.gameObject.tag == "Enemy")
            {
                enemyTarget = hit.collider;
                if (CheckAttackInRange(1) == true)
                {
                    Attack();
                }
                else
                {
                    MoveToTarget();
                }
            }
        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity, moveClickLayer))
        {
            MoveToClickPos();
        }

    }

    
    private void Attack()
    {
        Debug.Log("Attack");
    }

    private void MoveToTarget()
    {
        if (enemyTarget != null)
        {
            m_NavAgent.SetDestination(enemyTarget.transform.position);
            m_Animator.SetBool("IsRunning", true);
        }
    }

    private void CastAttack()
    {
        Debug.Log("Cast Attack");
    }

    private bool CheckAttackInRange(int attackType)
    {
        if (attackType == 1) // Standard Attack
        {
            attackRange.radius = 10;
            if (enemyInfo != null)
            {
                if (targetInRange == true)
                {
                    return true;
                    Debug.Log("Now Atacking " + enemyInfo.name);
                }
                else
                {
                    return false;
                    Debug.Log(enemyInfo.name + " out of Range");
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

    public void MoveToClickPos()
    {
        targetReached = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, moveClickLayer))
        {

            m_NavAgent.SetDestination(hit.point);
            target = hit.collider;
            m_Animator.SetBool("IsRunning", true);
            Debug.Log("Hero - Click: Moving to: " + hit.collider + " " + Input.mousePosition);
        }               
    }

    private void CheckTargetReached()
    {
        if (m_NavAgent.pathPending)
        {
            distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        }
        else
        {
            distanceToTarget = m_NavAgent.remainingDistance;
        }
        if (distanceToTarget <= m_NavAgent.stoppingDistance || targetInRange == true)
        {
            Debug.Log("Hero - Target reached");
            //m_NavAgent.isStopped = true;
            m_Animator.SetBool("IsRunning", false);
            targetReached = true;
            target = null;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other == enemyTarget)
        {
            Debug.Log("Enemy Target in Range");
            targetInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == enemyTarget)
        {
            Debug.Log("Enemy Target not longer in Range");
            targetInRange = false;
        }
    }

}
