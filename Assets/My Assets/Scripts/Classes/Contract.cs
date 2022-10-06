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
    public List<int> ResultsCount { get; }
    public int Cost { get; }
    public string Description { get; }

    public ContractData(
        string name, string category, string subcategory, string location,
        int citizens, List<int> days, List<string> requirements,
        List<(Resource resource, int modifier)> results,
        List<int> resultsCount, int cost, string description)
    {
        Name = name;
        Category = category;
        Subcategory = subcategory;
        Location = location;
        Citizens = citizens;
        Days = days.Select(num => num).ToList();
        Requirements = requirements;
        Results = results;
        ResultsCount = resultsCount;
        Cost = cost;
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
    public List<int> ResultsCount { get; }
    public int Cost { get; private set; }
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
        ResultsCount = contractData.ResultsCount;
        Cost = contractData.Cost;
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
                obj[8].list.ToList().Select(num => num.intValue).ToList(),
                obj[9].intValue, obj[10].stringValue);
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

    // Roll Collection Count:
    // ------------------------------------------------------------------------------------------
    /// <summary>
    /// Each Contract has its own range of Items that can be collected on it.
    /// </summary>
    /// <returns>A number of resources to acquire</returns>
    public int RollResultsCount() => UnityEngine.Random.Range(ResultsCount[0], ResultsCount[1] + 1);

    // Do Item Award Rolls:
    // ------------------------------------------------------------------------------------------
    /// <summary>
    /// Randomly select the awarded Items from the completion of a Contract.
    /// </summary>
    /// <returns></returns>
    public List<Resource> RollResults()
    {
        int resultsResourceNumber = RollResultsCount();

        return Resource.GetRandomResources(resultsResourceNumber, Results);
    }
    // X:
    // ------------------------------------------------------------------------------------------

    public static bool IsCategory(string label) => ContractInfo.Any(contractData => contractData.Category == label);
    public static bool IsSubcategory(string label) => ContractInfo.Any(contractData => contractData.Subcategory == label);
    public static bool IsCategoryOrSubcategory(string label) => IsCategory(label) || IsSubcategory(label);
}
