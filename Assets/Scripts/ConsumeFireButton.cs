using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConsumeFireButton : MonoBehaviour, IPointerClickHandler
{
    private HeroController heroController;
    GameHandler gameHandler;


    void Awake()
    {
        heroController = GameObject.Find("Hero").GetComponent<HeroController>();
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            heroController.ConsumeFire(1);
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            gameHandler.RemoveFireLife(1);
        }
    }
    private void OnDestroy()
    {
        //Destroy(GetComponent<EventTrigger>());

    }
}