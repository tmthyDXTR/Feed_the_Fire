using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowShroom : MonoBehaviour
{
    private float growTime = 0.0f;
    public float growSpeed = 45f;

    public bool hasSpores = false;
    public bool hasShrooms = false;
    public bool hasSporeDrop = false;

    public bool isTargeted = false;

    void Awake()
    {
        
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
            Destroy(this.transform.GetChild(1).gameObject);

            GameObject shrooms = Instantiate(Resources.Load("Shrooms")) as GameObject;
            shrooms.transform.position = new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z);
            shrooms.transform.SetParent(this.transform);
            hasShrooms = true;
            GetComponent<ShroomNodes>().currentAmount = GetComponent<ShroomNodes>().shroomAmount;
            growTime -= (int)growTime;
            
        }
    }

}
