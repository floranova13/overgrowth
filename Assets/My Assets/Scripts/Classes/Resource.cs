using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Defective.JSON;

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
public class Resource
{
    public static List<ResourceData> ResourceInfo;

    public string name;
    public Category category;
    public int price;
    public Rarity rarity;
    public string description;

    public Resource(string nameVal)
    {
        ResourceData info = GetResourceData(nameVal);

        name = nameVal;
        category = new Category(info.Category, info.Subcategory);
        price = info.Price;
        rarity = new Rarity(info.Rarity);
        description = info.Description;
    }

    public ResourceData GetResourceData(string s)
    {
        return ResourceInfo.First(resourceData => resourceData.Name == s);
    }

    // public Resource(string nameVal)
    // {
    //     List<string> info = FloraInfo.First(el => el[0] == nameVal).ToList();
    //     string[] times = info[5].Split('-');

    //     name = nameVal;
    //     isPlant = info[1].Equals("Plant");
    //     price = int.Parse(info[2]);
    //     rarity = new Rarity(info[3]);
    //     maxGrowthStage = int.Parse(info[4]);
    //     growthTime = new GrowthTime(int.Parse(times[0]), int.Parse(times[1]), int.Parse(times[2]), int.Parse(times[3]));
    //     description = info[6];
    //     growthStage = -1;
    //     modifiers = new List<string>();
    // }

    public static List<ResourceData> ReadFromJSON()
    {
        TextAsset infoJSON = Resources.Load<TextAsset>("Resource");
        JSONObject jsonObject = JSONObject.Create(infoJSON);
        Debug.Log(infoJSON.text);
        // Debug.Log(jsonObject.ToString());
        //.SelectMany(list => list).ToList();
        return new List<ResourceData>();
    }

    // Get Same:
    // ------------------------------------------------------------------------------------------
    /// <summary>
    /// Gets a specific Resource from a list of Resource or a new one if there is none.
    /// </summary>
    /// <param name="resourceList">Resource to search through</param>
    /// <param name="resource">Resource to search for</param>
    /// <returns>The Resource found</returns>
    public static Resource Same(List<Resource> resourceList, Resource resource)
    {
        if (resource == null)
        {
            Debug.Log("Searching for NULL Resource.");
            return null;
        }
        if (resourceList.FirstOrDefault(x => x.name == resource.name) == null)
        {
            return new Resource(resource.name);
        }
        return resourceList.FirstOrDefault(x => x.name == resource.name);
    }

    /// <summary>
    /// Gets a specific List of Resource from a list of Resource, replacing any not found with
    /// new ones if one is not found.
    /// </summary>
    /// <param name="resourceList">Resource to search through</param>
    /// <param name="resourceToSearchFor">Resource to search for</param>
    /// <returns>The Resources found</returns>
    public static List<Resource> Same(List<Resource> resourceList, List<Resource> resourcesToSearchFor)
    {
        List<Resource> resourcesFoundList = new();
        for (int i = 0; i < resourcesToSearchFor.Count; i++)
        {
            resourcesFoundList.Add(Same(resourceList, resourcesToSearchFor[i]));
        }
        return resourcesFoundList;
    }

    /// <summary>
    /// Gets a random Resource decided by the weighted rarities.
    /// </summary>
    /// <returns>The random Resource</returns>
    public static Resource GetRandomResource()
    {
        List<Resource> resourceList = new();
        List<int> resourceIndexes = new();

        for (int i = 0; i < ResourceInfo.Count; i++)
        {
            Resource resource = new(ResourceInfo[i].Name);
            resourceList.Add(resource);
            resourceIndexes = resourceIndexes.Concat(resource.rarity.GetWeight(i)).ToList();
        }

        return resourceList[resourceIndexes.PickRandom()];
    }
}