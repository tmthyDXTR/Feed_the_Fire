using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : MonoBehaviour
{

    public int workerTotalCount = 0;
    public int unemployedCount = 0;
    public int stonecutterCount = 0;
    public int woodcutterCount = 0;
    public GameObject[] workerList;
    

    // Start is called before the first frame update
    void Awake()
    {
        GetWorkerCounts();     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveWorkerToJob(string currentJobTag, string newJobTag)
    {
        workerList = GetWorkerList(currentJobTag);
        Debug.Log("woker list length current: " + workerList.Length);
        
        if (workerList.Length >= 1)
        {
            workerList[workerList.Length - 1].gameObject.tag = newJobTag;
        }
        GetWorkerCounts();
    }


    public GameObject[] GetWorkerList(string jobTag)
    {
        int listLength = GetWorkerCount(jobTag);
        int counter = 0;

        GameObject[] workerList = new GameObject[listLength];       

        foreach (Transform worker in transform)
        {

            if (worker.gameObject.tag == jobTag)
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
            if (worker.gameObject.tag == jobTag)
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
        woodcutterCount = 0;
        stonecutterCount = 0;

        foreach (Transform worker in transform)
        {
            if (worker.gameObject.tag == "Unemployed")
            {
                unemployedCount += 1;
            }

            if (worker.gameObject.tag == "Woodcutter")
            {
                woodcutterCount += 1;
            }

            if (worker.gameObject.tag == "Stonecutter")
            {
                stonecutterCount += 1;
            }

            workerTotalCount += 1;
        }
        Debug.Log("Worker Total Count: " + workerTotalCount);
        Debug.Log("Unemployed Worker Total Count: " + unemployedCount);
        Debug.Log("Woodcutter Worker Total Count: " + woodcutterCount);
        Debug.Log("Stonecutter Worker Total Count: " + stonecutterCount);
    }
        
            

    
}
