using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerUnitAI : MonoBehaviour
{
    private WorkerController util;
    private UnitInfo info;

    [SerializeField] public Job job;
    public enum Job
    {
        Unemployed,
        Woodcutter,
        LightWarden,
        Builder,
        Stonecutter,
    }

    [SerializeField] private State state;
    private enum State
    {
        MovingToSafety,
        Idling,
        SearchingTree,
        MovingToTree,
        ChoppingTree,
        SearchingStorage,
        MovingToStorage,
        StoringResources,
        TakingResources,
        MovingToConstruction,
        Constructing,
        GrowingFire,
    }


    void Awake()
    {
        util = GetComponent<WorkerController>();
        info = GetComponent<UnitInfo>();

        job = Job.Unemployed;
        state = State.MovingToSafety;
    }

    void Update()
    {
        switch (state)
        {
            #region State MovingToSafety

            case State.MovingToSafety:
                Debug.Log("Worker Unit State: MovingToSafety");
                //Search nearby FirePlace for Safety
                if (util.SearchTarget("Bonfire", LayerMask.GetMask("Buildings")) != null)
                {
                    util.MoveToTarget();
                    if (util.TargetReached() == true)
                    {
                        if (job == Job.LightWarden)
                        {
                            state = State.GrowingFire;
                        }
                        else
                        {
                            state = State.Idling;
                        }
                    }
                }
                break;
            #endregion


            #region State Idling

            case State.Idling:
                Debug.Log("Worker Unit State: Idling");
                if (job == Job.Unemployed)
                {
                    if (info.invWood != 0)
                    {
                        state = State.GrowingFire;
                    }
                    else
                    {
                        state = State.Idling;
                    }
                }
                if (job == Job.Woodcutter)
                {
                    //If Inventory empty Search for valid TreeNode
                    if (info.invWood == 0)
                    {
                        if (util.SearchTarget("WorkInactive", LayerMask.GetMask("TreeNodes")) != null)
                        {
                            state = State.MovingToTree;
                        }
                        else
                        {
                            state = State.SearchingTree;
                        }
                    }
                    //If Inventory full Search for valid Storage
                    if (info.invWood != 0)
                    {
                        if (util.SearchStorage("Store", LayerMask.GetMask("Buildings")) != null)
                        {
                            state = State.MovingToStorage;
                        }
                        else
                        {
                            state = State.GrowingFire;
                        }
                    }
                }
                if (job == Job.LightWarden)
                {
                    //If Inventory empty Search for valid Storage
                    if (info.invWood == 0)
                    {
                        if (util.SearchStorage("Collect", LayerMask.GetMask("Buildings")) != null)
                        {
                            state = State.MovingToStorage;
                        }
                        else
                        {
                            state = State.SearchingStorage;
                        }
                    }
                    //If Inventory full Search for Fire
                    if (info.invWood != 0)
                    {
                        if (ResourceBank.fireLife < ResourceBank.fireLifeMax)
                        {
                            state = State.GrowingFire;
                        }
                        else
                        {
                            state = State.Idling;
                        }
                    }
                }
        
                break;
            #endregion
                

            #region State SearchingTree

            case State.SearchingTree:
                Debug.Log("Worker Unit State: SearchingTree");
                if (job == Job.Woodcutter)
                {
                    if (info.invWood == 0)
                    {
                        if (util.SearchTarget("WorkInactive", LayerMask.GetMask("TreeNodes")) != null)
                        {
                            util.SetTargetActive();
                            state = State.MovingToTree;
                        }
                        else
                        {
                            state = State.SearchingTree;
                        }
                    }
                }
                if (job == Job.Unemployed)
                {
                    state = State.MovingToSafety;
                }
                break;
            #endregion
                

            #region State MovingToTree

            case State.MovingToTree:
                Debug.Log("Worker Unit State: MovingToTree");
                if (job == Job.Woodcutter)
                {
                    util.SetTargetActive();
                    util.MoveToTarget();
                    if (util.TargetReached() == true)
                    {
                        state = State.ChoppingTree;
                    }
                }
                if (job == Job.Unemployed)
                {
                    util.SetTargetInactive();
                    state = State.MovingToSafety;
                }
            
                break;
            #endregion


            #region State ChoppingTree

            case State.ChoppingTree:
                Debug.Log("Worker Unit State: ChoppingTree");
                if (info.invWood < info.invMax)
                {
                    util.ChopWood();
                }
                else
                {
                    util.SetTargetInactive();
                    state = State.SearchingStorage;
                }

                break;
            #endregion


            #region State SearchingStorage

            case State.SearchingStorage:
                Debug.Log("Worker Unit State: SearchingStorage");
                if (job == Job.Woodcutter)
                {
                    if (info.invWood != 0)
                    {
                        if (util.SearchStorage("Store", LayerMask.GetMask("Buildings")) != null)
                        {
                            state = State.MovingToStorage;
                        }
                        else
                        {
                            state = State.MovingToSafety;
                        }
                    }
                }
                if (job == Job.Unemployed)
                {
                    if (info.invWood != 0)
                    {
                        if (util.SearchStorage("Store", LayerMask.GetMask("Buildings")) != null)
                        {
                            state = State.MovingToStorage;
                        }
                        else
                        {
                            state = State.MovingToSafety;
                        }
                    }
                }
                if (job == Job.LightWarden)
                {
                    if (info.invWood == 0)
                    {
                        if (util.SearchStorage("Collect", LayerMask.GetMask("Buildings")) != null)
                        {
                            state = State.MovingToStorage;
                        }
                        else
                        {
                            state = State.SearchingStorage;
                        }
                    }
                }
                break;
            #endregion


            #region State MovingToStorage

            case State.MovingToStorage:
                Debug.Log("Worker Unit State: MovingToStorage");
                util.MoveToTarget();
                if (util.TargetReached() == true)
                {
                    if (job == Job.Woodcutter)
                    {
                        state = State.StoringResources;
                    }
                    if (job == Job.Unemployed)
                    {
                        state = State.StoringResources;
                    }
                    if (job == Job.LightWarden)
                    {
                        state = State.TakingResources;
                    }
                }
                break;
            #endregion


            #region State StoringResources

            case State.StoringResources:
                Debug.Log("Worker Unit State: StoringResources");
                if (job == Job.Woodcutter)
                {
                    if (info.invWood > 0)
                    {
                        if (util.CheckStorage("Store") == true)
                        {
                            util.StoreWood();
                        }
                        else
                        {
                            state = State.SearchingStorage;
                        }
                    }
                    else
                    {
                        state = State.SearchingTree;
                    }
                }
                if (job == Job.Unemployed)
                {
                    if (info.invWood > 0)
                    {
                        if (util.CheckStorage("Store") == true)
                        {
                            util.StoreWood();
                        }
                        else
                        {
                            state = State.SearchingStorage;
                        }
                    }
                    else
                    {
                        state = State.MovingToSafety;
                    }
                }
                break;
            #endregion


            #region State TakingResources

            case State.TakingResources:
                Debug.Log("Worker Unit State: TakingResources");
                if (job == Job.LightWarden)
                {
                    if (info.invWood < info.invMax)
                    {
                        if (util.CheckStorage("Collect") == true)
                        {
                            util.CollectWood();
                        }
                        else
                        {
                            state = State.SearchingStorage;
                        }
                    }
                    else
                    {
                        state = State.MovingToSafety;
                    }
                }
                break;
            #endregion


            #region State GrowingFire

            case State.GrowingFire:
                Debug.Log("Worker Unit State: GrowingFire");
                if (job == Job.Woodcutter)
                {
                    if (info.invWood > 0 && ResourceBank.fireLife < ResourceBank.fireLifeMax)
                    {                        
                        util.GrowFire();                      
                    }
                    else if (info.invWood > 0 && ResourceBank.fireLife == ResourceBank.fireLifeMax)
                    {
                        state = State.Idling;
                    }
                    else
                    {
                        state = State.SearchingTree;
                    }
                }
                if (job == Job.Unemployed)
                {
                    if (info.invWood > 0 && ResourceBank.fireLife < ResourceBank.fireLifeMax)
                    {
                        util.GrowFire();
                    }
                    else if (info.invWood > 0 && ResourceBank.fireLife == ResourceBank.fireLifeMax)
                    {
                        state = State.Idling;
                    }
                }
                if (job == Job.LightWarden)
                {
                    if (info.invWood > 0 && ResourceBank.fireLife < ResourceBank.fireLifeMax)
                    {
                        util.GrowFire();
                    }
                    else if (info.invWood > 0 && ResourceBank.fireLife == ResourceBank.fireLifeMax)
                    {
                        state = State.Idling;
                    }
                    else
                    {
                        state = State.SearchingStorage;
                    }
                }

                break;
                #endregion
        }
    }
}
