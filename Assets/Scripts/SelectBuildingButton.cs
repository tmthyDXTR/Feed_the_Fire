using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;


public class SelectBuildingButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent leftClick;
    public ObjectPlacement objectPlacement;
    public Building building;
    [SerializeField] private bool hovered;

    private Image image;
    private GameHandler gameHandler;

    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = building.icon;
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        objectPlacement = GameObject.Find("Main Camera").GetComponent<ObjectPlacement>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (objectPlacement == null)
            {
                objectPlacement = GameObject.Find("Main Camera").GetComponent<ObjectPlacement>();
            }
            leftClick.Invoke();
            objectPlacement.SetItem(building.prefab);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!hovered)
        {
            hovered = true;
            Debug.Log("OnPointerEnter Building Button");
            gameHandler.CreateTooltip(building, this.transform.position);
        }
        

    }
    public void OnPointerExit(PointerEventData eventData)
    {        
        hovered = false;
        Debug.Log("OnPointerExit Building Button");
        gameHandler.DestroyTooltip();       
    }
}
