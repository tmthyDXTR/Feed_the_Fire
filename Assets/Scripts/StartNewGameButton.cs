using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartNewGameButton : MonoBehaviour
{
    //public Button btn;
    bool isStarted = false;
    private GameObject level;
    void Start()
    {
        //Button btn = this.transform.GetChild(0).GetComponent<Button>();
        GameObject level = GameObject.Find("Level");
        if (level != null)
        {
            level.SetActive(false);
            Destroy(level, 0.5f);
        }
    }

    public void StartNewGame()
    {
        if (!isStarted)
        {            
            Debug.Log("NEW GAME");
            //SceneManager.LoadScene(0, LoadSceneMode.Single);
            GameObject newGame = Instantiate(Resources.Load("Level")) as GameObject;
            newGame.name = "Level";
            gameHandler.GameStart();
            isStarted = true;
            Destroy(this.transform.parent.parent.gameObject);

        }
    }



}
