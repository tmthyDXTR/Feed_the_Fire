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



    [SerializeField] private GameObject obj_Slot_RClick;
    [SerializeField] private GameObject obj_Slot_1;
    [SerializeField] private GameObject obj_Slot_2;
    [SerializeField] private GameObject obj_Slot_3;
    [SerializeField] private GameObject obj_Slot_4;
    void Start()
    {
        hero = GameObject.Find("Hero").GetComponent<HeroController>();
        actionButtons = GetComponentsInChildren<Button>();
        Transform skills = hero.transform.Find("Skills").transform;
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
                Image image = button.gameObject.GetComponent<Image>();
                image.sprite = obj_Slot_1.GetComponent<Attack>().skill.icon;
                button.GetComponent<SkillButton>().attack = obj_Slot_1.GetComponent<Attack>();

            }
            if (slotName == slot_2)
            {
                button_Slot_2 = button;
                obj_Slot_2 = skills.GetChild(2).gameObject;
                button.onClick.AddListener(delegate { hero.SelectAttackSlot(HeroController.Slot.Slot_2, obj_Slot_2); });
                Image image = button.gameObject.GetComponent<Image>();
                image.sprite = obj_Slot_2.GetComponent<Attack>().skill.icon;
                button.GetComponent<SkillButton>().attack = obj_Slot_2.GetComponent<Attack>();

            }
            if (slotName == slot_3)
            {
                button_Slot_3 = button;
                obj_Slot_3 = skills.GetChild(3).gameObject;
                button.onClick.AddListener(delegate { hero.SelectAttackSlot(HeroController.Slot.Slot_3, obj_Slot_3); });
                Image image = button.gameObject.GetComponent<Image>();
                image.sprite = obj_Slot_3.GetComponent<Attack>().skill.icon;
                button.GetComponent<SkillButton>().attack = obj_Slot_3.GetComponent<Attack>();

            }
            if (slotName == slot_4)
            {
                button_Slot_4 = button;
                obj_Slot_4 = skills.GetChild(4).gameObject;
                button.onClick.AddListener(delegate { hero.SelectAttackSlot(HeroController.Slot.Slot_4, obj_Slot_4); });
                Image image = button.gameObject.GetComponent<Image>();
                image.sprite = obj_Slot_4.GetComponent<Attack>().skill.icon;
                button.GetComponent<SkillButton>().attack = obj_Slot_4.GetComponent<Attack>();

            }
        }        
    }
}
