using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_FoodStorage : MonoBehaviour
{
    public GameObject selectedObject;
    private SelectionManager selectionManager;
    [SerializeField] private Storage storage;

    void Start()
    {
        selectionManager = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        selectedObject = selectionManager.selection[0];
        storage = selectedObject.GetComponent<Storage>();
    }

    void Update()
    {
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        if (storage != null)
        {
            transform.Find("Info_1").GetComponent<Text>().text = selectedObject.name;
            transform.Find("Info_2").GetComponent<Text>().text = "Shrooms: " + storage.stockShrooms;
            transform.Find("Info_3").GetComponent<Text>().text = "Spores: " + storage.stockSpores;
            transform.Find("Info_4").GetComponent<Text>().text = "";
            transform.Find("Info_5").GetComponent<Text>().text = "";

        }

    }
}
