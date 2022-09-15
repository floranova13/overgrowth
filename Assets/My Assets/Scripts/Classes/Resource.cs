using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[Serializable]
public struct ResourceData
{
    public string Name { get; }
    public string Category { get; }
    public string Subcategory { get; }
    public string Rarity { get; }
    public int Price { get; }
    public string Description { get; }
}

[Serializable]
public struct ResourceCategoryData
{
    public string Name { get; }
    public string Category { get; }
    public string Subcategory { get; }
    public string Rarity { get; }
    public int Price { get; }
    public string Description { get; }
}

[Serializable]
public class Resource
{
    protected static List<ResourceStruct> ResourceInfo
    {
        get
        {
            if (resourceInfo == null)
            {
                resourceInfo = ;
                return resourceInfo;
            }
            return resourceInfo;
        }
        set
        {
            if (resourceInfo == null || resourceInfo.Count == 0)
            {
                resourceInfo = value;
            }
        }
    }

    public static List<ResourceStruct> resourceInfo;

    public string name;
    public Category category;
    public int price;
    public Rarity rarity;
    public string description;

    public Resource(string nameVal)
    {
        List<string> info = FloraInfo.First(el => el[0] == nameVal).ToList();
        string[] times = info[5].Split('-');

        name = nameVal;
        isPlant = info[1].Equals("Plant");
        price = int.Parse(info[2]);
        rarity = new Rarity(info[3]);
        maxGrowthStage = int.Parse(info[4]);
        growthTime = new GrowthTime(int.Parse(times[0]), int.Parse(times[1]), int.Parse(times[2]), int.Parse(times[3]));
        description = info[6];
        growthStage = -1;
        modifiers = new List<string>();
    }

    public static List<ResourceStruct> ReadFromJSON()
    {
        TextAsset infoJSON = Resources.Load<TextAsset>("resourceInfo.json");
        return JsonUtility.FromJson(infoJSON.text);
    }

    // Get Same:
    // ------------------------------------------------------------------------------------------
    /// <summary>
    /// Gets a specific Flora from a list of Flora or a new one if there is none.
    /// </summary>
    /// <param name="floraList">Flora to search through</param>
    /// <param name="flora">Flora to search for</param>
    /// <returns>The Item found or a new one with a count of zero</returns>
    public static Flora Same(List<Flora> floraList, Flora flora)
    {
        if (flora == null)
        {
            Debug.Log("Searching for NULL Flora.");
            return null;
        }
        if (floraList.FirstOrDefault(x => x.name == flora.name) == null)
        {
            return new Flora(flora.name);
        }
        return floraList.FirstOrDefault(x => x.name == flora.name);
    }

    /// <summary>
    /// Gets a specific List of Flora from a list of Flora, replacing any not found with
    /// new ones if one is not found.
    /// </summary>
    /// <param name="floraList">Flora to search through</param>
    /// <param name="floraToSearchFor">Flora to search for</param>
    /// <returns>The Flora found, any not found with counts of zero</returns>
    public static List<Flora> Same(List<Flora> floraList, List<Flora> floraToSearchFor)
    {
        List<Flora> floraFoundList = new();
        for (int i = 0; i < floraToSearchFor.Count; i++)
        {
            floraFoundList.Add(Same(floraList, floraToSearchFor[i]));
        }
        return floraFoundList;
    }

    /// <summary>
    /// Gets a random Flora decided by the weighted rarities.
    /// </summary>
    /// <returns>The random Flora</returns>
    public static Flora GetRandomFlora()
    {
        List<Flora> floraList = new();
        List<int> floraIndexes = new();

        for (int i = 0; i < FloraInfo.Count; i++)
        {
            Flora flora = new(FloraInfo[i][0]);
            floraList.Add(flora);
            floraIndexes = floraIndexes.Concat(flora.rarity.GetWeight(i)).ToList();
        }

        return floraList[floraIndexes.PickRandom()];
    }

    public Vector3 GetDefaultGrowthTime(bool advanceGrowthStage = false)
    {
        if (advanceGrowthStage)
        {
            if (growthStage == maxGrowthStage)
            {
                return Vector3.zero;
            }
            growthStage++;
        }

        int hours = growthTime.hours;
        int minutes = growthTime.minutes;
        int seconds = growthTime.seconds;
        int growthStageMultiplier = growthTime.multiplier;

        hours *= growthStageMultiplier * growthStage;
        minutes *= growthStageMultiplier * growthStage;
        seconds *= growthStageMultiplier * growthStage;

        return new Vector3(hours, minutes, seconds);
    }

    public string GetGrowthTimeDescription()
    {
        // TODO: CALCULATE TIME TIERS
        // TODO: ONCE RESEARCH SYSTEM IS IMPLEMENTED, SHOW ONLY IF RESEARCHED. THERE SHOULD BE 6 TIERS FOR KNOWN AND 3 TIERS FOR HALF-KNOWN.
        return "Unknown";
    }

    public string GetClassification()
    {
        return isPlant ? "Plant" : "Fungus";
    }

    public bool IsFullyGrown() { return growthStage == maxGrowthStage; }
}