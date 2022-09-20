using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using GameExtensions;
using UnityEngine;

public struct MerchantData
{
    public string Name { get; }
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
        string name, string category,
        string subcategory, string rarity,
        List<string> possibleInventory, int inventorySize,
        int inventoryRefreshInterval, int costMargins,
        int budget, string description)
    {
        Name = name;
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
    public Citizen Citizen { get; }
    public Category Category { get; }
    public Rarity Rarity { get; }
    public List<ResourceData> PossibleInventory { get; }
    public int RefreshInterval { get; }
    public int InventorySize { get; }
    public int InventoryRefreshInterval { get; }
    public int CostMargins { get; }
    public int MaxBudget { get; }
    public string Description { get; }

    public int RefreshCount { get; private set; }
    public int Budget { get; }
    public List<Resource> Stock { get; set; }

    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // Constructors: 
    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------

    public Merchant()
    {
        Citizen = new Citizen();
        startingStock = stockInput;
        stock = stockInput;
        RefreshStock();
        refresh = 9; // FIX? CHANGE TO RANDOM WITHIN RANGE?
    }

    // X: 
    // ------------------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // General: 
    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------


    // Day Passed: 
    // ------------------------------------------------------------------------------------------
    public void DayPassed()
    {
        refresh--;
        if (refresh <= 0)
        {
            RefreshStock();
        }
    }

    // Refresh Stock: 
    // ------------------------------------------------------------------------------------------
    public void RefreshStock()
    {
        System.Random rnd = GameSave.s.rnd;
        refresh = 9; // FIX? CHANGE TO RANDOM WITHIN RANGE?
                     // CHANGE THE STOCK REFRESH SYSTEM
        stock = new List<Item>(startingStock);
        for (int i = 0; i < 3; i++)
        {
            if (50.PercentChance())
            {
                stock.RemoveAt(rnd.Next(0, stock.Count));
            }
        }
        for (int i = 0; i < stock.Count; i++)
        {
            stock[i].count += (rnd.Next(0, 6) * (50.PercentChance() ? -1 : 1));
            if (stock[i].count == 0)
            {
                stock[i].count = 1;
            }
        }
    }

    // Price: 
    // ------------------------------------------------------------------------------------------
    public int Price(string itemName, bool purchase)
    {
        int itemPrice = Economy.Price(itemName);
        if (purchase)
        {
            // FIX! CHANGE BASED ON INDIVIDUAL MERCHANT STATS
            return itemPrice;
        }
        else
        {
            // FIX! CHANGE BASED ON INDIVIDUAL MERCHANT STATS
            return Mathf.RoundToInt(itemPrice * 50f / 100f);
        }
    }

    // Matching Stock: 
    // ------------------------------------------------------------------------------------------
    public List<Item> MatchingStock()
    {
        var matchingStock = GameSave.s.items;
        var acceptedCategories = new List<string>();
        for (int i = 0; i < startingStock.Count; i++)
        {
            if (!acceptedCategories.Contains(startingStock[i].Cat(0)))
            {
                acceptedCategories.Add(startingStock[i].Cat(0));
            }
        }
        matchingStock = matchingStock.Where(x => acceptedCategories.Contains(x.Cat(0))).ToList();
        return matchingStock;
    }

    // X: 
    // ------------------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // Stock Lists: 
    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------

    // Get Starting Stock List: 
    // ------------------------------------------------------------------------------------------
    public static List<Item> GetStartingStockList(Rarity rarity = null)
    {
        RaritySet raritySet = new RaritySet(new float[] { 66f, 30f, 4f, 0f, 0f });
        Rarity stockRarity = rarity == null ? raritySet.GetRarity() : rarity;
        if (stockRarity == Rarity.Common)
        {
            var stockLists = new List<List<Item>>()
            {
                new List<Item>()
                {
                    new Item("Red Corregate", 36), new Item("Green Corregate", 36),
                    new Item("Blue Corregate", 36), new Item("Falestone", 36),
                    new Item("Umiste", 36)
                },
                new List<Item>()
                {
                    new Item("Amoten", 72), new Item("Lothen", 72),
                    new Item("Tundrium", 72)
                },
                new List<Item>()
                {
                    new Item("Reeker Redcap", 72), new Item("Breybarb", 72)
                }
            };
            return stockLists.PickRandom();
        }
        else if (stockRarity == Rarity.Uncommon)
        {
            var stockLists = new List<List<Item>>()
            {
                new List<Item>()
                {
                    new Item("Red Corregate", 36), new Item("Green Corregate", 36),
                    new Item("Blue Corregate", 36), new Item("Falestone", 36),
                    new Item("Umiste", 36),
                    new Item("Lumenite Lightstone", 18), new Item("Lumenite Nightstone", 18)
                },
                new List<Item>()
                {
                    new Item("Amoten", 72), new Item("Lothen", 72),
                    new Item("Tundrium", 72),
                    new Item("Diserine", 36), new Item("Glavis", 36),
                    new Item("Daultem", 36), new Item("Relium", 36),
                    new Item("Arborun", 36)
                },
                new List<Item>()
                {
                    new Item("Reeker Redcap", 72), new Item("Breybarb", 72),
                    new Item("Ruckel Shroom", 36), new Item("Ilunite Glowcap", 36)
                }
            };
            return stockLists.PickRandom();
        }
        else if (stockRarity == Rarity.Rare)
        {
            var stockLists = new List<List<Item>>()
            {
                new List<Item>()
                {
                    new Item("Red Corregate", 36), new Item("Green Corregate", 36),
                    new Item("Blue Corregate", 36), new Item("Falestone", 36),
                    new Item("Umiste", 36),
                    new Item("Lumenite Lightstone", 18), new Item("Lumenite Nightstone", 18),
                    new Item("Nullstone", 9), new Item("Seras Shimmerstone", 9)
                },
                new List<Item>()
                {
                    new Item("Amoten", 72), new Item("Lothen", 72),
                    new Item("Tundrium", 72),
                    new Item("Diserine", 36), new Item("Glavis", 36),
                    new Item("Daultem", 36), new Item("Relium", 36),
                    new Item("Arborun", 36),
                    new Item("Embris", 18), new Item("Luxule", 18)
                },
                new List<Item>()
                {
                    new Item("Reeker Redcap", 72), new Item("Breybarb", 72),
                    new Item("Ruckel Shroom", 36), new Item("Ilunite Glowcap", 36),
                    new Item("Ilunite Glowcap", 18)
                }
            };
            return stockLists.PickRandom();
        }

        Debug.Log("No Merchant Starting Stock Found [Rarity: " + stockRarity + "]");
        return new List<Item>();
    }

    // X: 
    // ------------------------------------------------------------------------------------------

    // X: 
    // ------------------------------------------------------------------------------------------


}
