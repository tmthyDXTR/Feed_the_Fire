using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ExitGameButton : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent leftClick;
    public UnityEvent middleClick;
    public UnityEvent rightClick;
    GameHandler gameHandler;

    void Start()
    {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (gameHandler == null)
            {
                gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
            }
            Debug.Log("Left click");
            leftClick.Invoke();
            gameHandler.ExitGame();
            Application.Quit();

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
