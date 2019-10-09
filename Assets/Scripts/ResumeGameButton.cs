using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResumeGameButton : MonoBehaviour
{
    //public Button btn;
    bool isStarted = false;
    GameHandler gameHandler;

    void Start()
    {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
    }
    public void ResumeGame()
    {
        if (!isStarted)
        {
            Debug.Log("RESUME GAME");
            //SceneManager.LoadScene(0, LoadSceneMode.Single);
            gameHandler.ResumeGame();
            isStarted = true;
            Destroy(this.transform.parent.parent.gameObject);

        }
    }



}
