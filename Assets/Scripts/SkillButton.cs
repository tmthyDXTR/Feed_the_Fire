using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    public Attack attack;
    public bool greyedOut = false;
    private HeroInfo hero;
    public bool hovered = false;
    private GameHandler gameHandler;

    void Start()
    {
        hero = GameObject.Find("Hero").GetComponent<HeroInfo>();
        button = GetComponent<Button>();
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!hovered)
        {
            hovered = true;
            Debug.Log("OnPointerEnter Skill Button");
            gameHandler.CreateSkillTooltip(attack.skill, this.transform.position);
        }


    }
    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
        Debug.Log("OnPointerExit Skill Button");
        gameHandler.DestroyTooltip();
    }

    internal void Deactivate()
    {
        if (!greyedOut)
        {
            greyedOut = true;
            button.interactable = false;
        }        
    }
    internal void Activate()
    {
        if (greyedOut)
        {
            greyedOut = false;
            button.interactable = true;
        }
    }
    void Update()
    {
        if (attack != null)
        {
            if (attack.onCoolDown || attack.skill.cost > hero.power)
            {
                Deactivate();
            }
            else
            {
                Activate();
            }
        }
    }
}
