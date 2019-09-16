using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConsumeFireButton : MonoBehaviour, IPointerClickHandler
{
    private HeroController heroController;

    void Awake()
    {
        heroController = GameObject.Find("Hero").GetComponent<HeroController>();
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
            ResourceBank.RemoveFireLife(1);
        }
    }
}