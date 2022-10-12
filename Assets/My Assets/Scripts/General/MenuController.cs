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
    };

    private void Start()
    {
        // if (GameStartScript.gameStarted)
        // {
        //     ChangeMenus(Menu.Market);
        // }
    }

    public void ChangeMenus(Menu newMenu)
    {
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

    public void ChangeMenus(string newMenu)
    {
        ChangeMenus(MenuDict[newMenu]);
    }
}