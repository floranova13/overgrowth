using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Defective.JSON;
using GameExtensions;

[Serializable]
public struct ContractData
{
    public string Name { get; }
    public string Category { get; }
    public string Subcategory { get; }
    public string Location { get; }
    public int Citizens { get; }
    public List<int> Days { get; }
    public List<string> Requirements { get; }
    public List<(Resource resource, int modifier)> Results { get; private set; }
    public string Description { get; }

    public ContractData(
        string name, string category, string subcategory, string location,
        int citizens, List<int> days, List<string> requirements,
        List<(Resource resource, int modifier)> results, string description)
    {
        Name = name;
        Category = category;
        Subcategory = subcategory;
        Location = location;
        Citizens = citizens;
        Days = days.Select(num => num).ToList();
        Requirements = requirements;
        Results = results;
        Description = description;
    }
}


/// <summary>
/// A contract to explore a region of the Wilderness. Contracts cost vira and require 
/// Seekers and sometimes Guardians. Contracts result in a gain of items.
/// </summary>
[Serializable]
public class Contract
{

    // Static: 
    // ------------------------------------------------------------------------------------------
    /// <summary>
    /// Each string in this list contains the information for a specific Contract. 
    /// [Index 0]: Name, 
    /// [Index 1]: Location, 
    /// [Index 2]: Citizens, 
    /// [Index 3]: Days List (one item means constant, two means range), 
    /// [Index 4]: Requirements List,
    /// [Index 5]: Description
    /// </summary>

    public static List<ContractData> ContractInfo;

    // Contract Information: 
    // ------------------------------------------------------------------------------------------
    /// <summary>
    /// Name of Contract
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// Category of the Contract
    /// </summary>
    public Category Category { get; private set; }
    /// <summary>
    /// The Location the Contract is in.
    /// </summary>
    public Location Location { get; private set; }
    /// <summary>
    /// Seeker Explorer count required for Contract.
    /// </summary>
    public int SeekersRequired { get; private set; }
    /// <summary>
    /// List of Items needed to fund Contract.
    /// </summary>
    /// <summary>
    /// Total days to complete Contract.
    /// </summary>
    public List<int> MaxDays { get; private set; }
    public List<string> Requirements { get; private set; }
    public List<(Resource resource, int modifier)> Results { get; private set; }
    /// <summary>
    /// Description of Contract
    /// </summary>
    public string Description { get; private set; }

    // Instance Variables: 
    // ------------------------------------------------------------------------------------------
    /// <summary>
    /// Days remaining to complete Contract.
    /// </summary>
    public int Days { get; private set; }
    /// <summary>
    /// The Seeker Citizen class references on a funded Contract.
    /// </summary>
    public List<SeekerSlot> Seekers { get; private set; }

    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // Constructors: 
    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------

    public Contract(
        string contractName)
    {
        ContractData contractData = GetContractData(contractName);
        Name = contractData.Name;
        Category = new Category(contractData.Category, contractData.Subcategory);
        Location = Location.GetLocation(contractData.Location);
        SeekersRequired = contractData.Citizens;
        MaxDays = contractData.Days.Select(num => num).ToList();
        Requirements = contractData.Requirements.Select(requirement => requirement).ToList();
        Results = contractData.Results;
        Description = contractData.Description;
        Days = GetRandomDays();
        Seekers = new();
    }

    public ContractData GetContractData(string name)
    {
        return ContractInfo.FirstOrDefault(contractData => contractData.Name == name);
    }

    public static List<ContractData> ReadFromJSON()
    {
        TextAsset infoJSON = Resources.Load<TextAsset>("ContractInfo");
        JSONObject jsonObject = JSONObject.Create(infoJSON.text);
        List<ContractData> contracts = new();
        List<JSONObject> locationList = jsonObject.list[0].list;

        for (int i = 0; i < locationList.Count; i++)
        {
            JSONObject obj = locationList[i];
            ContractData newContractData = new(
                obj[0].stringValue, obj[1].stringValue, obj[2].stringValue,
                obj[3].stringValue, obj[4].intValue,
                obj[5].list.ToList().Select(numObj => numObj.intValue).ToList(),
                obj[6].list.ToList().Select(stringObj => stringObj.stringValue).ToList(),
                GetResourceResults(obj[7].list.Select(result => (resource: result[0].stringValue, modifier: result[1].intValue)).ToList(),
                Location.GetLocation(obj[3].stringValue)),
                obj[8].stringValue);
            contracts.Add(newContractData);
        }

        return contracts;
    }

    public int GetRandomDays()
    {
        return MaxDays.Count == 1 ? MaxDays[0] : UnityEngine.Random.Range(MaxDays[0], MaxDays[1] + 1);
    }

    public static List<(Resource resource, int modifier)> GetResourceResults(List<(string resource, int modifier)> resultList, Location location)
    {
        List<(Resource resource, int modifier)> results = new();

        for (int i = 0; i < resultList.Count; i++)
        {
            if (Resource.IsCategoryOrSubcategory(resultList[i].resource))
            {
                List<Resource> currentResourceList = Resource.GetResources(resultList[i].resource).Select(data => new Resource(data)).ToList();
                for (int j = 0; j < currentResourceList.Count; j++)
                {
                    results.Add((currentResourceList[j], resultList[i].modifier));
                }
            }
        }

        results.AddAll(location.AvailableResources);

        return results;
    }

    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // Contract Class Setup: 
    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------


    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // Categories: 
    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------

    public static List<ContractData> GetContracts(string category)
    {
        return ContractInfo.Where(contract => contract.Category == category || contract.Subcategory == category)
            .ToList();
    }

    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // Contract Rolling: 
    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------

    // Do Rarity Roll:
    // ------------------------------------------------------------------------------------------
    public Rarity DoRarityRoll()
    {
        RaritySet raritySet = GetContractRaritySet(availableItems);
        return raritySet.GetRarity();
    }
    // Get Contract Rarity Set:
    // ------------------------------------------------------------------------------------------
    /// <summary>
    /// The RaritySet of a Contract is tied to how many Items are found in its Rarity tiers.
    /// </summary>
    /// <param name="availableDict"></param>
    /// <returns>A RaritySet class</returns>
    public static RaritySet GetContractRaritySet(Dictionary<Rarity, List<ItemWeight>> availableDict)
    {
        if (availableDict[Rarity.Rare].Count > 0)
        {
            if (availableDict[Rarity.Legendary].Count > 0)
            {
                return new RaritySet(new float[] { 72f, 21f, 6f, 1f });
            }
            return new RaritySet(new float[] { 72f, 21f, 7f, 0f });
        }
        else if (availableDict[Rarity.Legendary].Count > 0)
        {
            return new RaritySet(new float[] { 72f, 25f, 0f, 3f });
        }
        return new RaritySet(new float[] { 75f, 25f, 0f, 0f });
    }
    // Roll Collection Count:
    // ------------------------------------------------------------------------------------------
    /// <summary>
    /// Each Contract has its own range of Items that can be collected on it.
    /// </summary>
    /// <returns></returns>
    public int RollCollectionCount()
    {
        System.Random rnd = GameSave.s.rnd;
        var countMod = modifiers.First(x => x[0] == "Item Collection Count");
        return rnd.Next(int.Parse(countMod[1]), int.Parse(countMod[2]) + 1);
    }
    // Do Item Award Rolls:
    // ------------------------------------------------------------------------------------------
    /// <summary>
    /// Randomly select the awarded Items from the completion of a Contract.
    /// </summary>
    /// <returns></returns>
    public List<Item> DoItemAwardRolls()
    {
        Debug.Log("Doing Item Award Rolls: " + Name);
        var awardList = new List<Item>();

        int collectionCount = RollCollectionCount();
        for (int i = 0; i < collectionCount; i++)
        {
            Rarity r = DoRarityRoll();
            Debug.Log("Item Rarity Roll: " + r.Value);
            string randomItemName = availableItems[r].DoItemRoll();
            Debug.Log("Random Item Name: " + randomItemName);
            if (randomItemName == "")
            {
                Debug.Log("No Additional Items, Skipping Collection");
                continue;
            }
            // check if the resource collection count is overwritten
            if (modifiers.Any(x => x[0] == "Item Count" && x[1] == randomItemName))
            {
                awardList.Add(new Item(randomItemName,
                    int.Parse(modifiers.First(
                        x => x[0] == "Item Count" && x[1] == randomItemName)[2])));
            }
            else
            {
                awardList.Add(new Item(randomItemName, 1));
            }
        }
        return awardList;
    }
    // X:
    // ------------------------------------------------------------------------------------------

    public static bool IsCategory(string label) => ContractInfo.Any(contractData => contractData.Category == label);
    public static bool IsSubcategory(string label) => ContractInfo.Any(contractData => contractData.Subcategory == label);
    public static bool IsCategoryOrSubcategory(string label) => IsCategory(label) || IsSubcategory(label);
}
