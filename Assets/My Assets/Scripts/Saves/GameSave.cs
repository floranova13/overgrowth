using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using GameExtensions;

[Serializable]
public class GameSave
{
    public static GameSave s;
    public static int INITIAL_PETAL_COUNT = 60;
    public static List<string> INITIAL_RESOURCES = new() { "Solblade" };
    public static int INITIAL_MERCHANT_COUNT = 3;

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
        // TODO: MOVE THIS SOMEWHERE IT CAN BE STORED EVEN WHEN NOT CREATING A NEW SAVE
        Resource.ResourceInfo = Resource.ReadFromJSON();
        Merchant.MerchantInfo = Merchant.ReadFromJSON();
        Location.ReadFromJSON();
        Contract.ContractInfo = Contract.ReadFromJSON();

        // General:
        // ------------------------------------------------------------------------------------------
        // research = new Research();

        // Possessions:
        // ------------------------------------------------------------------------------------------
        petals = INITIAL_PETAL_COUNT;
        resources = new List<Resource>();
        for (int i = 0; i < 6; i++)
        {
            resources.AddAll(Resource.GetRandomResources(3));
        }
        INITIAL_RESOURCES.ForEach(r => resources.Add(new Resource(r)));

        // Economy:
        // ------------------------------------------------------------------------------------------
        merchants = new List<Merchant>();
        for (int i = 0; i < INITIAL_MERCHANT_COUNT; i++)
        {
            Merchant newMerchant = new();
            // Debug.Log($"GameSave| Merchant Info Count: {Merchant.MerchantInfo.Count}");
            // Debug.Log($"GameSave| Creating new Merchant: {newMerchant.Name}");
            merchants.Add(newMerchant);
        }

        // X:
        // ------------------------------------------------------------------------------------------
        Debug.Log("GameSave| Game Save Created");


        Debug.Log($"GameSave| Merchants: {merchants.Count}");

        MenuController.Instance.ChangeMenus(Menu.Market);
        BlockScript.Initialize();
        GameStartScript.gameStarted = true;

    }
}