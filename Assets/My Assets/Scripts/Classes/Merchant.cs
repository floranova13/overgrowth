using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using GameExtensions;
using UnityEngine;
using Defective.JSON;

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
        string name, string category, string subcategory,
        string rarity, List<string> possibleInventory,
        int inventorySize, int inventoryRefreshInterval,
        int costMargins, int budget, string description)
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
    public static List<MerchantData> MerchantInfo;

    public string Name { get; }
    public Citizen Citizen { get; }
    public Category Category { get; }
    public Rarity Rarity { get; }
    public string Description { get; }

    public Stock Stock { get; }
    public int RefreshCount { get; private set; }
    public int Budget { get; }
    public int Reputation { get { return Citizen.Reputation; } }

    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // Constructors: 
    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------

    public Merchant(MerchantData merchantData)
    {
        Name = merchantData.Name;
        Citizen = new Citizen();
        Category = new Category(merchantData.Category, merchantData.Subcategory);
        Rarity = new Rarity(merchantData.Rarity);
        Stock = new Stock(
          merchantData.PossibleInventory, merchantData.InventorySize,
          merchantData.InventoryRefreshInterval, merchantData.CostMargins,
          merchantData.Budget, this
        );
        Description = merchantData.Description;
    }

    public Merchant(string name) => new Merchant(GetMerchant(name));

    public Merchant() => new Merchant(MerchantInfo.PickRandom());

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
                    // Debug.Log(new List<string>() { 
                    //     obj[0].stringValue, obj[1].stringValue,
                    //     obj[2].stringValue, obj[3].stringValue,
                    //     string.Join(',', obj[4].list.Select(resource => resource.stringValue).ToList()),
                    //     obj[5].intValue.ToString(), obj[6].intValue.ToString(), 
                    //     obj[7].intValue.ToString(), obj[8].intValue.ToString(), 
                    //     obj[9].stringValue });
                    // Debug.Log($"Rarity: {obj[3]}");
                    // Debug.Log(message: $"PossibleInventory: {obj[4]}");
                    var newMerchantData = new MerchantData(
                        obj[0].stringValue, obj[1].stringValue,
                        obj[2].stringValue, obj[3].stringValue,
                        (obj[4].isNull || !obj[4].isArray)
                            ? new List<string>()
                            : obj[4].list.Select(resource => resource.stringValue).ToList(),
                        obj[5].intValue, obj[6].intValue, obj[7].intValue,
                        obj[8].intValue, obj[9].stringValue
                        );
                    merchants.Add(newMerchantData);
                }
            }
        }
        Debug.Log($"Merchant Data Count: {merchants.Count}");
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

    // Make Transaction: 
    // ------------------------------------------------------------------------------------------
    public void MakeTransaction(Resource resource, bool isBuying)
    {
        Stock.MakeTransaction(resource, isBuying);
    }

    // Change Reputation: 
    // ------------------------------------------------------------------------------------------
    public void ChangeReputation(int amount)
    {
        Citizen.ChangeReputation(amount);
    }

    // X: 
    // ------------------------------------------------------------------------------------------

    public static MerchantData GetMerchant(string name) => MerchantInfo.First(merchant => merchant.Name == name);

    public static List<MerchantData> GetMerchants(string category)
    {
        return MerchantInfo.Where(merchant =>
            (IsCategory(category) && merchant.Category == category)
            || (IsSubcategory(category) && merchant.Subcategory == category))
            .ToList();
    }

    public static List<string> GetMerchantNames(List<string> labels)
    {
        List<string> merchantNameList = new();

        for (int i = 0; i < labels.Count; i++)
        {
            if (IsCategoryOrSubcategory(labels[i]))
            {
                merchantNameList = merchantNameList
                .Concat(GetMerchants(labels[i])
                .Select(merchant => merchant.Name).ToList())
                .ToList();
            }
            else
            {
                merchantNameList.Add(labels[i]);
            }
        }

        return merchantNameList;
    }

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
    public static Merchant GetRandomMerchant(List<string> labels)
    {
        List<Merchant> merchantList = GetMerchantNames(labels).Select(merchant => new Merchant(GetMerchant(merchant))).ToList();
        List<int> merchantIndexes = new();

        for (int i = 0; i < merchantList.Count; i++)
        {
            merchantIndexes = merchantIndexes.Concat(merchantList[i].Rarity.GetWeight(i)).ToList();
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

    public static bool IsCategory(string label) => MerchantInfo.Any(merchant => merchant.Category == label);
    public static bool IsSubcategory(string label) => MerchantInfo.Any(merchant => merchant.Subcategory == label);

    public static bool IsCategoryOrSubcategory(string label) => IsCategory(label) || IsSubcategory(label);

    public string GetInventoryText()
    {
        return $"Inventory: ";
    }

    public string GetProficiencyText()
    {
        string proficiencyString = "Proficiency:";
        switch (Stock.CostMargins)
        {
            case 1:
                return $"{proficiencyString} Novice";
            case 2:
                return $"{proficiencyString} Novice";
            case 3:
                return $"{proficiencyString} Experienced";
            case 4:
                return $"{proficiencyString} Experienced";
            case 5:
                return $"{proficiencyString} Experienced";
            case 6:
                return $"{proficiencyString} Veteran";
            case 7:
                return $"{proficiencyString} Veteran";
            case 8:
                return $"{proficiencyString} Veteran";
            case 9:
                return $"{proficiencyString} Veteran";
            case 10:
                return $"{proficiencyString} Expert";
            case 11:
                return $"{proficiencyString} Expert";
            case 12:
                return $"{proficiencyString} Expert";
        }
        return proficiencyString;
    }

}
