using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeroController : MonoBehaviour
{
    public LayerMask moveClickLayer;
    private UnityEngine.AI.NavMeshAgent m_NavAgent;
    private SelectableObject selectable;
    public Animator m_Animator;
    private SelectionManager selectionManager;
    private BonfireManager bonfireManager;
    private HeroInfo hero;
    private Vector3 mousePos;
    private float globalCoolDown = 1f;

    //public Collider target;
    //[SerializeField] private bool targetInRange = false;
    [SerializeField] private bool isMoving = false;
    public bool isAttacking = false;
    public bool isStrafing = false;
    public bool isHit = false;
    public bool isStunned = false;
    public bool canConsumeFire = false;
    public bool isDead;

    private SphereCollider attackRangeSphere;
    //private float attackTime = 0.00f;
    //[SerializeField] private float attackRange = 15f;
    //[SerializeField] private float attackSpeed = 2f;
    //public float autoDamage = 0.5f;
    public float runSpeed = 7f;
    public float strafeSpeed = 14f;

    [SerializeField] private State state;
    private enum State
    {
        Idling,
        Moving,
        Attacking,
        Strafing,
        Stunned,
        Dead,
    }
    public Slot slot;
    public enum Slot
    {
        Slot_RClick,
        Slot_1,
        Slot_2,
        Slot_3,
        Slot_4,
    }
    [SerializeField] private GameObject selectedAttackSkill;

    public Collider consumeFire;

    private EnemyInfo enemyInfo;
    public Collider targetObject;
    [SerializeField] private float distanceToTarget;
    [SerializeField] private Vector3 targetHit;
    public List<Collider> inRangeTargets = new List<Collider>();


    public float RClick_powerMultiplicator = 1.0f; // Dependent on fire power level

    public float time = 0.0f;
    public float fireConsumeTicker = 1f;

    private float pendingCost;
    void Awake()
    {
        m_NavAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        m_NavAgent.speed = runSpeed;
        selectable = GetComponent<SelectableObject>();
        //Hardcoded animator reference
        m_Animator = this.transform.GetChild(1).GetComponent<Animator>();
        selectionManager = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        bonfireManager = GameObject.Find("BonfireManager").GetComponent<BonfireManager>();
        hero = GetComponent<HeroInfo>();
        attackRangeSphere = this.transform.Find("AttackRange").GetComponent<SphereCollider>();
        SelectAttackSlot(Slot.Slot_RClick, this.transform.Find("Skills").GetChild(0).gameObject);
        selectedAttackSkill = this.transform.Find("Skills").GetChild(0).gameObject;
        state = State.Idling;

        BonfireManager.OnBonfireAmountChanged += delegate (object sender, EventArgs e)
        {
            UpdateBonfireBuffs();
        };
    }

    

    void Update()
    {
        // PLAY HIT ANIMATION
        if (isHit)
        {
            if (!isStunned && !isDead)
            {
                isStunned = true;
                m_NavAgent.isStopped = true;
                m_NavAgent.ResetPath();
                m_Animator.Play("hit(stick)");
                StartCoroutine(StunDuration(0.5f));
                state = State.Stunned;
            }
        }
        if (hero.currentHealth <= 0)
        {
            if (!isDead)
            {
                isDead = true;
                m_NavAgent.isStopped = true;
                m_NavAgent.ResetPath();
                m_Animator.Play("death(stick)");
                Destroy(this.transform.Find("HitBox").gameObject, 0.1f);

                state = State.Dead;
            }

        }

        else if (selectable.isSelected == true)
        {
            // RIGHT CLICK DETECTION
            if (Input.GetMouseButtonDown(1) && selectionManager.isActive)
            {
                //Debug.Log("Hero Right Click");
                DetectClickedObject();
            }
            // SPACE JUMP
            if (Input.GetKeyUp(KeyCode.Space) && time != 0.000f)
            {
                time -= time;
                GetMousePosition();
                if (!isStrafing && !isDead && hero.power > 0)
                {
                    isStrafing = true;
                    //Debug.Log("Space - Srafe to mouse position");
                    m_Animator.Play("jump_start(stick)");
                    m_Animator.SetBool("IsStrafing", true);
                    m_NavAgent.speed = strafeSpeed;
                    MoveTo(mousePos);
                    state = State.Strafing;
                }
            }
            if (Input.GetKey(KeyCode.Space))
            {
                if (canConsumeFire)
                {
                    time += Time.deltaTime;
                    if (time >= fireConsumeTicker)
                    {
                        ConsumeFire(1);
                        time -= time;
                    }                    
                }
                else
                {
                    GetMousePosition();
                    if (!isStrafing && !isDead && hero.power > 0)
                    {
                        isStrafing = true;
                        //Debug.Log("Space - Srafe to mouse position");
                        m_Animator.Play("jump_start(stick)");
                        m_Animator.SetBool("IsStrafing", true);
                        m_NavAgent.speed = strafeSpeed;
                        MoveTo(mousePos);
                        RemovePower(1);
                        state = State.Strafing;
                    }
                }
            }
            
        }

        // STATES
        switch (state)
        {
            case State.Idling:
                //If clicked on enemy 
                if (targetObject != null)
                {
                    //If in range, attack instantly
                    if (inRangeTargets.Contains(targetObject))
                    {                                               
                        state = State.Attacking;
                    }
                    //Else move towards it
                    else
                    {
                        state = State.Moving;
                    }
                }
                //Else Move to clicked position
                else if (targetHit != Vector3.zero)
                {                                       
                    state = State.Moving;
                }  
                if (slot == Slot.Slot_2)
                {
                    state = State.Attacking;
                }
                break;

            case State.Moving:
                // If there is no Enemy target just move to clicked position
                if (targetObject == null)
                {                    
                    MoveTo(targetHit);                                      
                    CheckTargetReached();
                    //If target reached
                    if (!isMoving)
                    {
                        state = State.Idling;
                    }
                    if (slot == Slot.Slot_2)
                    {
                        state = State.Attacking;
                    }
                }                
                // If there is an enemy 
                else
                {
                    // If Enemy in Range do this
                    if (inRangeTargets.Contains(targetObject))
                    {
                        state = State.Attacking;
                    }
                    // Else move towards it
                    else
                    {
                        targetHit = targetObject.transform.position;
                        MoveTo(targetHit);
                    }
                }                
                break;

            case State.Attacking:
                //Debug.Log("Attacking");
                if(targetObject != null)
                {
                    StartCoroutine(Attack());
                }
                
                if (slot == Slot.Slot_2)
                {
                    StartCoroutine(Attack());
                    if(targetHit != Vector3.zero)
                    {
                        state = State.Moving;
                    }
                    else
                    {
                        state = State.Idling;
                    }
                }
                else if (targetObject == null && targetHit != Vector3.zero && slot != Slot.Slot_2)
                {
                    isAttacking = false;
                    state = State.Moving;
                }

                break;

            case State.Strafing:
                //Debug.Log("Strafing");
                CheckTargetReached();
                if (!isStrafing)
                {
                    m_NavAgent.speed = runSpeed;
                    state = State.Idling;
                }

                break;

            case State.Stunned:
                if (!isStunned)
                {
                    state = State.Idling;
                }
                break;

            case State.Dead:

                break;
        }      
    }

    public void SelectAttackSlot(Slot slotToSelect, GameObject skillObject)
    {
        Attack attack = skillObject.GetComponent<Attack>();
        if (attack != null)
        {
            if (slot != slotToSelect && attack.skill.cost <= hero.power)
            {
                slot = slotToSelect;
                selectedAttackSkill = skillObject;

                // Set Attack Parameters
                attackRangeSphere.radius = attack.skill.range;


                //

                Debug.Log("Selected " + slotToSelect.ToString() + " - " + attack.skill.name);
            }
        }        
    }

    public float PowerMultiplicator()
    {
        RClick_powerMultiplicator = (hero.power / ((float)ResourceBank.fireLifeFull * 2)) * 10;
        if (RClick_powerMultiplicator < 1)
        {
            RClick_powerMultiplicator = 1;
        }
        return RClick_powerMultiplicator;
    }

    private void UpdateBonfireBuffs()
    {
        runSpeed += 0.5f;
        m_NavAgent.speed = runSpeed;
        hero.health += 5f;
    }

    public void ConsumeFire(int amount)
    {
        // The fire power ball moving to the hero from the fireplace
        GameObject projectileObj = Instantiate(Resources.Load("PS_FireConsumeBall")) as GameObject;
        projectileObj.transform.position = consumeFire.transform.position;
        Projectile projectile = projectileObj.GetComponent<Projectile>();
        projectile.target = this.transform.Find("HitBox").GetComponent<Collider>();
        projectile.type = Projectile.Type.FirePower;

        ResourceBank.RemoveFireLife(amount);
        if (hero.currentHealth < hero.health)
        {
            hero.currentHealth += amount;
        }
        AddPower(amount);


    }
    public void AddPower(int amount)
    {
        hero.power += amount;
        PowerMultiplicator();
    }
    public void RemovePower(int amount)
    {
        hero.power -= amount;
        PowerMultiplicator();
    }

    private Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, moveClickLayer))
        {
            mousePos = hit.point;
        }
        return mousePos;
    }

    private Collider DetectClickedObject()
    {
        m_NavAgent.isStopped = false;
        //enemyTarget = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Interactable")))
        {
            if (hit.transform.gameObject.GetComponent<Attackable>() != null || hit.transform.parent.transform.gameObject.GetComponent<Attackable>())
            {
                targetObject = hit.collider;
                targetHit = hit.point;
                //MoveToClickPos(hit);
                //state = State.Moving;                
                //Debug.Log("RClicked on " + targetObject.transform.gameObject.GetComponent<EnemyInfo>().name);
                return targetObject;
            }
            else
            {
                return null;
            }
        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity, moveClickLayer))
        {
            //MoveToClickPos(hit);
            //targetReached = false;

            targetHit = hit.point;
            //state = State.Moving;
            if (targetObject != null)
            {
                targetObject = null;
            }            
            return null;
        }
        else
        {
            return null;
        }

    }

    
    private IEnumerator Attack()
    {
        Attack attack = selectedAttackSkill.GetComponent<Attack>();
        //Check if enough power for skill
        if (hero.power >= attack.skill.cost)
        {
            //Check if hero needs to stand still for the skill
            if (attack.skill.mustStayForCast)
            {
                StopMoving();
                //If attack is area effect
                

                //If attack is projectile
                if (!isAttacking && inRangeTargets.Contains(targetObject))
                {                    
                    isAttacking = true;
                    //Look at target
                    transform.LookAt(targetObject.transform);
                    //Initiate casting
                    m_Animator.Play("spell2(stick)");
                    //Cast time
                    yield return new WaitForSeconds(attack.skill.castTime);
                    //Cast Skill
                    attack.CastAttack(slot);
                    //Reset to RClick Attack
                    if (slot != Slot.Slot_RClick)
                    {
                        SelectAttackSlot(Slot.Slot_RClick, this.transform.Find("Skills").GetChild(0).gameObject);
                    }
                    //Cooldown time
                    yield return new WaitForSeconds(globalCoolDown);
                    isAttacking = false;                    
                }
            } 
            //If hero does not have to stand still attack instantly
            else
            {
                if (!isAttacking && slot == Slot.Slot_2)
                {
                    isAttacking = true;
                    //Initiate casting
                    m_Animator.Play("spell1(stick)");
                    //Cast time
                    yield return new WaitForSeconds(attack.skill.castTime);
                    //Cast Skill
                    attack.CastAttack(slot);
                    //Reset to RClick Attack
                    if (slot != Slot.Slot_RClick)
                    {
                        SelectAttackSlot(Slot.Slot_RClick, this.transform.Find("Skills").GetChild(0).gameObject);
                    }
                    //Cooldown time
                    yield return new WaitForSeconds(globalCoolDown);
                    isAttacking = false;
                }
            }
        }
    }

    private void MoveTo(Vector3 position)
    {
        //Debug.Log("Moving to: " + position);
        if (!isMoving)
        {
            isMoving = true;
        }
        if (m_NavAgent.isStopped != false)
        {
            m_NavAgent.isStopped = false;
        }        
        m_NavAgent.SetDestination(position);
        //m_Animator.SetBool("IsAttacking", false);
        m_Animator.SetBool("IsRunning", true);        
    }

    private void StopMoving()
    {
        if (isMoving)
        {            
            m_Animator.SetBool("IsRunning", false);
            m_NavAgent.ResetPath();
            m_NavAgent.isStopped = true;
            isMoving = false;
        }        
    }

    private void CheckTargetReached()
    {
        if (m_NavAgent.pathPending)
        {
            distanceToTarget = Vector3.Distance(transform.position, targetHit);
        }
        else
        {
            distanceToTarget = m_NavAgent.remainingDistance;
        }
        if (distanceToTarget <= m_NavAgent.stoppingDistance)
        {
            Debug.Log("Hero - Target reached");
            m_Animator.SetBool("IsRunning", false);
            m_Animator.SetBool("IsStrafing", false);
            isStrafing = false;
            isMoving = false;
            m_NavAgent.isStopped = true;
            targetHit = Vector3.zero;
            //target = null;
        }
    }

    IEnumerator StunDuration(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isStunned = false;
        m_NavAgent.isStopped = false;
    }

    private void OnTriggerEnter(Collider other)
    {
    }

    private void OnTriggerExit(Collider other)
    {
    }

}
