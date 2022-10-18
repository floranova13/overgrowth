using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameExtensions;
using System;
using System.Collections.Generic;

public class MenuController : Singleton<MenuController>
{
    [HideInInspector] public Menu menu = Menu.None;

    public static Dictionary<string, Menu> MenuDict = new() {
      { "Possessions", Menu.Possessions },
      { "Seeker", Menu.Seeker },
      { "Market", Menu.Market },
      { "Research", Menu.Research },
      { "None", Menu.None },
      { "", Menu.None },
    };

    private void Awake()
    {
    }

    private void Start()
    {
    }

    public void ChangeMenus(Menu newMenu)
    {
        if (BlockScript.Unblocked())
        {
            BlockScript.Add("Changing Menu");
            StartCoroutine(ChangeMenuCoroutine(newMenu));
        }
    }

    public void ChangeMenus(string newMenu)
    {
        if (BlockScript.Unblocked())
        {
            Debug.Log($"MenuController - ChangeMenus(string)| Changing To Menu: {newMenu}");
            ChangeMenus(MenuDict[newMenu]);
        }
    }

    public IEnumerator ChangeMenuCoroutine(Menu newMenu)
    {
        float menuChangeDelay = 0.2f;
        // Close Currently Open Menu
        Debug.Log($"MenuController - ChangeMenuCoroutine| Current Menu: {menu}");
        switch (menu)
        {
            case Menu.Possessions:
                PossessionsController.Instance.CloseMenu();
                break;
            case Menu.Seeker:
                break;
            case Menu.Market:
                MarketController.Instance.CloseMenu();
                break;
            case Menu.Research:
                break;
            default:
                break;
        }
        menu = newMenu;
        Debug.Log($"MenuController - ChangeMenuCoroutine| New Menu: {newMenu}");
        yield return new WaitForSeconds(menuChangeDelay);
        // Open New Menu
        switch (newMenu)
        {
            case Menu.Possessions:
                PossessionsController.Instance.OpenMenu();
                break;
            case Menu.Seeker:
                break;
            case Menu.Market:
                MarketController.Instance.OpenMenu();
                break;
            case Menu.Research:
                break;
            default:
                break;
        }
        BlockScript.Remove("Changing Menu");
    }
}