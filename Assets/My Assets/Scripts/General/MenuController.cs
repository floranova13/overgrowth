using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameExtensions;
using System;
using System.Collections.Generic;

public class MenuController : Singleton<MenuController>
{
    public static Dictionary<string, Menu> MenuDict = new() {
      { "Possessions", Menu.Possessions },
      { "Seeker", Menu.Seeker },
      { "Market", Menu.Market },
      { "Research", Menu.Research },
    };

    public void ChangeMenus(Menu menu)
    {
        switch (menu)
        {
            case Menu.Possessions:
                break;
            case Menu.Seeker:
                break;
            case Menu.Market:
                break;
            case Menu.Research:
                break;
            default:
                break;
        }
    }

    public void ChangeMenus(string menu)
    {
        ChangeMenus(MenuDict[menu]);
    }
}