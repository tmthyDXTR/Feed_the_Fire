using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Bonfire : MonoBehaviour
{
    public Button buttonBurn;
    public Button buttonPlant;
    public Button buttonCollectSpores;
    public GameObject selectedObject;
    private SelectionManager selectionManager;
    private BurnManager burnManager;
    private MinableNodes minableNodes;

    void Start()
    {
        buttonBurn = transform.Find("Button_Burn").GetComponent<Button>();
        buttonBurn.onClick.AddListener(BurnThis);

        selectionManager = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        selectedObject = selectionManager.selection[0];

        burnManager = GameObject.Find("BurnManager").GetComponent<BurnManager>();
    }

    

    void BurnThis()
    {
        Debug.Log("Burn this " + selectedObject.name);
        if (burnManager.burnList.Contains(selectedObject))
        {
            return;
        }
        else
        {
            burnManager.RegisterBurn(selectedObject);
        }
    }

    void Update()
    {
        if (selectedObject == null)
        {
            Destroy(this.gameObject);
        }
    }
}
