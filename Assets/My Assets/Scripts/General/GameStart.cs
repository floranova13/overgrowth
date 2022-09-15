using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour // TODO: THE HYBRID GLOBAL/INSTANCED NATURE OF THIS SCRIPT IS GOING TO CAUSE PROBLEMS
{
    public static GameStart r;
    public Menu startingMenu;

    public static bool gameStarted;

    private void Awake()
    {
        r = this;
        if (gameStarted)
        {
            return;
        }
        Debug.Log("Starting Game");
        SaveLoad.Load();
        MainController.Instance.unblocked = true;
    }

    private void Start()
    {
        switch (startingMenu)
        {
            case Menu.Main:
                break;
            case Menu.SelectMenu:
                break;
            case Menu.Market:
                break;
            case Menu.Nursery:
                break;
            case Menu.Research:
                break;
        }
    }

    public void OpenNurseryMenu()
    {
        SceneManager.LoadScene("Nursery");
    }

    public void OpenMarketMenu()
    {
        SceneManager.LoadScene("Market");
    }

    public void OpenResearchMenu()
    {
        SceneManager.LoadScene("Research");
    }

    public void OpenSelectMenu()
    {
        SceneManager.LoadScene("SelectMenu");
    }
}