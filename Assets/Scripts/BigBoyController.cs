using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoyController : MonoBehaviour
{
    private Transform damageBoxes;
    private Transform closeDamageBox1;
    private Transform closeDamageBox2;


    private EnemyInfo info;
    private SphereCollider detectionSphere;
    private UnityEngine.AI.NavMeshAgent navAgent;
    private Animator animator;
    [SerializeField] private GameObject target;
    [SerializeField] private bool isAggro = false;
    [SerializeField] private bool isAttacking = false;
    public bool isTooClose = false;
    public bool isHit = false;
    public bool isDead = false;
    [SerializeField] private float damage;
    [SerializeField] private float damageRadius;
    [SerializeField] private float rotationSpeed = 1.25f;
    public bool isRotating = false;


    private bool targetReached = false;

    public List<Collider> inRangeTargets = new List<Collider>();
    public List<Collider> tooCloseTargets = new List<Collider>();

    public State state;
    [SerializeField] public enum State
    {
        Idling,
        Moving,
        Attacking,
        Dead,
    }

    public AttackType attack;
    public enum AttackType
    {
        Kick_BothArms,
        Kick_Close,
        Kick_Foot,
    }

    void Awake()
    {
        // DamageBoxes
        damageBoxes = this.transform.Find("DamageBoxes");
        closeDamageBox1 = damageBoxes.Find("CloseDamageBox1");
        closeDamageBox1 = damageBoxes.Find("CloseDamageBox2");

        //
        detectionSphere = this.transform.Find("DetectionSphere").GetComponent<SphereCollider>();
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        info = GetComponent<EnemyInfo>();


        state = State.Idling;
        attack = AttackType.Kick_BothArms;
    }

    void Update()
    {
        if (isRotating)
        {
            Vector3 targetDir = target.transform.position - transform.position;

            // The step size is equal to speed times frame time.
            float step = rotationSpeed * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);

            transform.rotation = Quaternion.LookRotation(newDir);
        }

        if (info.currentHealth <= 0)
        {
            Death();
        }

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
                if (inRangeTargets.Contains(target.GetComponent<Collider>()))
                {
                    state = State.Attacking;
                }
                break;

            case State.Attacking:
                if (!isAttacking)
                {
                    if (!isTooClose)
                    {
                        StartCoroutine(Attack(attack));
                    }
                    else
                    {
                        StartCoroutine(Attack(AttackType.Kick_Close));
                    }

                }
                else if (!inRangeTargets.Contains(target.GetComponent<Collider>()))
                {
                    state = State.Moving;
                }
                break;

            case State.Dead:
                break;
        }
        
    }

    private IEnumerator Attack(AttackType attack)
    {
        isAttacking = true;
        // INSERT TYPES HERE -----
        if (attack == AttackType.Kick_BothArms)
        {
            StartCoroutine(RotateToTarget(0.33f));

            animator.SetBool("IsWalking", false);
            navAgent.isStopped = true;
            navAgent.ResetPath();
            animator.Play("Kick_BothArms");
            
            yield return new WaitForSeconds(2f); // Cooldown            
            isAttacking = false;

            //yield return new WaitForSeconds(1f);
            //isRotating = true;
        }
        if (attack == AttackType.Kick_Close)
        {
            animator.SetBool("IsWalking", false);
            navAgent.isStopped = true;
            navAgent.ResetPath();
            animator.Play("Kick_Close");
            yield return new WaitForSeconds(1f);
            //GameObject fireball = Instantiate(Resources.Load("PS_Fireball")) as GameObject;
            //fireball.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z);
            //fireball.GetComponent<Projectile>().target = targetObject;
            //fireball.GetComponent<Projectile>().damage = autoDamage;

            yield return new WaitForSeconds(1f); // Cooldown
            isAttacking = false;
        }
        //Debug.Log("Kick_BothArms");
        
    }

    private void AttackClose1()
    {
        StartCoroutine(RotateToTarget(0.33f));

        Debug.Log(info.name + " dealt Damage now");
        GameObject damageBox = Instantiate(Resources.Load("DamageBox"), damageBoxes) as GameObject;
        SphereCollider damageCol = damageBox.GetComponent<SphereCollider>();
        damageCol.center = new Vector3(-0.25f, 0, 1.8f);
        damageCol.radius = damageRadius;
        Vector3 damageBoxPos = damageBox.transform.position;
        DamageBox box = damageBox.GetComponent<DamageBox>();
        box.damage = damage;
        box.radius = damageRadius;

        GameObject impactEffect = Instantiate(Resources.Load("ImpactEffect"), damageBoxPos, Quaternion.identity) as GameObject;
        impactEffect.transform.position = damageBox.transform.TransformPoint(damageCol.center);
        GameObject DebuffSlow = Instantiate(Resources.Load("Debuff_Slow"), damageBoxes) as GameObject;
        //GameObject DebuffSlow = Instantiate(Resources.Load("Debuff_Slow")) as GameObject;

        SphereCollider col = DebuffSlow.GetComponent<SphereCollider>();
        col.center = new Vector3(-0.25f, 0, 1.8f);
        col.radius = damageRadius;
    }
    private void AttackClose2()
    {
        Debug.Log(info.name + " dealt Damage now");
        GameObject damageBox = Instantiate(Resources.Load("DamageBox"), damageBoxes) as GameObject;
        SphereCollider damageCol = damageBox.GetComponent<SphereCollider>();
        damageCol.center = new Vector3(-0.15f, 0, 1f);
        damageCol.radius = damageRadius;
        Vector3 damageBoxPos = damageBox.transform.position;
        DamageBox box = damageBox.GetComponent<DamageBox>();
        box.damage = damage;
        box.radius = damageRadius;

        GameObject impactEffect = Instantiate(Resources.Load("ImpactEffect"), damageBoxPos, Quaternion.identity) as GameObject;
        impactEffect.transform.position = damageBox.transform.TransformPoint(damageCol.center);
        GameObject DebuffSlow = Instantiate(Resources.Load("Debuff_Slow"), damageBoxes) as GameObject;
        //GameObject DebuffSlow = Instantiate(Resources.Load("Debuff_Slow")) as GameObject;

        SphereCollider col = DebuffSlow.GetComponent<SphereCollider>();
        col.center = new Vector3(-0.15f, 0, 1f);
        col.radius = damageRadius;
    }

    private IEnumerator RotateToTarget(float time) 
    {
        isRotating = true;
        yield return new WaitForSeconds(time);
        isRotating = false;
    }

    private void AttackDamage()
    {
        Debug.Log(info.name + " dealt Damage now");

        //if (hitBox.gameObject.GetComponent<DamageBox>() == null)
        //{
        //    hitBox.gameObject.AddComponent<DamageBox>();
        //}        
        //SphereCollider damageBox = hitBox.gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;
        //damageBox.radius = 0.65f;
        //damageBox.center = new Vector3(0, 0, 1.6f);
        //hitBox.gameObject.GetComponent<DamageBox>().damage = 20f;

        GameObject damageBox = Instantiate(Resources.Load("DamageBox"), damageBoxes) as GameObject;
        SphereCollider damageCol = damageBox.GetComponent<SphereCollider>();
        
        damageCol.center = new Vector3(0, 0, 1.6f);
        damageCol.radius = damageRadius;
        Vector3 damageBoxPos = damageBox.transform.position;
        DamageBox box = damageBox.GetComponent<DamageBox>();
        box.damage = damage;
        box.radius = damageRadius;
        

        GameObject impactEffect = Instantiate(Resources.Load("ImpactEffect"), damageBoxPos, Quaternion.identity) as GameObject;
        impactEffect.transform.position = damageBox.transform.TransformPoint(damageCol.center);
        GameObject DebuffSlow = Instantiate(Resources.Load("Debuff_Slow"), damageBoxes) as GameObject;
        //GameObject DebuffSlow = Instantiate(Resources.Load("Debuff_Slow")) as GameObject;
        
        SphereCollider col = DebuffSlow.GetComponent<SphereCollider>();
        col.center = new Vector3(0, 0, 1.6f);
        col.radius = damageRadius;
        

        //GameObject damageBox = Instantiate(Resources.Load("DamageBox"), hitBox.position, 
        //Quaternion.identity,
        //this.transform.GetChild(0)) as GameObject;
        //damageBox.transform.position += Vector3.forward * (1.6f);
        //damageBox.transform.position += new Vector3(0, 0, 1.6f);
        //damageBox.transform.SetParent(this.transform.GetChild(0));
        //damageBox.transform.position = this.transform.position + new Vector3 (0, 0, 5f);
        //HitBox Object
    }

    private IEnumerator ShoutThenAggro()
    {
        animator.Play("Shout");
        yield return new WaitForSeconds(2.25f);
        if(!isDead)
        {
            state = State.Moving;
        }
        else
        {
            animator.SetBool("IsWalking", false);
            navAgent.isStopped = true;
            navAgent.ResetPath();
            StartCoroutine(WaitAndDie());
            state = State.Dead;
        }
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
            targetReached = true;
        }
    }

    void MoveTo(Vector3 position)
    {
        targetReached = false;
        navAgent.isStopped = false;
        navAgent.SetDestination(position);
        if (!animator.GetBool("IsWalking"))
        {
            animator.SetBool("IsWalking", true);
        }
    }

    void Death()
    {
        if (!isDead)
        {
            animator.SetBool("IsWalking", false);
            navAgent.isStopped = true;
            navAgent.ResetPath();            
            isDead = true;
            state = State.Dead;
            StartCoroutine(WaitAndDie());
        }

    }

    void DeathImpact()
    {
        GameObject impactEffect = Instantiate(Resources.Load("ImpactEffect")) as GameObject;
        //Get main Impact Collider
        BoxCollider torso = transform.Find("DeadBox").GetChild(0).GetComponent<BoxCollider>();
        impactEffect.transform.position = torso.transform.TransformPoint(torso.center);

        GameObject bloodSplatter = Instantiate(Resources.Load("PS_BloodSplatter")) as GameObject;
        bloodSplatter.transform.position = impactEffect.transform.position;
    }

    private IEnumerator WaitAndDie()
    {
        yield return new WaitForSeconds(1.5f);
        animator.Play("Death");
        gameObject.AddComponent<BigBoyDeadBox>();
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
