using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxBigBoy : MonoBehaviour
{
    private EnemyInfo info;
    private BigBoyController bigBoyController;

    void Awake()
    {
        info = transform.parent.gameObject.GetComponent<EnemyInfo>();
        bigBoyController = transform.parent.gameObject.GetComponent<BigBoyController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DamageBox")
        {
            if (!bigBoyController.isHit)
            {
                float damage = other.transform.gameObject.GetComponent<DamageBox>().damage;
                info.currentHealth -= damage;
                Debug.Log("Big Boy EXPLODED");
                bigBoyController.isHit = true;
                StartCoroutine(WaitHit());
            }
        }
    }
    IEnumerator WaitHit()
    {
        yield return new WaitForSeconds(0.01f);
        bigBoyController.isHit = false;
    }
}
