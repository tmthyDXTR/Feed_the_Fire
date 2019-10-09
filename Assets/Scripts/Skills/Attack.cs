using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] public Skill skill;
    [SerializeField] private HeroController hero;
    private HeroInfo heroInfo;
    private GameStats gameStats;
    public bool onCoolDown = false;
    

    void Awake()
    {
        gameStats = GameObject.Find("Game").GetComponent<GameStats>();
        hero = this.transform.parent.parent.gameObject.GetComponent<HeroController>();
        heroInfo = this.transform.parent.parent.gameObject.GetComponent<HeroInfo>();

        //if (skill != null)
        //{
        //    this.gameObject.name = skill.name;            
        //}      
    }

    public void CastAttack(HeroController.Slot slot)
    {
        //Check if hero is still attacking or moving again
        if (hero.isAttacking)
        {
            if (skill.type == Skill.SkillType.Projectile)
            {
                CreateProjectile(slot);
                hero.RemovePower(skill.cost);
                StartCoroutine(StartCoolDownTimer(skill.cooldown));
            }
            if (skill.type == Skill.SkillType.Area)
            {
                if (slot == HeroController.Slot.Slot_2)
                {
                    //Instantiate Explosion
                    GameObject explosion = Instantiate(skill.explosionPrefab) as GameObject;
                    explosion.transform.position = this.transform.position;

                    CreateDamageBox();
                    hero.RemovePower(skill.cost);
                    StartCoroutine(StartCoolDownTimer(skill.cooldown));

                }
            }
        }
    }

    private IEnumerator StartCoolDownTimer(float cooldown)
    {
        Debug.Log(this.gameObject.name + " - " + skill.cooldown + " sec CoolDown started");
        if (!onCoolDown)
        {
            onCoolDown = true;            
        }
        yield return new WaitForSeconds(cooldown);
        onCoolDown = false;
        Debug.Log(this.gameObject.name + " - " + skill.cooldown + " sec CoolDown ended");

    }

    void CreateProjectile(HeroController.Slot slot)
    {
        GameObject projectileObj = Instantiate(skill.projectilePrefab) as GameObject;
        projectileObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 3f, this.transform.position.z);
        Projectile projectile = projectileObj.GetComponent<Projectile>();
        //projectile.target = hero.targetObject;        
        projectile.speed = skill.speed;

        //Damage Adjustments
        if (slot == HeroController.Slot.Slot_1)
        {
            projectile.damage = skill.baseDamage * hero.PowerMultiplicator() + heroInfo.currentHealth;
            //projectile.targetVector = Input.mousePosition;
            projectile.target = hero.targetObject;

            //projectile.damage = Mathf.RoundToInt((skill.baseDamage + hero.PowerMultiplicator()) + (float)(Random.Range(0f, (float)(ResourceBank.fireLifeFull - ResourceBank.fireLife))));

        }
        else
        {
            projectile.target = hero.targetObject;
            projectile.damage = Mathf.RoundToInt(((float)skill.baseDamage + (float)gameStats.fireLife) + (Random.Range(0f, ((float)gameStats.fireLifeFull - (float)gameStats.fireLife))));
        }
    }

    

    void CreateDamageBox()
    {
        GameObject damageBox = Instantiate(skill.damageBoxPrefab) as GameObject;
        damageBox.transform.position = this.transform.position;
        DamageBox damage = damageBox.GetComponent<DamageBox>();
        damage.heroImmune = skill.heroImmune;
        damage.type = DamageBox.Type.FireAOE;
        damage.radius = skill.effectRadius;
        damage.damage = skill.baseDamage;
    }

    //public void PlayCastAnimation(string animation)
    //{
    //    hero.m_Animator.Play(animation);
    //}
}
