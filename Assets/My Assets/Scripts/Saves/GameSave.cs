using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSave
{
    public static GameSave s;
    public static int INITIAL_PETALS = 60;
    public static List<string> INITIAL_RESOURCES = new List<string> { "Solblade" };

    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // Variables:
    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------

    // General:
    // ------------------------------------------------------------------------------------------
    //public Random rnd;
    // public Research research;


    // Possessions:
    // ------------------------------------------------------------------------------------------
    public int petals;
    public List<Resource> resources;

    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // Constructor:
    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------

    public GameSave()
    {
        s = this;
        Resource.ResourceInfo = Resource.ReadFromJSON();

        // General:
        // ------------------------------------------------------------------------------------------
        // rnd = new Random();
        // research = new Research();

        // Possessions:
        // ------------------------------------------------------------------------------------------
        petals = INITIAL_PETALS;
        resources = new List<Resource>();
        INITIAL_RESOURCES.ForEach(r => resources.Add(new Resource(r)));

        // X:
        // ------------------------------------------------------------------------------------------
        Debug.Log(resources[0].ToString());
        Debug.Log("Game Save Created");
        GameStartScript.gameStarted = true;
    }
}