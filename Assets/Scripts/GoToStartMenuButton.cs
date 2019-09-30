using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToStartMenuButton : MonoBehaviour
{
    //public Button btn;
    bool isStarted = false;
    void Awake()
    {
        //Button btn = this.transform.GetChild(0).GetComponent<Button>();
    }

    public void GoToStartMenu()
    {
        if (!isStarted)
        {
            gameHandler.ResetGame();
            Destroy(GameObject.Find("Level"));
            Debug.Log("Go to Start Menu");
            //SceneManager.LoadScene(0, LoadSceneMode.Single);
            GameObject newGame = Instantiate(Resources.Load("StartMenu")) as GameObject;
            isStarted = true;
            Destroy(this.transform.parent.gameObject);

        }
    }

    // Update is called once per frame

}
