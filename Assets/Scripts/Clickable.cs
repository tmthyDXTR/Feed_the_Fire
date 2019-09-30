using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour, IPointerClickHandler
{
    GameHandler gameHandler;

    void Awake()
    {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            gameHandler.AddFireLife(1);
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            gameHandler.RemoveFireLife(1);
        }
    }
}