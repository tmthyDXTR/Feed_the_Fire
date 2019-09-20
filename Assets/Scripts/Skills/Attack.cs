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
    }

    void CreateProjectile(HeroController.Slot slot)
    {
        GameObject projectileObj = Instantiate(skill.projectilePrefab) as GameObject;
        projectileObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z);
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
            projectile.damage = skill.baseDamage;
        }
    }

    

    void CreateDamageBox()
    {

    }

    //public void PlayCastAnimation(string animation)
    //{
    //    hero.m_Animator.Play(animation);
    //}
}
