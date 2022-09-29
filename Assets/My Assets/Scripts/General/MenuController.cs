using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameExtensions;

public class MenuController : Singleton<MenuController>
{
    public void ChangeMenus(Menu menu)
    {
        switch (menu)
        {
            case menu.Possessions:
                break;
            case menu.Seeker:
                break;
            case menu.Market:
                break;
            case menu.Research:
                break;
            default:
                break;
        }
    }
}