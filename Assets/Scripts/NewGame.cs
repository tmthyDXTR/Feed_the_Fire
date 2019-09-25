using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
    //public Button btn;
    bool isStarted = false;
    void Awake()
    {
        //Button btn = this.transform.GetChild(0).GetComponent<Button>();
    }

    public void StartNewGame()
    {
        if (!isStarted)
        {
            Destroy(GameObject.Find("Level"));
            Debug.Log("NEW GAME");
            //SceneManager.LoadScene(0, LoadSceneMode.Single);
            GameObject newGame = Instantiate(Resources.Load("Level")) as GameObject;
            isStarted = true;
            Destroy(GameObject.Find("DeathScreen"));

        }
    }

    // Update is called once per frame
    
}
