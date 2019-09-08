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
        Shroomer,
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
        SearchingConstruction,
        MovingToConstruction,
        MovingToBurn,
        Constructing,
        GrowingFire,
        TakingFire,
        BurningObject,
        SearchingGrowArea,
        MovingToStump,
        PlantingShroomSpores,
        CollectingShrooms,

    }


    void Awake()
    {
        util = GetComponent<WorkerController>();
        info = GetComponent<UnitInfo>();

        //job = Job.Unemployed;
        //state = State.MovingToSafety;
    }

    void Update()
    {
        if (job == Job.Unemployed)
        {
            info.job = "Unemployed";
        }
        else if (job == Job.Woodcutter)
        {
            info.job = "Woodcutter";
        }
        else if (job == Job.LightWarden)
        {
            info.job = "LightWarden";
        }
        else if (job == Job.Builder)
        {
            info.job = "Builder";
        }
        else if (job == Job.Shroomer)
        {
            info.job = "Shroomer";
        }



        switch (state)
        {
            #region State MovingToSafety

            case State.MovingToSafety:
                //Debug.Log("Worker Unit State: MovingToSafety");
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
                //Debug.Log("Worker Unit State: Idling");
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
                else if (job == Job.Woodcutter)
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
                else if (job == Job.LightWarden)
                {
                    // Check if there is no Object to Burn
                    if (util.SearchBurn() == null)
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
                    else if (util.SearchBurn() != null)
                    {
                        
                        state = State.TakingFire;
                    }
                    
                }
                else if (job == Job.Builder)
                {                    
                    state = State.SearchingConstruction;                    
                }
                else if (job == Job.Shroomer)
                {
                    state = State.SearchingGrowArea;
                }

                break;
            #endregion
                

            #region State SearchingTree

            case State.SearchingTree:
                //Debug.Log("Worker Unit State: SearchingTree");
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
                else
                {
                    state = State.MovingToSafety;
                }
                break;
            #endregion
                

            #region State MovingToTree

            case State.MovingToTree:
                //Debug.Log("Worker Unit State: MovingToTree");
                if (job == Job.Woodcutter)
                {
                    util.SetTargetActive();
                    util.MoveToTarget();
                    if (util.TargetReached() == true)
                    {
                        state = State.ChoppingTree;
                    }
                }
                else
                {
                    util.SetTargetInactive();
                    state = State.MovingToSafety;
                }
            
                break;
            #endregion


            #region State MovingToConstruction

            case State.MovingToConstruction:
                //Debug.Log("Worker Unit State: MovingToConstruction");
                if (job == Job.Builder)
                {
                    util.target = util.constructionTarget;
                    util.MoveToTarget();
                    if (util.TargetReached() == true)
                    {
                        // Check if Construction needs more Ressources
                        if (util.CheckConstruction("Store") == true && info.invWood > 0)
                        {
                            state = State.StoringResources;
                        }
                        else
                        {
                            state = State.Constructing;
                        }
                    }
                }
                else
                {
                    state = State.MovingToSafety;
                }

                break;
            #endregion


            #region State MovingToBurn

            case State.MovingToBurn:
                //Debug.Log("Worker Unit State: MovingToConstruction");
                if (job == Job.LightWarden)
                {
                    util.target = util.burnTarget;
                    util.MoveToTarget();
                    if (util.TargetReached() == true)
                    {
                        state = State.BurningObject;
                    }
                }
                else
                {
                    state = State.MovingToSafety;
                }

                break;
            #endregion


            #region State ChoppingTree

            case State.ChoppingTree:
                //Debug.Log("Worker Unit State: ChoppingTree");
                if (job == Job.Woodcutter)
                {
                    if (info.invWood < info.invMax)
                    {
                        if (util.target != null)
                        {
                            util.ChopWood();
                        }
                        else
                        {
                            state = State.SearchingTree;
                        }
                    }
                    else
                    {
                        util.SetTargetInactive();
                        state = State.SearchingStorage;
                    }                    
                }                            
                else
                {
                    state = State.MovingToSafety;
                }
                break;
            #endregion


            #region State SearchingStorage

            case State.SearchingStorage:
                //Debug.Log("Worker Unit State: SearchingStorage");
                if (job == Job.Woodcutter)
                {
                    if (info.invWood != 0)
                    {
                        if (util.SearchStorage("Store", LayerMask.GetMask("Buildings")) != null)
                        {
                            state = State.MovingToStorage;
                        }                        
                    }
                    else
                    {
                        state = State.MovingToSafety;
                    }
                }
                else if (job == Job.Unemployed)
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
                else if (job == Job.LightWarden)
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
                else if (job == Job.Builder)
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
                else if (job == Job.Shroomer)
                {
                    if (util.SearchFoodStorage("Store", LayerMask.GetMask("Buildings")) != null)
                    {
                        state = State.MovingToStorage;
                    }
                    else
                    {
                        state = State.SearchingStorage;
                    }
                }
                break;
            #endregion


            #region State MovingToStorage

            case State.MovingToStorage:
                //Debug.Log("Worker Unit State: MovingToStorage");
                util.MoveToTarget();
                if (util.TargetReached() == true)
                {
                    if (job == Job.Woodcutter)
                    {
                        state = State.StoringResources;
                    }
                    else if (job == Job.Unemployed)
                    {
                        state = State.StoringResources;
                    }
                    else if (job == Job.LightWarden)
                    {
                        state = State.TakingResources;
                    }
                    else if (job == Job.Builder)
                    {
                        state = State.TakingResources;
                    }
                    else if (job == Job.Shroomer)
                    {
                        if (info.invShroom > 0)
                        {
                            state = State.StoringResources;
                        }
                        else
                        {
                            state = State.TakingResources;
                        }
                    }
                }
                break;
            #endregion


            #region State SearchingConstruction

            case State.SearchingConstruction:
                //Debug.Log("Worker Unit State: SearchingConstruction");
                if (job == Job.Builder)
                {
                    // Check if there is a Construction
                    if (util.SearchConstruction() != null)
                    {
                        // Check if Construction needs more ressources
                        if (util.CheckConstruction("Collect") == true)
                        {
                            state = State.SearchingStorage;
                        }
                        else
                        {
                            state = State.MovingToConstruction;
                        }
                    }   
                    else
                    {
                        state = State.SearchingConstruction;
                    }
                }
                else
                {
                    state = State.MovingToSafety;
                }
                break;
            #endregion


            #region State SearchingGrowArea

            case State.SearchingGrowArea:
                //Debug.Log("Worker Unit State: SearchingGrowArea");
                if (job == Job.Shroomer)
                {
                    // Check if there is a Shroom ready to collect
                    if (util.SearchShroomGrow("Collect") != null)
                    {
                        Debug.Log("Shroom found to Collect");
                        util.target.GetComponent<GrowShroom>().isTargeted = true;
                        util.MoveToTarget();
                        state = State.MovingToStump;
                    }

                    // Check if there is a Stump to Plant Shroom Seed on
                    else if (util.SearchShroomGrow("Plant") != null)
                    {
                        if (ResourceBank.sporesStock > 0)
                        {
                            Debug.Log("There are Spores to plant");
                            util.SearchSporesStorage("Collect", LayerMask.GetMask("Buildings"));
                            util.MoveToTarget();
                            state = State.MovingToStorage;
                        }
                        //Debug.Log("Shroom Grow found to plant Spore Seed");
                        //util.target.GetComponent<GrowShroom>().isTargeted = true;
                        //util.MoveToTarget();
                        //state = State.MovingToStump;
                        else
                        {
                            Debug.Log("No Spores to Plan");
                            state = State.SearchingGrowArea;
                        }
                    }
                    else
                    {
                        state = State.SearchingGrowArea;
                    }
                }
                else
                {
                    state = State.MovingToSafety;
                }
                break;
            #endregion


            #region State MovingToStump

            case State.MovingToStump:
                //Debug.Log("Worker Unit State: MovingToStump");
                if (job == Job.Shroomer)
                {                    
                    
                    if (util.TargetReached() == true)
                    {
                        Debug.Log("Stump reached");
                        if (util.target.GetComponent<GrowShroom>().hasShrooms == true)
                        {
                            state = State.CollectingShrooms;
                        }
                        else if (util.target.GetComponent<GrowShroom>().hasShrooms == false && util.target.GetComponent<GrowShroom>().hasSpores == false)
                        {
                            state = State.PlantingShroomSpores;
                        }                        
                    }
                }
                else
                {
                    //util.SetTargetInactive();
                    state = State.MovingToSafety;
                }

                break;
            #endregion


            #region State PlantingShroomSpores

            case State.PlantingShroomSpores:
                //Debug.Log("Worker Unit State: PlantingShroomSpores");
                if (job == Job.Shroomer)
                {                    
                    if (util.target.GetComponent<GrowShroom>().hasSpores == false)
                    {
                        util.PlantShroomSpores(util.target.gameObject);
                    }
                    else
                    {
                        util.target.GetComponent<GrowShroom>().isTargeted = false;
                        state = State.MovingToSafety;
                    }
                    
                }
                else
                {
                    state = State.MovingToSafety;
                }

                break;
            #endregion


            #region State CollectingShrooms

            case State.CollectingShrooms:
                //Debug.Log("Worker Unit State: CollectingShrooms");
                if (job == Job.Shroomer)
                {
                    if (info.invShroom < info.invMax)
                    {
                        util.CollectShrooms();
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
                break;
            #endregion


            #region State StoringResources

            case State.StoringResources:
                //Debug.Log("Worker Unit State: StoringResources");
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
                else if (job == Job.Builder)
                {
                    // Check if Storing to Construction or Storage
                    if (util.target.gameObject.tag == "Construction")
                    {
                        // Check Inventory
                        if (info.invWood > 0)
                        {
                            // Check if Construction needs more Ressources
                            if (util.CheckConstruction("Store") == true)
                            {
                            
                                util.StoreWoodForConstruction();
                            }
                            else
                            {
                                state = State.SearchingStorage;
                            }
                        }
                        // Check if Construction needs more Ressources
                        else if (util.CheckConstruction("Collect") == true)
                        {
                            state = State.SearchingStorage;
                        }    
                        else
                        {
                            state = State.Constructing;
                        }
                    }
                    else
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
                }
                else if (job == Job.Shroomer)
                {
                    if (util.target.GetComponent<Storage>() != null)
                    {
                        if (util.target.GetComponent<Storage>().isFull != true)
                        {
                            if (info.invShroom > 0)
                            {
                                util.StoreShrooms();
                            }
                            else
                            {
                                state = State.SearchingGrowArea;
                            }
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
                else
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
                //Debug.Log("Worker Unit State: TakingResources");
                if (job == Job.LightWarden)
                {
                    if (info.invWood < info.invMax)
                    {
                        // Check if Storage not empty
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
                else if (job == Job.Builder)
                {
                    // Check if Construction needs more Ressources
                    if (util.CheckConstruction("Collect") == true)
                    {
                        // Check if Inventory Full
                        if (info.invWood != info.invMax)
                        {
                            // Check if Storage not empty
                            if (util.CheckStorage("Collect") == true && info.invWood < info.invMax)
                            {
                                util.CollectWoodForConstruction();
                            }
                            else
                            {
                                state = State.SearchingStorage;
                            }
                        }
                        else
                        {
                            state = State.MovingToConstruction;
                        }
                    }
                    else
                    {
                        state = State.MovingToConstruction;
                    }
                }
                else if (job == Job.Shroomer)
                {
                    if (util.target.gameObject.GetComponent<Storage>() != null)
                    {
                        if (info.invSpores != info.invMax)
                        {
                            if (util.target.gameObject.GetComponent<Storage>().stockSpores > 0)
                            {
                                util.CollectSpores();

                            }
                            else
                            {
                                state = State.SearchingStorage;
                            }
                        }
                        else
                        {
                            util.SearchShroomGrow("Plant");
                            util.MoveToTarget();
                            state = State.MovingToStump;
                        }
                        
                    }
                }
                else
                {
                    state = State.MovingToSafety;
                }
                break;
            #endregion


            #region State Constructing

            case State.Constructing:
                //Debug.Log("Worker Unit State: Constructing");
                if (job == Job.Builder)
                {
                    if (util.constructionTarget != null && 
                        util.constructionTarget.gameObject.tag == "Construction")
                    {
                        util.ConstructBuilding();
                    }
                    else
                    {
                        state = State.MovingToSafety;
                    }
                }
                else
                {
                    state = State.MovingToSafety;
                }
                break;
            #endregion


            #region State GrowingFire

            case State.GrowingFire:
                //Debug.Log("Worker Unit State: GrowingFire");
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
                else if (job == Job.Unemployed)
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
                else if (job == Job.LightWarden)
                {
                    if (info.invWood > 0 && ResourceBank.fireLife < ResourceBank.fireLifeMax)
                    {
                        util.GrowFire();
                    }
                    else if (info.invWood > 0 && ResourceBank.fireLife == ResourceBank.fireLifeMax)
                    {
                        state = State.Idling;
                    }
                    else if (info.invWood > 0 && ResourceBank.fireLife > ResourceBank.fireLifeMax)
                    {
                        state = State.Idling;
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

                break;
            #endregion


            #region State TakingFire

            case State.TakingFire:
                //Debug.Log("Worker Unit State: TakingFire");
                if (job == Job.LightWarden)
                {
                    if (info.invWood < 1)
                    {
                        util.TakeFire();
                    }
                    else
                    {
                        state = State.MovingToBurn;
                    }
                }
                
                break;
            #endregion


            #region State BurningObject

            case State.BurningObject:
                //Debug.Log("Worker Unit State: BurningObject");

                if (job == Job.LightWarden)
                {
                    if (info.invWood > 0)
                    {
                        util.BurnObject();
                    }
                    else
                    {
                        state = State.MovingToSafety;
                    }
                }
                else
                {
                    state = State.MovingToSafety;
                }

                break;
                #endregion
                
        }
    }
}
