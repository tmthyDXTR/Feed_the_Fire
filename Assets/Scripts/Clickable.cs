using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ResourceBank.AddWoodToFire(1);
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