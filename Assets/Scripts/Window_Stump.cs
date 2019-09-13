using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Stump : MonoBehaviour
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

        buttonPlant = transform.Find("Button_Plant").GetComponent<Button>();
        buttonPlant.onClick.AddListener(PlantSpores);

        // Check if stump has spores drop
        buttonCollectSpores = transform.Find("Button_CollectSpores").GetComponent<Button>();
        buttonCollectSpores.onClick.AddListener(CollectSpores);
        //if (selectedObject.GetComponent<ShroomNodes>() != null)
        //{
        //    ShroomNodes stump = selectedObject.GetComponent<ShroomNodes>();
        //    if (stump.sporesStock > 0)
        //    {
        //        buttonCollectSpores.gameObject.SetActive(false);
        //    }
        //    else
        //    {
        //        buttonCollectSpores.gameObject.SetActive(true);
        //    }
        //}

        // --
        selectionManager = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        selectedObject = selectionManager.selection[0];

        burnManager = GameObject.Find("BurnManager").GetComponent<BurnManager>();
        minableNodes = GameObject.Find("MinableNodes").GetComponent<MinableNodes>();
    }

    void CollectSpores()
    {
        if (selectedObject.GetComponent<ShroomNodes>() != null)
        {
            ShroomNodes stump = selectedObject.GetComponent<ShroomNodes>();
            if (stump.sporesAmount > 0)
            {
                if (minableNodes.sporeCollectList.Contains(selectedObject))
                {
                    return;
                }
                    else
                {
                    Debug.Log("Collect Spores from " + selectedObject.name);
                    minableNodes.sporeCollectList.Add(selectedObject);
                }
            }
        }
    }


    void PlantSpores()
    {
        Debug.Log("Plant Shroom Spores on " + selectedObject.name);
        if (!minableNodes.shroomGrowList.Contains(selectedObject.GetComponent<Collider>()))
        {
            minableNodes.shroomGrowList.Add(selectedObject.GetComponent<Collider>());
            selectedObject.tag = "ShroomGrow";
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
