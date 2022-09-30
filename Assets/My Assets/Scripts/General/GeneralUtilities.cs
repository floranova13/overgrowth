using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GeneralUtilities
{
    public const string SCENE_MAIN_MENU = "MainMenu";
    public const string SCENE_GAME_MENU = "GameMenu";

    public static void ChangeScene(Scene sceneToLoad)
    {
        switch (sceneToLoad)
        {
            case Scene.MainMenu:
                SceneManager.LoadScene(SCENE_MAIN_MENU);
                break;
            case Scene.GameMenu:
                SceneManager.LoadScene(SCENE_GAME_MENU);
                break;
            default:
                Debug.LogError($"Cannot Load Scene: {sceneToLoad}");
                break;
        }
    }
}
