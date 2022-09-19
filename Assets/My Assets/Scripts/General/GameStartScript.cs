using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartScript : MonoBehaviour // TODO: THE HYBRID GLOBAL/INSTANCED NATURE OF THIS SCRIPT IS GOING TO CAUSE PROBLEMS
{
    public static GameStartScript r;
    public Menu startingMenu;

    public static bool gameStarted;

    private void Awake()
    {
        r = this;
        if (!gameStarted)
        {
            Debug.Log("Starting Game");
            SaveLoad.Load();
            // MainController.Instance.unblocked = true;
        }
    }

    private void Start()
    {
        switch (startingMenu)
        {
            case Menu.MainMenu:
                break;
            case Menu.GameMenu:
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