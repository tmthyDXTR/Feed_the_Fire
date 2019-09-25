using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxWorker : MonoBehaviour
{
    private WorkerController workerController;
    private UnitInfo info;
    //private HeroDebuffs debuffs;

    void Awake()
    {
        workerController = this.transform.parent.gameObject.GetComponent<WorkerController>();
        info = this.transform.parent.gameObject.GetComponent<UnitInfo>();
        //debuffs = workerController.GetComponent<HeroDebuffs>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DamageBox")
        {
            if (!workerController.isHit)
            {
                float damage = other.transform.gameObject.GetComponent<DamageBox>().damage;
                info.TakeDamage(damage);
                //Debug.Log("Worker exploded");
                workerController.isHit = true;
                StartCoroutine(WaitHit());
            }
        }
        if (other.tag == "FireConsume")
        {

        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "FireConsume")
        {
            
        }
    }
    IEnumerator WaitHit()
    {
        yield return new WaitForSeconds(0.01f);
        workerController.isHit = false;
    }
}
