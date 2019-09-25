using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] public Skill skill;
    [SerializeField] private HeroController hero;
    

    void Awake()
    {
        hero = this.transform.parent.parent.gameObject.GetComponent<HeroController>();
        //if (skill != null)
        //{
        //    this.gameObject.name = skill.name;            
        //}      
    }

    public void CastAttack(HeroController.Slot slot)
    {
        if (skill.type == Skill.SkillType.Projectile)
        {
            //Check if hero is still attacking or moving again
            if (hero.isAttacking)
            {
                CreateProjectile(slot);
                hero.RemovePower(skill.cost);
            }            
        }
        if (skill.type == Skill.SkillType.Area)
        {
            //Check if hero is still attacking or moving again
            if (hero.isAttacking)
            {
                if (slot == HeroController.Slot.Slot_2)
                {
                    //Instantiate Explosion
                    GameObject explosion = Instantiate(skill.explosionPrefab) as GameObject;
                    explosion.transform.position = this.transform.position;

                    CreateDamageBox();
                    hero.RemovePower(skill.cost);
                }
                    
            }
        }
    }

    void CreateProjectile(HeroController.Slot slot)
    {
        GameObject projectileObj = Instantiate(skill.projectilePrefab) as GameObject;
        projectileObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 3f, this.transform.position.z);
        Projectile projectile = projectileObj.GetComponent<Projectile>();
        projectile.target = hero.targetObject;        
        projectile.speed = skill.speed;

        //Damage Adjustments
        if (slot == HeroController.Slot.Slot_1)
        {
            projectile.damage = skill.baseDamage * hero.PowerMultiplicator();
        }
        else
        {
            projectile.damage = skill.baseDamage + (21 - ResourceBank.fireLife);
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
