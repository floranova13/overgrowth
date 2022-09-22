using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using GameExtensions;
using UnityEngine;
using Defective.JSON;

public struct MerchantData
{
    public string Category { get; }
    public string Subcategory { get; }
    public string Rarity { get; }
    public List<string> PossibleInventory { get; }
    public int InventorySize { get; }
    public int InventoryRefreshInterval { get; }
    public int CostMargins { get; }
    public int Budget { get; }
    public string Description { get; }

    public MerchantData(
        string category, string subcategory,
        string rarity, List<string> possibleInventory,
        int inventorySize, int inventoryRefreshInterval,
        int costMargins, int budget, string description)
    {
        Category = category;
        Subcategory = subcategory;
        Rarity = rarity;
        PossibleInventory = possibleInventory;
        InventorySize = inventorySize;
        InventoryRefreshInterval = inventoryRefreshInterval;
        CostMargins = costMargins;
        Budget = budget;
        Description = description;
    }
}

/// <summary>
/// Merchants stock many resources.
/// </summary>
[Serializable]
public class Merchant
{
    public static List<MerchantData> MerchantInfo;

    public Citizen Citizen { get; }
    public Category Category { get; }
    public Rarity Rarity { get; }
    public string Description { get; }

    public Stock Stock { get; }
    public int RefreshCount { get; private set; }
    public int Budget { get; }

    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // Constructors: 
    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------

    public Merchant(MerchantData merchantData)
    {
        Citizen = new Citizen();
        Category = new Category(merchantData.Category, merchantData.Subcategory);
        Rarity = new Rarity(merchantData.Rarity);
        Stock = new Stock(
          merchantData.PossibleInventory, merchantData.InventorySize,
          merchantData.InventoryRefreshInterval, merchantData.CostMargins,
          merchantData.Budget
        );
        Description = merchantData.Description;
    }

    public Merchant() => new Merchant(MerchantInfo.PickRandom());

    public Merchant(List<string> subcategories)
    {
        new Merchant(MerchantInfo
        .Where(merchant => subcategories
        .Contains(merchant.Subcategory))
        .ToList()
        .PickRandom());
    }

    public Merchant(string subcategory)
    {
        new Merchant(MerchantInfo
        .Where(merchant =>
        subcategory == merchant.Subcategory)
        .ToList()
        .PickRandom());
    }

    // Read From JSON: 
    // ------------------------------------------------------------------------------------------
    public static List<MerchantData> ReadFromJSON()
    {
        TextAsset infoJSON = Resources.Load<TextAsset>("MerchantInfo");
        JSONObject jsonObject = JSONObject.Create(infoJSON.text);
        var merchants = new List<MerchantData>();

        for (int i = 0; i < jsonObject.list.Count; i++)
        {
            for (int j = 0; j < jsonObject.list[i].count; j++)
            {
                for (int k = 0; k < jsonObject.list[i][j].count; k++)
                {
                    var obj = jsonObject[i][j][k];
                    var newMerchantData = new MerchantData(
                        obj[0].stringValue, obj[1].stringValue, obj[2].stringValue,
                        obj[3].list.Select(resource => resource.stringValue).ToList(),
                        obj[4].intValue, obj[5].intValue, obj[6].intValue,
                        obj[7].intValue, obj[8].stringValue
                        );
                    merchants.Add(newMerchantData);
                }
            }
        }

        return merchants;
    }

    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // General: 
    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------


    // Day Passed: 
    // ------------------------------------------------------------------------------------------
    public void DayPassed()
    {
        Stock.DayPassed();
    }

    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // Stock Lists: 
    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------

    // Get Starting Stock List: 
    // ------------------------------------------------------------------------------------------
    // public static List<Item> GetStartingStockList(Rarity rarity = null)
    // {
    //     RaritySet raritySet = new RaritySet(new float[] { 66f, 30f, 4f, 0f, 0f });
    //     Rarity stockRarity = rarity == null ? raritySet.GetRarity() : rarity;
    //     if (stockRarity == Rarity.Common)
    //     {
    //         var stockLists = new List<List<Item>>()
    //         {
    //             new List<Item>()
    //             {
    //                 new Item("Red Corregate", 36), new Item("Green Corregate", 36),
    //                 new Item("Blue Corregate", 36), new Item("Falestone", 36),
    //                 new Item("Umiste", 36)
    //             },
    //             new List<Item>()
    //             {
    //                 new Item("Amoten", 72), new Item("Lothen", 72),
    //                 new Item("Tundrium", 72)
    //             },
    //             new List<Item>()
    //             {
    //                 new Item("Reeker Redcap", 72), new Item("Breybarb", 72)
    //             }
    //         };
    //         return stockLists.PickRandom();
    //     }
    //     else if (stockRarity == Rarity.Uncommon)
    //     {
    //         var stockLists = new List<List<Item>>()
    //         {
    //             new List<Item>()
    //             {
    //                 new Item("Red Corregate", 36), new Item("Green Corregate", 36),
    //                 new Item("Blue Corregate", 36), new Item("Falestone", 36),
    //                 new Item("Umiste", 36),
    //                 new Item("Lumenite Lightstone", 18), new Item("Lumenite Nightstone", 18)
    //             },
    //             new List<Item>()
    //             {
    //                 new Item("Amoten", 72), new Item("Lothen", 72),
    //                 new Item("Tundrium", 72),
    //                 new Item("Diserine", 36), new Item("Glavis", 36),
    //                 new Item("Daultem", 36), new Item("Relium", 36),
    //                 new Item("Arborun", 36)
    //             },
    //             new List<Item>()
    //             {
    //                 new Item("Reeker Redcap", 72), new Item("Breybarb", 72),
    //                 new Item("Ruckel Shroom", 36), new Item("Ilunite Glowcap", 36)
    //             }
    //         };
    //         return stockLists.PickRandom();
    //     }
    //     else if (stockRarity == Rarity.Rare)
    //     {
    //         var stockLists = new List<List<Item>>()
    //         {
    //             new List<Item>()
    //             {
    //                 new Item("Red Corregate", 36), new Item("Green Corregate", 36),
    //                 new Item("Blue Corregate", 36), new Item("Falestone", 36),
    //                 new Item("Umiste", 36),
    //                 new Item("Lumenite Lightstone", 18), new Item("Lumenite Nightstone", 18),
    //                 new Item("Nullstone", 9), new Item("Seras Shimmerstone", 9)
    //             },
    //             new List<Item>()
    //             {
    //                 new Item("Amoten", 72), new Item("Lothen", 72),
    //                 new Item("Tundrium", 72),
    //                 new Item("Diserine", 36), new Item("Glavis", 36),
    //                 new Item("Daultem", 36), new Item("Relium", 36),
    //                 new Item("Arborun", 36),
    //                 new Item("Embris", 18), new Item("Luxule", 18)
    //             },
    //             new List<Item>()
    //             {
    //                 new Item("Reeker Redcap", 72), new Item("Breybarb", 72),
    //                 new Item("Ruckel Shroom", 36), new Item("Ilunite Glowcap", 36),
    //                 new Item("Ilunite Glowcap", 18)
    //             }
    //         };
    //         return stockLists.PickRandom();
    //     }

    //     Debug.Log("No Merchant Starting Stock Found [Rarity: " + stockRarity + "]");
    //     return new List<Item>();
    // }

    // X: 
    // ------------------------------------------------------------------------------------------

    /// <summary>
    /// Gets a random Merchant decided by the weighted rarities.
    /// </summary>
    /// <returns>The random Merchant</returns>
    public static Merchant GetRandomMerchant()
    {
        List<Merchant> merchantList = new();
        List<int> merchantIndexes = new();

        for (int i = 0; i < MerchantInfo.Count; i++)
        {
            Merchant merchant = new(MerchantInfo[i].Subcategory);
            merchantList.Add(merchant);
            merchantIndexes = merchantIndexes.Concat(merchant.Rarity.GetWeight(i)).ToList();
        }

        return merchantList[merchantIndexes.PickRandom()];
    }

    /// <summary>
    /// Gets a random Resource from a list of possible resources, decided by the weighted rarities.
    /// </summary>
    /// <returns>The random Resource</returns>
    public static Merchant GetRandomMerchant(List<string> subcategories)
    {
        List<Merchant> merchantList = new();
        List<int> merchantIndexes = new();

        for (int i = 0; i < subcategories.Count; i++)
        {
            Merchant merchant = new(subcategories[i]);
            merchantList.Add(merchant);
            merchantIndexes = merchantIndexes.Concat(merchant.Rarity.GetWeight(i)).ToList();
        }

        return merchantList[merchantIndexes.PickRandom()];
    }

    /// <summary>
    /// Gets a random list of n Merchants, decided by the weighted rarities.
    /// </summary>
    /// <returns>The list of random Merchants</returns>
    public static List<Merchant> GetRandomMerchants(int num)
    {
        List<Merchant> merchantList = new();

        for (int i = 0; i < num; i++)
        {
            merchantList.Add(GetRandomMerchant());
        }

        return merchantList;
    }

    /// <summary>
    /// Gets a random list of n Merchants from a list of possible Merchants subcategories, decided by the weighted rarities.
    /// </summary>
    /// <returns>The list of random Merchants</returns>
    public static List<Merchant> GetRandomMerchants(int num, List<string> subcategories)
    {
        List<Merchant> merchantList = new();

        for (int i = 0; i < num; i++)
        {
            merchantList.Add(GetRandomMerchant(subcategories));
        }

        return merchantList;
    }

    // X: 
    // ------------------------------------------------------------------------------------------


}
