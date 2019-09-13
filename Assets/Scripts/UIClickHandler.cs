using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIClickHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private SelectionManager selectionManager;

    void Awake()
    {
        selectionManager = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        selectionManager.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selectionManager.SetActive(true);
    }
}
