using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSave
{
    public static GameSave s;
    public static int INITIAL_PETAL_COUNT = 60;
    public static List<string> INITIAL_RESOURCES = new List<string> { "Solblade" };
    public static int INITIAL_MERCHANT_COUNT = 1;

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

    // Economy:
    // ------------------------------------------------------------------------------------------
    public List<Merchant> merchants;

    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // Constructor:
    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------

    public GameSave()
    {
        s = this;
        Resource.ResourceInfo = Resource.ReadFromJSON();
        Merchant.MerchantInfo = Merchant.ReadFromJSON();

        // General:
        // ------------------------------------------------------------------------------------------
        // rnd = new Random();
        // research = new Research();

        // Possessions:
        // ------------------------------------------------------------------------------------------
        petals = INITIAL_PETAL_COUNT;
        resources = new List<Resource>();
        INITIAL_RESOURCES.ForEach(r => resources.Add(new Resource(r)));

        // Economy:
        // ------------------------------------------------------------------------------------------
        merchants = new List<Merchant>();
        for (int i = 0; i < INITIAL_MERCHANT_COUNT; i++)
        {
            merchants.Add(new Merchant());
        }

        // X:
        // ------------------------------------------------------------------------------------------
        Debug.Log(resources[0].ToString());
        Debug.Log("Game Save Created");
        GameStartScript.gameStarted = true;
    }
}