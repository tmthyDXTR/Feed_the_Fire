using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudioController : MonoBehaviour
{

    AudioSource m_MyAudioSource;


    void Awake()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        m_MyAudioSource.volume = 1 - ((transform.position.y - 10) / 100);
    }
}
