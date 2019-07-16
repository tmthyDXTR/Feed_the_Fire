using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : MonoBehaviour
{

    public int workerTotalCount = 0;
    public int unemployedCount = 0;
    public int lighWardenCount = 0;
    public int stonecutterCount = 0;
    public int woodcutterCount = 0;
    public GameObject[] workerList;
    

    void Awake()
    {
        GetWorkerCounts();     
    }

    void Update()
    {
        
    }

    public void MoveWorkerToJob(string currentJob, string newJob)
    {
        
        workerList = GetWorkerList(currentJob);
        Debug.Log("woker list length current: " + workerList.Length);
        
        if (workerList.Length >= 1)
        {
            WorkerAI workerAI = workerList[workerList.Length - 1].GetComponent<WorkerAI>();
            workerAI.currentJob = newJob;
        }
        GetWorkerCounts();
    }


    public GameObject[] GetWorkerList(string job)
    {
        int listLength = GetWorkerCount(job);
        int counter = 0;

        GameObject[] workerList = new GameObject[listLength];       

        foreach (Transform worker in transform)
        {
            WorkerAI workerAI = worker.GetComponent<WorkerAI>();
            if (workerAI.currentJob == job)
            {
                workerList[counter] = worker.gameObject;
                counter += 1;
            }

        }
        return workerList;
    }

    public int GetWorkerCount(string jobTag)
    {
        int counter = 0;     

        foreach (Transform worker in transform)
        {
            WorkerAI workerAI = worker.GetComponent<WorkerAI>();
            if (workerAI.currentJob == jobTag)
            {
                counter += 1;
            }            
        }
        Debug.Log(jobTag + " Count: " + counter);
        return counter;
    }


    public void GetWorkerCounts()
    {
        workerTotalCount = 0;
        unemployedCount = 0;
        lighWardenCount = 0;
        woodcutterCount = 0;
        stonecutterCount = 0;

        foreach (Transform worker in transform)
        {
            WorkerAI workerAI = worker.GetComponent<WorkerAI>();
            if (workerAI.currentJob == "Unemployed")
            {
                unemployedCount += 1;
            }

            if (workerAI.currentJob == "LightWarden")
            {
                lighWardenCount += 1;
            }

            if (workerAI.currentJob == "Woodcutter")
            {
                woodcutterCount += 1;
            }

            if (workerAI.currentJob == "Stonecutter")
            {
                stonecutterCount += 1;
            }

            workerTotalCount += 1;
        }
        Debug.Log("Worker Total Count: " + workerTotalCount);
        Debug.Log("Unemployed Worker Total Count: " + unemployedCount);
        Debug.Log("Worker Total Count: " + lighWardenCount);
        Debug.Log("Woodcutter Worker Total Count: " + woodcutterCount);
        Debug.Log("Stonecutter Worker Total Count: " + stonecutterCount);
    }
        
            

    
}
