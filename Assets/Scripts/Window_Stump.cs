using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Stump : MonoBehaviour
{
    public Button buttonBurn;
    public Button buttonPlant;
    public GameObject selectedObject;
    private SelectionManager selectionManager;
    private BurnManager burnManager;
    private MinableNodes minableNodes;

    void Start()
    {
        buttonBurn = transform.Find("Button_Burn").GetComponent<Button>();
        buttonBurn.onClick.AddListener(BurnThis);

        buttonPlant = transform.Find("Button_Plant").GetComponent<Button>();
        buttonPlant.onClick.AddListener(PlantSpores);

        selectionManager = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        selectedObject = selectionManager.selection[0];

        burnManager = GameObject.Find("BurnManager").GetComponent<BurnManager>();
        minableNodes = GameObject.Find("MinableNodes").GetComponent<MinableNodes>();
    }

    void PlantSpores()
    {
        Debug.Log("Plant Shroom Spores on " + selectedObject.name);
        if (!minableNodes.shroomGrowList.Contains(selectedObject.GetComponent<Collider>()))
        {
            minableNodes.shroomGrowList.Add(selectedObject.GetComponent<Collider>());
        }
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
