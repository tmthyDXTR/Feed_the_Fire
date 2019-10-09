using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Burnable : MonoBehaviour
{
    public bool isBurning = false;
    public bool isTargeted = false;
    private float burnTime = 0.0f;
    public static float burnSpeed = 20f; //-- Seconds to lose 1 Life
    [SerializeField] private GameObject burnEffectObj;
    private SelectionManager selectionManager;
    private MinableNodes minableNodes;

    public EventHandler OnBurning;

    void Awake()
    {
        isBurning = false;
        selectionManager = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        minableNodes = GameObject.Find("MinableNodes").GetComponent<MinableNodes>();
    }

    void Update()
    {
        if (isBurning == true)
        {
            if (minableNodes.minableNodesList.Contains(this.gameObject.GetComponent<Collider>()))
            {
                minableNodes.minableNodesList.Remove(this.gameObject.GetComponent<Collider>());
            }
            burnTime += Time.deltaTime;
            if (burnTime >= burnSpeed)
            {
                //Tree stuff
                if (this.gameObject.layer == 9)
                {
                    TreeNodes tree = GetComponent<TreeNodes>();
                    selectionManager.selection.Remove(this.gameObject);
                    tree.Death();

                    // The fire power ball moving to the hero from the fireplace
                    if (UnityEngine.Random.value > 0.5)
                    {
                        GameObject projectileObj = Instantiate(Resources.Load("PS_FireConsumeBall")) as GameObject;
                        projectileObj.transform.position = this.transform.position;
                        Projectile projectile = projectileObj.GetComponent<Projectile>();
                        HeroController hero = GameObject.Find("Hero").GetComponent<HeroController>();

                        projectile.target = hero.transform.Find("HitBox").GetComponent<Collider>();
                        projectile.type = Projectile.Type.FirePower;

                        //hero.AddPower(1);
                    }                      

                    Destroy(gameObject);
                    Destroy(burnEffectObj);
                }
                else
                {
                    selectionManager.selection.Remove(this.gameObject);
                    Destroy(gameObject);
                    Destroy(burnEffectObj);
                }                
            }
        }
    }

    public void AddBurnEffect()
    {
        if (this.gameObject.tag == "UnlitBonfire")
        {
            this.gameObject.tag = "Bonfire";
            this.gameObject.layer = 15;
            BonfireManager bonfire = GameObject.Find("BonfireManager").GetComponent<BonfireManager>();
            bonfire.AddBonfire(this.gameObject);
            foreach (Transform child in transform)
            {
                if (child.name == "Point Light" || child.name == "Flame" || child.name == "LightZone")
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
        if(this.gameObject.layer == 9)
        {
            GameObject burnEffect = Instantiate(Resources.Load("TreeBurnEffect")) as GameObject;
            burnEffectObj = burnEffect;
            burnEffect.transform.SetParent(GameObject.Find("Level").transform);
            burnEffect.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        }
        else
        {
            GameObject burnEffect = Instantiate(Resources.Load("BurnEffect")) as GameObject;
            burnEffectObj = burnEffect;
            burnEffect.transform.SetParent(GameObject.Find("Level").transform);

            burnEffect.transform.position = new Vector3(transform.position.x, transform.position.y + 0.78f, transform.position.z);
        }

    }
}
