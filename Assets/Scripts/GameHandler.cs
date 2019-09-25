using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    JobManager jobManager;
    HeroInfo hero;
    private bool isGG = false;



    // Start is called before the first frame update
    void Awake()
    {
        jobManager = GameObject.Find("Workers").GetComponent<JobManager>();
        hero = GameObject.Find("Hero").GetComponent<HeroInfo>();

    }

    // Update is called once per frame
    void Update()
    {
        if (jobManager.workerTotalCount <= 0 || hero.currentHealth <= 0 || ResourceBank.fireLife <= 0)
        {
            if (!isGG)
            {
                isGG = true;
                GameObject deathScreen = Instantiate(Resources.Load("DeathScreen")) as GameObject;
            }
            
        }
    }
}
