using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameExtensions;
using System;
using System.Collections.Generic;

public class MenuController : Singleton<MenuController>
{
    public Menu menu;

    public static Dictionary<string, Menu> MenuDict = new() {
      { "Possessions", Menu.Possessions },
      { "Seeker", Menu.Seeker },
      { "Market", Menu.Market },
      { "Research", Menu.Research },
      { "None", Menu.None },
      { "", Menu.None },
    };

    private void Start()
    {
    }

    public void ChangeMenus(Menu newMenu)
    {
        StartCoroutine(ChangeMenuCoroutine(newMenu));
    }

    public void ChangeMenus(string newMenu)
    {
        Debug.Log($"MenuController - ChangeMenus(string)| Changing To Menu: {newMenu}");
        ChangeMenus(MenuDict[newMenu]);
    }

    public IEnumerator ChangeMenuCoroutine(Menu newMenu)
    {
        float menuChangeDelay = 0.25f;
        // Close Currently Open Menu
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
    }
}