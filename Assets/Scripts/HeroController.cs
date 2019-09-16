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
    private HeroInfo hero;

    public Collider target;
    [SerializeField] private bool targetInRange = false;
    [SerializeField] private bool targetReached = true;
    private float distanceToTarget;

    [SerializeField] public bool canConsumeFire = false;
    private SphereCollider attackRange;
    private float attackTime = 0.00f;
    [SerializeField] private float attackSpeed = 2f;
    [SerializeField] private bool isAttacking = false;
    public float autoDamage = 0.5f;

    [SerializeField] private Collider enemyTarget;
    private EnemyInfo enemyInfo;

    [SerializeField] private State state;
    private enum State
    {
        Idling,
        Moving,
        Attacking,
    }
    [SerializeField] private AttackType attack;
    private enum AttackType
    {
        RClick,
    }



    void Awake()
    {
        m_NavAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        selectable = GetComponent<SelectableObject>();
        m_Animator = this.transform.GetChild(0).GetComponent<Animator>();
        selectionManager = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        hero = GetComponent<HeroInfo>();
        attackRange = this.transform.Find("AttackRange").GetComponent<SphereCollider>();

        state = State.Idling;
    }

    void Update()
    {
        if (selectable.isSelected == true)
        {
            //if (selectionManager.isActive == true)
            //{
            //    selectionManager.SetActive(false);
            //}

            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("Hero Right Click");
                DetectClickedObject();
            }
        }

        switch (state)
        {
            case State.Idling:
                if (target != null)
                {
                    state = State.Moving;
                }
                break;

            case State.Moving:
                // If there is no Enemy target just move to click position
                if (enemyTarget == null)
                {
                    CheckTargetReached();
                    if (targetReached == true)
                    {
                        state = State.Idling;
                    }
                }
                // Else move to enemy until in attack range
                else
                {
                    if (enemyTarget != null)
                    {
                        CheckAttackInRange(AttackType.RClick);
                        if (targetInRange)
                        {
                            m_NavAgent.isStopped = true;
                            m_NavAgent.ResetPath();
                            state = State.Attacking;
                        }
                    }
                }                
                break;

            case State.Attacking:
                if (enemyTarget != null && targetInRange)
                {
                    transform.LookAt(enemyTarget.transform);

                    if (!isAttacking)
                    {
                        StartCoroutine(Attack(AttackType.RClick));
                    }
                }
                else if (enemyTarget != null && !targetInRange)
                {
                    m_NavAgent.isStopped = false;
                    MoveToTarget();
                    state = State.Moving;
                }
                else
                {
                    targetInRange = false;
                    targetReached = false;
                    m_NavAgent.isStopped = false;
                    state = State.Idling;
                }

                break;
        }      
    }

    public void ConsumeFire(int amount)
    {
        ResourceBank.RemoveFireLife(amount);
        hero.firePower += amount;
    }

    private void DetectClickedObject()
    {
        //enemyTarget = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Enemy")))
        {
            if (hit.transform.gameObject.GetComponent<Attackable>() != null )
            {
                enemyTarget = hit.collider;
                MoveToClickPos(hit);
                state = State.Moving;                
                Debug.Log("Clicked on Enemy");
            }
        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity, moveClickLayer))
        {
            MoveToClickPos(hit);
            targetReached = false;

            enemyTarget = null;
            state = State.Moving;
            Debug.Log("Clicked on Terrain - Moving to Position");
        }

    }

    
    private IEnumerator Attack(AttackType attackType)
    {
        isAttacking = true;
        m_Animator.SetBool("IsRunning", false);
        m_Animator.Play("spell2(stick)");

        yield return new WaitForSeconds(1.25f);
        if (enemyTarget != null)
        {
            Debug.Log("Attack");
            GameObject fireball = Instantiate(Resources.Load("PS_Fireball")) as GameObject;
            fireball.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z);
            fireball.GetComponent<Projectile>().target = enemyTarget;
            fireball.GetComponent<Projectile>().damage = autoDamage;
        }
        yield return new WaitForSeconds(1f); // Cooldown
        isAttacking = false;



        //attackTime += Time.deltaTime;
        //if (attackTime >= attackSpeed)
        //{
        //    if (enemyTarget != null)
        //    {
        //        // Try and find a Enemy Info script on the gameobject hit.
        //        //EnemyInfo enemy = enemyTarget.GetComponent<EnemyInfo>();
        //        //HeroInfo info = GetComponent<HeroInfo>();
        //        // If the Node Resource script component exists...

        //        // Cast Attack
        //        Debug.Log("Attack");
        //        attackTime -= (int)attackTime;

        //    }
        //}
    }

    private void MoveToTarget()
    {
        if (enemyTarget != null)
        {
            m_NavAgent.isStopped = false;
            m_NavAgent.SetDestination(enemyTarget.transform.position);
            m_Animator.SetBool("IsRunning", true);
        }
    }


    private bool CheckAttackInRange(AttackType attackType)
    {
        if (attackType == AttackType.RClick) // Standard Attack
        {
            attackRange.radius = 15;
            if (enemyTarget != null)
            {
                if (targetInRange == true)
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
        else
        {
            return false;
        }
    }

    public void MoveToClickPos(RaycastHit hit)
    {
        targetReached = false;
        
        m_NavAgent.SetDestination(hit.point);
        target = hit.transform.gameObject.GetComponent<Collider>();
        m_Animator.SetBool("IsRunning", true);
        Debug.Log("Hero - Click: Moving to: " + hit.transform.gameObject.GetComponent<Collider>() + " " + Input.mousePosition);
                     
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
            //Debug.Log("Enemy Target in Range");
            targetInRange = true;
        }
        if (other.tag == "FireConsume")
        {
            canConsumeFire = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == enemyTarget)
        {
            //Debug.Log("Enemy Target not longer in Range");
            targetInRange = false;
        }
        if (other.tag == "FireConsume")
        {
            canConsumeFire = false;
        }
    }

}
