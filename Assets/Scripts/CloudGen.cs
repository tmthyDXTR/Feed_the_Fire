using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGen : MonoBehaviour
{
    public GameObject cloud;
    public float width = 10f;
    public float hight = 10f;
    public float cloudSize = 5f;

    void Start()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y <hight; y++)
            {
                GameObject go = Instantiate(cloud, new Vector3(transform.position.x + x * cloudSize, 
                    transform.position.y, 
                    transform.position.z + y * cloudSize), Quaternion.identity);

                go.name = "Cloud_" + x + "_" + y;
                go.transform.SetParent(transform);
            }
        }
    }

}
