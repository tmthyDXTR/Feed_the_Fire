using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : MonoBehaviour
{

    public int workerTotalCount = 0;
    public int unemployedCount = 0;
    public int lighWardenCount = 0;
    public int builderCount = 0;
    public int woodcutterCount = 0;
    public int stonecutterCount = 0;

    public WorkerUnitAI worker;

    void Awake()
    {

    }

    void Update()
    {
        GetWorkerCounts();
    }

    public void MoveWorkerToJob(string job, string newJob)
    {
        List<GameObject> workerList = new List<GameObject>();
        workerList = GetWorkerList(job);
        if (workerList.Count >= 1)
        {
            WorkerUnitAI worker = workerList[0].GetComponent<WorkerUnitAI>();
            if (newJob == "Unemployed")
            {
                worker.job = WorkerUnitAI.Job.Unemployed;
            }
            if (newJob == "Woodcutter")
            {
                worker.job = WorkerUnitAI.Job.Woodcutter;
            }
            if (newJob == "LightWarden")
            {
                worker.job = WorkerUnitAI.Job.LightWarden;
            }
            if (newJob == "Builder")
            {
                worker.job = WorkerUnitAI.Job.Builder;
            }
            if (newJob == "Stonecutter")
            {
                worker.job = WorkerUnitAI.Job.Stonecutter;
            }
        }
    }

    private List<GameObject> GetWorkerList(string job)
    {
        List<GameObject> workerList = new List<GameObject>();
        if (job == "Unemployed")
        {
            foreach (Transform unit in transform)
            {
                WorkerUnitAI worker = unit.GetComponent<WorkerUnitAI>();
                if (worker.job == WorkerUnitAI.Job.Unemployed)
                {
                    workerList.Add(unit.gameObject);
                }
            }
        }
        if (job == "Woodcutter")
        {
            foreach (Transform unit in transform)
            {
                WorkerUnitAI worker = unit.GetComponent<WorkerUnitAI>();
                if (worker.job == WorkerUnitAI.Job.Woodcutter)
                {
                    workerList.Add(unit.gameObject);
                }
            }
        }
        if (job == "LightWarden")
        {
            foreach (Transform unit in transform)
            {
                WorkerUnitAI worker = unit.GetComponent<WorkerUnitAI>();
                if (worker.job == WorkerUnitAI.Job.LightWarden)
                {
                    workerList.Add(unit.gameObject);
                }
            }
        }
        if (job == "Builder")
        {
            foreach (Transform unit in transform)
            {
                WorkerUnitAI worker = unit.GetComponent<WorkerUnitAI>();
                if (worker.job == WorkerUnitAI.Job.Builder)
                {
                    workerList.Add(unit.gameObject);
                }
            }
        }
        return workerList;
    }

    public void GetWorkerCounts()
    {
        workerTotalCount = 0;
        unemployedCount = 0;
        lighWardenCount = 0;
        builderCount = 0;
        woodcutterCount = 0;

        foreach (Transform unit in transform)
        {
            WorkerUnitAI worker = unit.GetComponent<WorkerUnitAI>();
            if (worker.job == WorkerUnitAI.Job.Unemployed)
            {
                unemployedCount += 1;
            }

            if (worker.job == WorkerUnitAI.Job.LightWarden)
            {
                lighWardenCount += 1;
            }

            if (worker.job == WorkerUnitAI.Job.Woodcutter)
            {
                woodcutterCount += 1;
            }

            if (worker.job == WorkerUnitAI.Job.Builder)
            {
                builderCount += 1;
            }

            workerTotalCount += 1;
        }
        Debug.Log("Worker Total Count: " + workerTotalCount);
        Debug.Log("Unemployed Count: " + unemployedCount);
        Debug.Log("LightWarden Count: " + lighWardenCount);
        Debug.Log("Woodcutter Count: " + woodcutterCount);
        Debug.Log("Builder Count: " + builderCount);
    }
}
