using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GeneralUtilities
{
    public const string SCENE_MAIN_MENU = "MainMenu";
    public const string SCENE_GAME_MENU = "GameMenu";

    public static void ChangeScene(Menu sceneToLoad)
    {
        switch (sceneToLoad)
        {
            case Menu.MainMenu:
                SceneManager.LoadScene(SCENE_MAIN_MENU);
                break;
            case Menu.GameMenu:
                SceneManager.LoadScene(SCENE_GAME_MENU);
                break;
            default:
                Debug.LogError($"Cannot Load Scene: {sceneToLoad}");
                break;
        }
    }
}
