using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GeneralUtilities
{
    public const string SCENE_MAIN_MENU = "Main";
    public const string SCENE_NURSERY = "Nursery";
    public const string SCENE_MARKET = "Market";

    static void ChangeScene(Menu sceneToLoad)
    {
        switch (sceneToLoad)
        {
            case Menu.Main:
                SceneManager.LoadScene(SCENE_MAIN_MENU);
                break;
            case Menu.Nursery:
                SceneManager.LoadScene(SCENE_NURSERY);
                break;
            case Menu.Market:
                SceneManager.LoadScene(SCENE_MARKET);
                break;
            default:
                Debug.LogError($"Cannot Load Scene: {sceneToLoad}");
                break;
        }
    }
}
