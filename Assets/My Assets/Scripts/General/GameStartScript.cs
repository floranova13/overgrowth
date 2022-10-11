using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartScript : MonoBehaviour // TODO: THE HYBRID GLOBAL/INSTANCED NATURE OF THIS SCRIPT IS GOING TO CAUSE PROBLEMS
{
    public static GameStartScript r;
    public Scene startingScene;

    public static bool gameStarted;

    private void Awake()
    {
        r = this;
        if (!gameStarted)
        {
            Debug.Log("Starting Game");
            SaveLoad.Load();

            DebugUtilities.CheckForMisspelledResourceNames();
        }
    }

    private void Start()
    {
        switch (startingScene)
        {
            case Scene.MainMenu:
                break;
            case Scene.GameMenu:
                break;
            default:
                break;
        }
    }

    // public void OpenNurseryMenu()
    // {
    //     SceneManager.LoadScene("Nursery");
    // }
}