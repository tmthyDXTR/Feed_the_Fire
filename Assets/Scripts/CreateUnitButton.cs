using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CreateUnitButton : MonoBehaviour, IPointerClickHandler
{
    public Window_WorkerBank window_WorkerBank;
    public GameObject unitPrefab;
    public UnityEvent leftClick;
    public UnityEvent middleClick;
    public UnityEvent rightClick;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Left click");
            leftClick.Invoke();
            Instantiate(unitPrefab, this.transform.position, Quaternion.identity);
            window_WorkerBank.UpdateJobsCounter();
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