using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JobManager : MonoBehaviour
{
    public EventHandler OnJobChanged;
    public int workerTotalCount = 0;
    public int unemployedCount = 0;
    public int lighWardenCount = 0;
    public int builderCount = 0;
    public int woodcutterCount = 0;
    public int stonecutterCount = 0;
    public int shroomerCount = 0;

    public WorkerUnitAI worker;
    GameHandler gameHandler;

    void Awake()
    {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();

        GetWorkerCounts();
        gameHandler.OnHousingChanged += delegate (object sender, EventArgs e)
        {
            //Debug.Log("OnHousingChanged event");
            GetWorkerCounts();
        };
    }

    void Update()
    {
        
    }

    public void MoveThisWorkerToJob(GameObject workerToChangeJob, string newJob)
    {
        WorkerUnitAI worker = workerToChangeJob.GetComponent<WorkerUnitAI>();
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
        if (newJob == "Shroomer")
        {
            worker.job = WorkerUnitAI.Job.Shroomer;
        }
        GetWorkerCounts();
        if (OnJobChanged != null) OnJobChanged(null, EventArgs.Empty);

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
            if (newJob == "Shroomer")
            {
                worker.job = WorkerUnitAI.Job.Shroomer;
            }
        }
        GetWorkerCounts();
        if (OnJobChanged != null) OnJobChanged(null, EventArgs.Empty);

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
        if (job == "Shroomer")
        {
            foreach (Transform unit in transform)
            {
                WorkerUnitAI worker = unit.GetComponent<WorkerUnitAI>();
                if (worker.job == WorkerUnitAI.Job.Shroomer)
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
        shroomerCount = 0;

        foreach (Transform unit in transform)
        {
            WorkerUnitAI worker = unit.GetComponent<WorkerUnitAI>();
            UnitInfo info = unit.GetComponent<UnitInfo>();
            if (!info.isDead)
            {
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
                if (worker.job == WorkerUnitAI.Job.Shroomer)
                {
                    shroomerCount += 1;
                }

                workerTotalCount += 1;
            }
            
        }
        if (OnJobChanged != null) OnJobChanged(null, EventArgs.Empty);
    }
}
