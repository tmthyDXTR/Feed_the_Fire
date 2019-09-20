using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private HeroController heroController;
    private HeroInfo heroInfo;
    private HeroDebuffs debuffs;

    void Awake()
    {
        heroController = GameObject.Find("Hero").GetComponent<HeroController>();
        heroInfo = GameObject.Find("Hero").GetComponent<HeroInfo>();
        debuffs = heroController.GetComponent<HeroDebuffs>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DamageBox")
        {
            if (!heroController.isHit)
            {
                float damage = other.transform.gameObject.GetComponent<DamageBox>().damage;
                heroInfo.currentHealth -= damage;
                Debug.Log("Hero SMASHED");
                heroController.isHit = true;
                StartCoroutine(WaitHit());
            }
        }
        if (other.tag == "FireConsume")
        {
            heroController.canConsumeFire = true;
        }
        if (other.tag == "Debuff_Slow")
        {
            if (!heroController.isStrafing)
            {
                Debuff_Slow effect = other.gameObject.GetComponent<Debuff_Slow>();
                float factor = effect.factor;
                float duration = effect.duration;
                debuffs.SpeedChange(factor, duration);
            }            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "FireConsume")
        {
            heroController.canConsumeFire = false;
        }
    }
    IEnumerator WaitHit()
    {
        yield return new WaitForSeconds(0.01f);
        heroController.isHit = false;
    }
}
