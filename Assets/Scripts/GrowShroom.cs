using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowShroom : MonoBehaviour
{
    private float growTime = 0.0f;
    public float growSpeed = 45f;

    public bool hasSpores = false;
    public bool hasShrooms = false;
    public bool hasLightDebuff = false;
    public bool hasSporesDrop = false;
    public bool hasSaplingDrop = false;

    public bool isTargeted = false;

    void Awake()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LightZone")
        {
            Debug.Log("Stump entered Light Zone");
            growSpeed *= 2f;
            hasLightDebuff = true;
        }
    }
    //void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "LightZone")
    //    {
    //        Debug.Log("Stump entered Light Zone");
    //        growSpeed *= 2f;
    //        hasLightDebuff = true;
    //    }
    //}
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "LightZone")
        {
            Debug.Log("Stump exited Light Zone");
            growSpeed /= 2f;
            hasLightDebuff = false;
        }
    }

    void Update()
    {
        if (hasSpores == true)
        {
            if (hasShrooms == false)
            {
                GrowShrooms();
            }

        }
    }

    private void GrowShrooms()
    {
        growTime += Time.deltaTime;
        if (growTime >= growSpeed)
        {
            //selectionManager.selection.Remove(this.gameObject);

            //Destroy the small shroom model
            if (this.transform.GetChild(1) != null)
            {
                Destroy(this.transform.GetChild(1).gameObject);
            }

            // Chance depending on Light/Shadow Zone

            // If stump in LightZone and debuffed:
            if (hasLightDebuff == true)
            {
                if (Random.value <= 0.8)
                {
                    Debug.Log("New Shrooms to harvest");
                    GameObject shrooms = Instantiate(Resources.Load("Shrooms")) as GameObject;
                    shrooms.transform.position = new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z);
                    shrooms.transform.SetParent(this.transform);
                    hasShrooms = true;
                    this.gameObject.GetComponent<ShroomNodes>().shroomAmount += 1;
                    growTime -= (int)growTime;
                }
                else
                {
                    Debug.Log("No Shrooms grew here - it was too warm and bright");
                    GameObject spores = Instantiate(Resources.Load("Vial_Spores")) as GameObject;
                    spores.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
                    spores.transform.SetParent(this.transform);
                    hasSporesDrop = true;
                    growTime -= (int)growTime;

                }
            }
            else if (hasLightDebuff == false)
            {
                if (Random.value <= 0.25)
                {
                    Debug.Log("Many new Shrooms to harvest");
                    GameObject shrooms = Instantiate(Resources.Load("Shrooms")) as GameObject;
                    shrooms.transform.position = new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z);
                    shrooms.transform.SetParent(this.transform);
                    hasShrooms = true;
                    this.gameObject.GetComponent<ShroomNodes>().shroomAmount += 2;
                    growTime -= (int)growTime;
                }
                else
                {
                    Debug.Log("New Shrooms to harvest");
                    GameObject shrooms = Instantiate(Resources.Load("Shrooms")) as GameObject;
                    shrooms.transform.position = new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z);
                    shrooms.transform.SetParent(this.transform);
                    hasShrooms = true;
                    this.gameObject.GetComponent<ShroomNodes>().shroomAmount += 1;
                    growTime -= (int)growTime;
                }
            }
            
        }
    }

}
