using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Defective.JSON;
using GameExtensions;

[Serializable]
public struct ResourceData
{
    public string Name { get; }
    public string Category { get; }
    public string Subcategory { get; }
    public string Rarity { get; }
    public int Price { get; }
    public string Description { get; }

    public ResourceData(
        string name, string category,
        string subcategory, string rarity,
        int price, string description)
    {
        Name = name;
        Category = category;
        Subcategory = subcategory;
        Rarity = rarity;
        Price = price;
        Description = description;
    }
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

    public ResourceData GetResourceData(string name)
    {
        return ResourceInfo.First(resourceData => resourceData.Name == name);
    }

    public static List<ResourceData> ReadFromJSON()
    {
        TextAsset infoJSON = Resources.Load<TextAsset>("ResourceInfo");
        JSONObject jsonObject = JSONObject.Create(infoJSON.text);
        var resources = new List<ResourceData>();

        for (int i = 0; i < jsonObject.list.Count; i++)
        {
            for (int j = 0; j < jsonObject.list[i].count; j++)
            {
                for (int k = 0; k < jsonObject.list[i][j].count; k++)
                {
                    var obj = jsonObject[i][j][k];
                    var newResourceData = new ResourceData(
                        obj[0].stringValue, obj[1].stringValue, obj[2].stringValue,
                        obj[3].stringValue, obj[4].intValue, obj[5].stringValue
                        );
                    resources.Add(newResourceData);
                }
            }
        }

        return resources;
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
            Debug.LogError("Searching for NULL Resource.");
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

    /// <summary>
    /// Gets a random Resource from a list of possible resources, decided by the weighted rarities.
    /// </summary>
    /// <returns>The random Resource</returns>
    public static Resource GetRandomResource(List<string> names)
    {
        List<Resource> resourceList = new();
        List<int> resourceIndexes = new();

        for (int i = 0; i < names.Count; i++)
        {
            Resource resource = new(names[i]);
            resourceList.Add(resource);
            resourceIndexes = resourceIndexes.Concat(resource.rarity.GetWeight(i)).ToList();
        }

        return resourceList[resourceIndexes.PickRandom()];
    }

    /// <summary>
    /// Gets a random list of n Resources, decided by the weighted rarities.
    /// </summary>
    /// <returns>The list of random Resources</returns>
    public static List<Resource> GetRandomResources(int num)
    {
        List<Resource> resourceList = new();

        for (int i = 0; i < num; i++)
        {
            resourceList.Add(GetRandomResource());
        }

        return resourceList;
    }

    /// <summary>
    /// Gets a random list of n Resources from a list of possible Resources, decided by the weighted rarities.
    /// </summary>
    /// <returns>The list of random Resources</returns>
    public static List<Resource> GetRandomResources(int num, List<string> names)
    {
        List<Resource> resourceList = new();

        for (int i = 0; i < num; i++)
        {
            resourceList.Add(GetRandomResource(names));
        }

        return resourceList;
    }

    /// <summary>
    /// Gets a list of Resources by name category or subcategory.
    /// </summary>
    /// <returns>The found Resource</returns>
    public static List<Resource> GetResources(string category, bool isCategory)
    {
        return ResourceInfo
            .Where(r => isCategory && (r.Category == category) || !isCategory && r.Subcategory == category)
            .Select(r => new Resource(r.Name))
            .ToList();
    }

    /// <summary>
    /// Gets a Resources by a list of names.
    /// </summary>
    /// <returns>The found Resource</returns>
    public static List<Resource> GetResources(List<string> names)
    {
        return names.Select(r => new Resource(r)).ToList();
    }

    override public string ToString()
    {
        return $"{{ Name: '{name}', Category: '{category.Primary}', Subcategory: '{category.Secondary}', Rarity: '{rarity.GetRarityText()}', Price: '{price}', Description: '{description}' }}";
    }
}