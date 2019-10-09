using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameHandler : MonoBehaviour
{
    JobManager jobManager;
    HeroInfo hero;
    GameStats gameStats;
    SelectionManager selection;
    private bool isGG = false;


    public EventHandler OnFireLifeChanged;
    public EventHandler OnFireLifeMaxChanged;

    public EventHandler OnWoodStockChanged;
    public EventHandler OnSporesStockChanged;
    public EventHandler OnFoodStockChanged;
    public EventHandler OnFoodConsumed;
    public EventHandler OnHousingChanged;
    internal bool isPaused;

    void Awake()
    {
        selection = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        jobManager = GameObject.Find("Workers").GetComponent<JobManager>();
        hero = GameObject.Find("Hero").GetComponent<HeroInfo>();
        gameStats = GameObject.Find("Game").GetComponent<GameStats>();
    }

    void Update()
    {
        // Loss Conditions 
        if (jobManager.workerTotalCount <= 0 || hero.currentHealth <= 0 || gameStats.fireLife <= 0)
        {
            if (!isGG)
            {
                isGG = true;
                GameObject deathScreen = Instantiate(Resources.Load("DeathScreen")) as GameObject;
            }
            
        }
        else
        {
            if (GameObject.Find("FirePlace") != null)
            {
                FireBurner();
                //Debug.Log("Fire burning");
            }
        }
    }

    
    #region Fire
    private void FireBurner()
    {
        gameStats.burnTime += Time.deltaTime;

        if (gameStats.fireLife >= 15)
        {
            if (gameStats.burnTime >= gameStats.burnSpeed)
            {
                RemoveFireLife(1);
                Debug.Log("1 Fire Life lost - Burn Time: " + (int)gameStats.burnTime);
                gameStats.burnTime -= gameStats.burnTime;
            }
        }
        else if (gameStats.fireLife >= 10 && gameStats.fireLife < 15)
        {
            if (gameStats.burnTime >= gameStats.burnSpeed * 1.22f)
            {
                RemoveFireLife(1);
                Debug.Log("1 Fire Life lost - Burn Time: " + (int)gameStats.burnTime);
                gameStats.burnTime -= gameStats.burnTime;
            }
        }
        else if (gameStats.fireLife >= 5 && gameStats.fireLife < 10)
        {
            if (gameStats.burnTime >= gameStats.burnSpeed * 1.4f)
            {
                RemoveFireLife(1);
                Debug.Log("1 Fire Life lost - Burn Time: " + (int)gameStats.burnTime);
                gameStats.burnTime -= gameStats.burnTime;
            }
        }
        else if (gameStats.fireLife > 0 && gameStats.fireLife < 5)
        {
            if (gameStats.burnTime >= gameStats.burnSpeed * 1.6f)
            {
                RemoveFireLife(1);
                Debug.Log("1 Fire Life lost - Burn Time: " + (int)gameStats.burnTime);
                gameStats.burnTime -= gameStats.burnTime;
            }
        }
    }

    public void RemoveFireLife(int amount)
    {
        gameStats.fireLife -= amount;
        if (OnFireLifeChanged != null) OnFireLifeChanged(null, EventArgs.Empty);        
    }

    public void AddFireLife(int amount)
    {
        if (gameStats.fireLife < gameStats.fireLifeFull)
        {
            gameStats.fireLife += amount;
            if (OnFireLifeChanged != null) OnFireLifeChanged(null, EventArgs.Empty);
        }        
    }

    public void ChangeFireLifeMax(int value)
    {
        gameStats.fireLife = value;
        if (OnFireLifeMaxChanged != null) OnFireLifeMaxChanged(null, EventArgs.Empty);

    }
    #endregion


    #region Units
    public void CreateWorker()
    {
        if (gameStats.housingCurrent < gameStats.housingMax)
        {
            Debug.Log("Create Worker: enough houses");
            if (gameStats.foodStock >= gameStats.workerFoodCost)
            {
                Debug.Log("Create Worker: enough food");

                GameObject worker = Instantiate(Resources.Load("Worker"), selection.selection[0].transform.position, Quaternion.identity) as GameObject;
                worker.transform.SetParent(GameObject.Find("Workers").transform);
                AddResident(1);
                RemoveFoodFromStock(1);
                if (OnHousingChanged != null) OnHousingChanged(null, EventArgs.Empty);

                //jobManager.GetWorkerCounts();
            }
        }
    }


    #endregion

    #region Resources
    // Wood
    public void AddWoodToStock(int amount)
    {
        gameStats.woodStock += amount;
        if (OnWoodStockChanged != null) OnWoodStockChanged(null, EventArgs.Empty);
    }

    public void RemoveWoodFromStock(int amount)
    {
        gameStats.woodStock -= amount;
        if (OnWoodStockChanged != null) OnWoodStockChanged(null, EventArgs.Empty);
    }

    

    // Food
    public void AddFoodToStock(int amount)
    {
        gameStats.foodStock += amount;
        if (OnFoodStockChanged != null) OnFoodStockChanged(null, EventArgs.Empty);
    }

    public void RemoveFoodFromStock(int amount)
    {
        gameStats.foodStock -= amount;
        if (OnFoodStockChanged != null) OnFoodStockChanged(null, EventArgs.Empty);
        //if (OnFoodConsumed != null) OnFoodConsumed(null, EventArgs.Empty);

        for (int i = 0; i < amount; i++)
        {
            Debug.Log("Shrooms consumed");
            Storage storage = GameObject.Find("FoodStorage").GetComponent<Storage>(); ;
            storage.Collect(StoredItem.Shrooms);
        }
    }

    public void AddSporesToStock(int amount)
    {
        gameStats.sporesStock += amount;
        if (OnSporesStockChanged != null) OnSporesStockChanged(null, EventArgs.Empty);
    }

    public void RemoveSporesFromStock(int amount)
    {
        gameStats.sporesStock -= amount;
        if (OnSporesStockChanged != null) OnSporesStockChanged(null, EventArgs.Empty);
    }

    #endregion

    #region Housing
    public void AddHousing(int amount)
    {
        gameStats.housingMax += amount;
        if (OnHousingChanged != null) OnHousingChanged(null, EventArgs.Empty);
    }

    public void AddResident(int amount)
    {
        gameStats.housingCurrent += amount;
        if (OnHousingChanged != null) OnHousingChanged(null, EventArgs.Empty);
    }
    public void RemoveResident(int amount)
    {
        if (gameStats.housingCurrent > 0)
        {
            gameStats.housingCurrent -= amount;
        }

        if (OnHousingChanged != null) OnHousingChanged(null, EventArgs.Empty);
    }

    public void RemoveHousing(int amount)
    {
        gameStats.housingMax -= amount;
        if (OnHousingChanged != null) OnHousingChanged(null, EventArgs.Empty);
    }


    #endregion

    #region UI

    internal void CreateBuildingTooltip(Building building, Vector3 position)
    {
        GameObject tooltipObj = Instantiate(Resources.Load("Tooltip")) as GameObject;
        tooltipObj.name = "Tooltip";
        tooltipObj.transform.SetParent(GameObject.Find("Canvas").transform);
        
        tooltipObj.transform.position = position + new Vector3(100f, 0, 0);
        Tooltip tooltip = tooltipObj.GetComponent<Tooltip>();
        tooltip.text = building.name;

        if (building.type == Building.Type.Building)
        {
            tooltip.info = "Wood needed: " + building.woodCost.ToString();

        }
        if (building.type == Building.Type.Area)
        {
            tooltip.info = building.description;
        }
    }

    internal void CreateSkillTooltip(Skill skill, Vector3 position)
    {
        GameObject tooltipObj = Instantiate(Resources.Load("TooltipSkill")) as GameObject;
        tooltipObj.name = "Tooltip";
        tooltipObj.transform.SetParent(GameObject.Find("Canvas").transform);

        tooltipObj.transform.position = position + new Vector3(0, 200f, 0);
        Tooltip tooltip = tooltipObj.GetComponent<Tooltip>();
        tooltip.text = skill.name;        
        tooltip.info = "Power req: " + skill.cost.ToString() + "\n" +
                       "Type: " + skill.type.ToString() + "\n" + 
                       "Range: " + skill.range.ToString() + "\n" +
                       "BaseDamage: " + skill.baseDamage.ToString();

    }

    internal void CreateMiningTooltip(Vector3 position)
    {
        GameObject tooltipObj = Instantiate(Resources.Load("TooltipMining")) as GameObject;
        tooltipObj.name = "Tooltip";
        tooltipObj.transform.SetParent(GameObject.Find("Canvas").transform);

        tooltipObj.transform.position = position;
        MiningTooltip tooltip = tooltipObj.GetComponent<MiningTooltip>();
        tooltip.text = "Area selection";
    }
    internal void DestroyTooltip()
    {
        Destroy(GameObject.Find("Tooltip"));
    }

    #endregion

    #region MISC
    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void PauseGame()
    {
        Debug.Log("Pause Game");

        isPaused = true;
        Time.timeScale = 0.0f;
        //GameObject pauseMenu = Instantiate(Resources.Load("PauseMenu")) as GameObject;
    }

    public void ResumeGame()
    {
        Debug.Log("UnPause Game");

        isPaused = false;
        Time.timeScale = 1f;
        GameObject.Find("Main Camera").GetComponent<GameTimeManager>().gameSpeed = Time.timeScale * 1f;
    }

    internal void MaximizeRadius()
    {        
        if (selection.selection[0].tag == "MiningArea" || selection.selection[0].tag == "ShroomArea")
        {
            SphereCollider col = selection.selection[0].GetComponent<SphereCollider>();
            if (col.radius < 25)
            {
                col.radius += 1;
            }
        }
    }

    internal void MinimizeRadius()
    {
        if (selection.selection[0].tag == "MiningArea" || selection.selection[0].tag == "ShroomArea")
        {
            SphereCollider col = selection.selection[0].GetComponent<SphereCollider>();
            if (col.radius > 6)
            {
                col.radius -= 1;
            }

        }
    }
    #endregion
}
