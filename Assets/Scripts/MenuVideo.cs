using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MenuVideo : MonoBehaviour
{
    UnityEngine.Video.VideoPlayer player;
    void Awake()
    {
        player = GetComponent<VideoPlayer>();
        if (player.targetCamera == null)
        {
            player.targetCamera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
