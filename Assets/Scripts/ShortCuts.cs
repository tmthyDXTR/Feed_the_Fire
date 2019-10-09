using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortCuts : MonoBehaviour
{
    private GameHandler gameHandler;
    private SelectionManager selection;
    public GameObject cheatPanel;
    private HeroController hero;
    public Transform skills;

    [SerializeField] private KeyCode key_OpenPauseMenu = KeyCode.Escape;

    [SerializeField] private KeyCode key_Slot_1 = KeyCode.Alpha1;
    [SerializeField] private KeyCode key_Slot_2 = KeyCode.Alpha2;
    [SerializeField] private KeyCode key_Slot_3 = KeyCode.Alpha3;
    [SerializeField] private KeyCode key_Slot_4 = KeyCode.Alpha4;
    [SerializeField] private KeyCode key_OpenCheatPanel = KeyCode.P;
    [SerializeField] private KeyCode key_selectHero = KeyCode.F1;
    [SerializeField] private KeyCode key_shiftSelect = KeyCode.LeftShift;


    private GameObject obj_Slot_RClick;
    private GameObject obj_Slot_1;
    private GameObject obj_Slot_2;
    private GameObject obj_Slot_3;
    private GameObject obj_Slot_4;
    void Awake()
    {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        selection = GetComponent<SelectionManager>();
        hero = GameObject.Find("Hero").GetComponent<HeroController>();

        cheatPanel = GameObject.Find("CheatPanel");
        //cheatPanel.SetActive(false);
        skills = hero.transform.Find("Skills").transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(key_selectHero))
        {
            GameObject hero = GameObject.Find("Hero");
            if (hero != null)
            {
                if (hero.GetComponent<SelectableObject>() != null)
                {
                    if (!hero.GetComponent<SelectableObject>().isSelected)
                    {
                        selection.DeselectAll();
                        selection.Select(hero);
                    }
                }
            }
        }
        if (Input.GetKeyDown(key_OpenCheatPanel))
        {
            if (cheatPanel == null)
            {
                cheatPanel = GameObject.Find("CheatPanel");
            }
            if (cheatPanel.activeSelf == false)
            {
                cheatPanel.SetActive(true);
            }
            else
            {
                cheatPanel.SetActive(false);
            }
        }

        if (Input.GetKeyDown(key_Slot_1))
        {
            if (hero == null)
            {
                hero = GameObject.Find("Hero").GetComponent<HeroController>();
            }
            if (skills == null)
            {
                skills = hero.transform.Find("Skills").transform;
            }
            obj_Slot_1 = skills.GetChild(1).gameObject;


            hero.SelectAttackSlot(HeroController.Slot.Slot_1, obj_Slot_1);
        }
        if (Input.GetKeyDown(key_Slot_2))
        {
            if (hero == null)
            {
                hero = GameObject.Find("Hero").GetComponent<HeroController>();
            }
            if (skills == null)
            {
                skills = hero.transform.Find("Skills").transform;
            }
            obj_Slot_2 = skills.GetChild(2).gameObject;

            hero.SelectAttackSlot(HeroController.Slot.Slot_2, obj_Slot_2);
        }
        if (Input.GetKeyDown(key_Slot_3))
        {
            if (hero == null)
            {
                hero = GameObject.Find("Hero").GetComponent<HeroController>();
            }
            if (skills == null)
            {
                skills = hero.transform.Find("Skills").transform;
            }
            obj_Slot_3 = skills.GetChild(3).gameObject;

            hero.SelectAttackSlot(HeroController.Slot.Slot_3, obj_Slot_3);
        }
        if (Input.GetKeyDown(key_Slot_4))
        {
            if (hero == null)
            {
                hero = GameObject.Find("Hero").GetComponent<HeroController>();
            }
            if (skills == null)
            {
                skills = hero.transform.Find("Skills").transform;
            }
            obj_Slot_4 = skills.GetChild(4).gameObject;

            hero.SelectAttackSlot(HeroController.Slot.Slot_4, obj_Slot_4);
        }

        if (Input.GetKeyDown(key_OpenPauseMenu))
        {
            if (gameHandler == null)
            {
                gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
            }
            if (selection.selection.Count > 0)
            {
                selection.DeselectAll();
                selection.SetActive(true);
            }
            else
            {
                if (!gameHandler.isPaused)
                {
                    gameHandler.PauseGame();
                }
                else if (gameHandler.isPaused)
                {
                    gameHandler.ResumeGame();
                }
            }
        }

        if (Input.GetKey(key_shiftSelect))
        {
            CameraController camCon = GameObject.Find("Main Camera").GetComponent<CameraController>();
            if (camCon.canZoom)
            {
                camCon.canZoom = false;
            }
            Debug.Log("Key Shift Select");
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll < 0)
            {
                gameHandler.MinimizeRadius();
            }
            if (scroll > 0)
            {
                gameHandler.MaximizeRadius();
            }
        }
        if (Input.GetKeyUp(key_shiftSelect))
        {
            CameraController camCon = GameObject.Find("Main Camera").GetComponent<CameraController>();
            if (!camCon.canZoom)
            {
                camCon.canZoom = true;
            }
        }
    }
}
