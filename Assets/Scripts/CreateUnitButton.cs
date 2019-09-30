using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class CreateUnitButton : MonoBehaviour, IPointerClickHandler
{
    //private Window_WorkerBank window_WorkerBank;
    //private JobManager jobManager;
    //private SelectionManager selection;
    //public GameObject unitPrefab;
    public UnityEvent leftClick;
    public UnityEvent middleClick;
    public UnityEvent rightClick;
    GameHandler gameHandler;
    //GameStats gameStats;

    //public GameStats GameStats { get => gameStats; set => gameStats = value; }

    void Awake()
    {
        //jobManager = GameObject.Find("Workers").GetComponent<JobManager>();
        //selection = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        //window_WorkerBank = GameObject.Find("Window_WorkerBank").GetComponent<Window_WorkerBank>();
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        //GameStats = GameObject.Find("Game").GetComponent<GameStats>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Left click");
            leftClick.Invoke();
            gameHandler.CreateWorker();

        }

        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
            Debug.Log("Middle click");
            middleClick.Invoke();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right click");
            rightClick.Invoke();
        }
    }
}