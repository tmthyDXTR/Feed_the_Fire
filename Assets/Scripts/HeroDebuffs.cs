using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDebuffs : MonoBehaviour
{
    private HeroController hero;
    private UnityEngine.AI.NavMeshAgent navAgent;

    public bool speedChanged = false;
    [SerializeField] private float originalRunSpeed;

    void Awake()
    {
        hero = GetComponent<HeroController>();
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        originalRunSpeed = hero.runSpeed;
    }

    void Update()
    {
        
    }

    public void SpeedChange(float factor, float seconds)
    {        
        float newRunSpeed = originalRunSpeed * factor;
        navAgent.speed = newRunSpeed;
        StartCoroutine(SkillDuration(seconds));
        speedChanged = true;
        Debug.Log("Speed changed by factor " + factor + " for " + seconds + " seconds");
    }

    private IEnumerator SkillDuration(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log("De/Buff Duration ended");
        navAgent.speed = originalRunSpeed;
        speedChanged = false;
    }
}
