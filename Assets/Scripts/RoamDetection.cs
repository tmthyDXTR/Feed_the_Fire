using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamDetection : MonoBehaviour
{
    EnemyRoam enemyRoam;
    void Awake()
    {
        enemyRoam = this.transform.parent.GetComponent<EnemyRoam>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 17)
        {
            enemyRoam.targetList.Add(other);
            
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 17)
        {
            enemyRoam.targetList.Remove(other);
        }
        
    }
}
