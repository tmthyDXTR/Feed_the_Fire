using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ActionBarAttack : MonoBehaviour
{
    public Button[] actionButtons;
    private HeroController hero;

    private string slot_RClick = "Slot_RClick";
    private string slot_1 = "Slot_1";
    private string slot_2 = "Slot_2";
    private string slot_3 = "Slot_3";
    private string slot_4 = "Slot_4";
    private string slot_Space = "Slot_Space";

    private Button button_Slot_RClick;
    private Button button_Slot_1;
    private Button button_Slot_2;
    private Button button_Slot_3;
    private Button button_Slot_4;

    [SerializeField] private KeyCode key_Slot_1 = KeyCode.Alpha1;
    [SerializeField] private KeyCode key_Slot_2 = KeyCode.Alpha2;
    [SerializeField] private KeyCode key_Slot_3 = KeyCode.Alpha3;
    [SerializeField] private KeyCode key_Slot_4 = KeyCode.Alpha4;

    [SerializeField] private GameObject obj_Slot_RClick;
    [SerializeField] private GameObject obj_Slot_1;
    [SerializeField] private GameObject obj_Slot_2;
    [SerializeField] private GameObject obj_Slot_3;
    [SerializeField] private GameObject obj_Slot_4;
    void Awake()
    {
        hero = GameObject.Find("Hero").GetComponent<HeroController>();
        actionButtons = GetComponentsInChildren<Button>();
        Transform skills = GameObject.Find("Hero").transform.Find("Skills").transform;
        foreach (Button button in actionButtons)
        {
            string slotName = button.gameObject.name;
            
            if (slotName == slot_RClick)
            {
                button_Slot_RClick = button;
                obj_Slot_RClick = skills.GetChild(0).gameObject;
                button.onClick.AddListener(delegate { hero.SelectAttackSlot(HeroController.Slot.Slot_RClick, obj_Slot_RClick); });
            }
            if (slotName == slot_1)
            {
                button_Slot_1 = button;
                obj_Slot_1 = skills.GetChild(1).gameObject;
                button.onClick.AddListener(delegate { hero.SelectAttackSlot(HeroController.Slot.Slot_1, obj_Slot_1); });
            }
            if (slotName == slot_2)
            {
                button_Slot_2 = button;
                obj_Slot_2 = skills.GetChild(2).gameObject;
                button.onClick.AddListener(delegate { hero.SelectAttackSlot(HeroController.Slot.Slot_2, obj_Slot_2); });
            }
            if (slotName == slot_3)
            {
                button_Slot_3 = button;
                obj_Slot_3 = skills.GetChild(3).gameObject;
                button.onClick.AddListener(delegate { hero.SelectAttackSlot(HeroController.Slot.Slot_3, obj_Slot_3); });
            }
            if (slotName == slot_4)
            {
                button_Slot_4 = button;
                obj_Slot_4 = skills.GetChild(4).gameObject;
                button.onClick.AddListener(delegate { hero.SelectAttackSlot(HeroController.Slot.Slot_4, obj_Slot_4); });
            }                        
        }        
    }

    private void Update()
    {
        if (Input.GetKeyDown(key_Slot_1))
        {
            hero.SelectAttackSlot(HeroController.Slot.Slot_1, obj_Slot_1);
        }
        if (Input.GetKeyDown(key_Slot_2))
        {
            hero.SelectAttackSlot(HeroController.Slot.Slot_2, obj_Slot_2);
        }
        if (Input.GetKeyDown(key_Slot_3))
        {
            hero.SelectAttackSlot(HeroController.Slot.Slot_3, obj_Slot_3);
        }
        if (Input.GetKeyDown(key_Slot_4))
        {
            hero.SelectAttackSlot(HeroController.Slot.Slot_4, obj_Slot_4);
        }

    }
}
