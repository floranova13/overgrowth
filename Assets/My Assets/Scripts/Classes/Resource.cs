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

    public string Name { get; }
    public Category Category { get; }
    public int Price { get; }
    public Rarity Rarity { get; }
    public string Description { get; }

    public int Count { get; set; }

    public Resource(string nameVal, int count = 1)
    {
        ResourceData info = GetResourceData(nameVal);

        Name = nameVal;
        Category = new Category(info.Category, info.Subcategory);
        Price = info.Price;
        Rarity = new Rarity(info.Rarity, "Resource Class");
        Description = info.Description;
        Count = count;
    }

    public Resource(ResourceData info, int count = 0)
    {

        Name = info.Name;
        Category = new Category(info.Category, info.Subcategory);
        Price = info.Price;
        Rarity = new Rarity(info.Rarity, "Resource Class with Count");
        Description = info.Description;
        Count = count;
    }

    public ResourceData GetResourceData(string name)
    {
        ResourceData info = ResourceInfo.FirstOrDefault(resourceData => resourceData.Name == name);

        if (info.Name == null)
        {
            Debug.LogError($"GetResourceData| No Resource Info Found For: {name}");
        }

        return info;
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

        Debug.Log($"Resource Data Count: {resources.Count}");

        return resources;
    }

    // Same:
    // ------------------------------------------------------------------------------------------
    /// <summary>
    /// Gets a specific Resource from a list of Resource or a new one if there is none.
    /// </summary>
    /// <param name="resourceList">Resource to search through</param>
    /// <param name="resource">Resource to search for</param>
    /// <returns>The Resource found</returns>
    public static Resource Same(List<Resource> resourceList, Resource resource, bool requireCount = true)
    {
        if (resource == null)
        {
            Debug.LogError("Searching for NULL Resource.");
            return null;
        }
        if (resourceList.FirstOrDefault(x => x.Name == resource.Name) == null)
        {
            if (requireCount)
            {
                return null;
            }
            return new Resource(resource.Name, 0);
        }
        Resource foundResource = resourceList.FirstOrDefault(x => x.Name == resource.Name);

        return (requireCount && (foundResource.Count >= resource.Count))
            ? null
            : foundResource;
    }

    /// <summary>
    /// Gets a specific List of Resource from a list of Resource, replacing any not found with
    /// new ones if one is not found.
    /// </summary>
    /// <param name="resourceList">Resource to search through</param>
    /// <param name="resourceToSearchFor">Resource to search for</param>
    /// <returns>The Resources found</returns>
    public static List<Resource> Same(List<Resource> resourceList, List<Resource> resourcesToSearchFor, bool requireCounts = true)
    {
        List<Resource> resourcesFoundList = new();
        for (int i = 0; i < resourcesToSearchFor.Count; i++)
        {
            Resource foundResource = Same(resourceList, resourcesToSearchFor[i], requireCounts);

            if (foundResource == null || (requireCounts && foundResource.Count < resourcesToSearchFor[i].Count))
            {
                return null;
            }

            resourcesFoundList.Add(foundResource);
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
            resourceIndexes = resourceIndexes.Concat(resource.Rarity.GetWeight(i)).ToList();
        }

        return resourceList[resourceIndexes.PickRandom()];
    }

    /// <summary>
    /// Gets a random Resource of the chosen Rarity or below, decided by the weighted rarities.
    /// </summary>
    /// <returns>The random Resource</returns>
    //     public static Resource GetRandomResource(string rarity)
    //     {
    //         return switch rarity {
    //             "Common" => GetRandomResource(GetResources("Common")),
    //             "Uncommon" => GetRandomResource(GetResources("Common").Join(GetResources("Uncommon")).ToList()),
    //             "Rare" => GetRandomResource(GetResources("Common").Join(GetResources("Uncommon")).ToList().Join(GetResources("Rare").ToList())),
    //             "Wondrous" => GetRandomResource(GetResources("Common").Join(GetResources("Uncommon")).ToList().Join(GetResources("Rare").ToList().Join(GetResources("Wondrous")))),
    //         }
    // }

    /// <summary>
    /// Gets a list of Resources of the chosen Rarity.
    /// </summary>
    /// <returns>The Resource list</returns>
    public static List<ResourceData> GetResources(string rarity)
    {
        return ResourceInfo.Where(resource => resource.Rarity == rarity).ToList();
    }

    /// <summary>
    /// Gets a random Resource from a list of possible resource names, decided by the weighted rarities.
    /// </summary>
    /// <returns>The random Resource</returns>
    public static Resource GetRandomResource(List<string> names)
    {
        // Debug.Log($"Resource - GetRandomResource(List<string>)| Getting Item Weights For: {string.Join(',', names)}");
        List<Resource> resourceList = new();
        List<int> resourceIndexes = new();

        for (int i = 0; i < names.Count; i++)
        {
            Resource resource = new(names[i]);
            resourceList.Add(resource);
            resourceIndexes = resourceIndexes.Concat(resource.Rarity.GetWeight(i)).ToList();
        }

        // Debug.Log($"Resource - GetRandomResource| Resource Indexes: {string.Join(',', resourceIndexes)}");

        return resourceList[resourceIndexes.PickRandom()];
    }

    /// <summary>
    /// Gets a random Resource from a list of possible resource names with modifiers, decided by the weighted rarities and the modifiers.
    /// </summary>
    /// <returns>The random Resource</returns>
    public static Resource GetRandomResource(List<(string resource, int modifier)> modifiers)
    {
        List<Resource> resourceList = new();
        List<int> resourceIndexes = new();

        for (int i = 0; i < modifiers.Count; i++)
        {
            Resource resource = new(modifiers[i].resource);
            resourceList.Add(resource);
            resourceIndexes = resourceIndexes.Concat(resource.Rarity.GetWeight(i, modifiers[i].modifier)).ToList();
        }

        return resourceList[resourceIndexes.PickRandom()];
    }

    /// <summary>
    /// Gets a random Resource from a list of possible resource names with modifiers, decided by the weighted rarities and the modifiers.
    /// </summary>
    /// <returns>The random Resource</returns>
    public static Resource GetRandomResource(List<(Resource resource, int modifier)> modifiers)
    {
        List<Resource> resourceList = new();
        List<int> resourceIndexes = new();

        for (int i = 0; i < modifiers.Count; i++)
        {
            resourceList.Add(modifiers[i].resource);
            resourceIndexes = resourceIndexes.Concat(modifiers[i].resource.Rarity.GetWeight(i, modifiers[i].modifier)).ToList();
        }

        return resourceList[resourceIndexes.PickRandom()];
    }

    /// <summary>
    /// Gets a random Resource from a list of possible resource data structs, decided by the weighted rarities.
    /// </summary>
    /// <returns>The random Resource</returns>
    public static Resource GetRandomResource(List<ResourceData> resources)
    {
        List<Resource> resourceList = new();
        List<int> resourceIndexes = new();

        for (int i = 0; i < resources.Count; i++)
        {
            Resource resource = new(resources[i].Name);
            resourceList.Add(resource);
            resourceIndexes = resourceIndexes.Concat(resource.Rarity.GetWeight(i)).ToList();
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
    /// Gets a random list of n Resources from a list of possible Resources, decided by the weighted rarities and modifiers.
    /// </summary>
    /// <returns>The list of random Resources</returns>
    public static List<Resource> GetRandomResources(int num, List<(string resource, int modifier)> modifiers)
    {
        List<Resource> resourceList = new();

        for (int i = 0; i < num; i++)
        {
            resourceList.Add(GetRandomResource(modifiers));
        }

        return resourceList;
    }

    /// <summary>
    /// Gets a random list of n Resources from a list of possible Resources, decided by the weighted rarities and modifiers.
    /// </summary>
    /// <returns>The list of random Resources</returns>
    public static List<Resource> GetRandomResources(int num, List<(Resource resource, int modifier)> modifiers)
    {
        List<Resource> resourceList = new();

        for (int i = 0; i < num; i++)
        {
            resourceList.Add(GetRandomResource(modifiers));
        }

        return resourceList;
    }

    /// <summary>
    /// Gets a random list of n Resources from a list of possible Resources, decided by the weighted rarities.
    /// </summary>
    /// <returns>The list of random Resources</returns>
    public static List<Resource> GetRandomResources(int num, List<ResourceData> resources)
    {
        List<Resource> resourceList = new();

        for (int i = 0; i < num; i++)
        {
            resourceList.Add(GetRandomResource(resources));
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
            .Select(r => new Resource(r))
            .ToList();
    }

    /// <summary>
    /// Gets a list of Resources by rarity and category or subcategory.
    /// </summary>
    /// <returns>The found Resource</returns>
    public static List<Resource> GetResources(string category, bool isCategory, string rarity)
    {
        return ResourceInfo
            .Where(r => r.Rarity == rarity && (isCategory && (r.Category == category) || !isCategory && r.Subcategory == category))
            .Select(r => new Resource(r))
            .ToList();
    }

    /// <summary>
    /// Gets a Resources by a list of names.
    /// </summary>
    /// <returns>The found Resource</returns>
    public static List<Resource> GetResources(List<string> names)
    {
        List<Resource> resources = new();
        for (int i = 0; i < names.Count; i++)
        {
            if (names[i].Contains(',') || IsCategory(names[i]) || IsSubcategory(names[i]))
            {
                if (names[i].Contains(','))
                {
                    string[] nameParts = names[i].Split(',');
                    resources.AddAll(GetResources(nameParts[1], IsCategory(nameParts[1]), nameParts[0]));
                }
                else
                {
                    Debug.Log($"Resource - GetResources| Getting Resources From Names: {string.Join(',', names)}");
                    // Debug.Log($"Resource - GetResources| Getting These Resources: {string.Join(',', GetResources(names[i], IsCategory(names[i])).Select(resource => resource.Name))}");

                    resources.AddAll(GetResources(names[i], IsCategory(names[i])));
                }
            }
            else
            {
                // Debug.Log($"Resource - GetResources| Adding new Resource: {names[i]}");
                resources.Add(new Resource(names[i]));
            }
        }

        // Debug.Log($"Resource - GetResources| Resources Found: {string.Join(',', resources.Select(resource => resource.Name))}");

        return resources;
    }

    public static List<string> GetResourceNames(List<string> names)
    {
        List<Resource> resources = GetResources(names);

        return resources.Select(r => r.Name).ToList();
    }

    override public string ToString()
    {
        return $"{{ Name: '{Name}', Category: '{Category.Primary}', Subcategory: '{Category.Secondary}', Rarity: '{Rarity.GetRarityText()}', Price: '{Price}', Description: '{Description}' }}";
    }

    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // Operator Overloading: 
    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------

    public static Resource operator +(Resource resource, int i)
    {
        resource.Count += i;
        return resource;
    }
    public static Resource operator +(Resource resource1, Resource resource2)
    {
        if (resource1.GetType() == resource2.GetType())
        {
            resource1.Count += resource2.Count;
            return resource1;
        }
        return resource1;
    }
    public static Resource operator -(Resource resource, int i)
    {
        resource.Count -= i;
        return resource;
    }
    public static Resource operator -(Resource resource1, Resource resource2)
    {
        if (resource1.GetType() == resource2.GetType())
        {
            resource1.Count -= resource2.Count;
            return resource1;
        }
        return resource1;
    }
    public static bool operator >=(Resource resource1, Resource resource2)
    {
        return resource1.GetType() == resource2.GetType() && resource1.Count >= resource2.Count;
    }
    public static bool operator <=(Resource resource1, Resource resource2)
    {
        return resource1.GetType() == resource2.GetType() && resource1.Count <= resource2.Count;
    }
    public static bool operator >=(Resource resource, int i)
    {
        return resource.Count >= i;
    }
    public static bool operator <=(Resource resource, int i)
    {
        return resource.Count >= i;
    }
    public static bool operator >(Resource resource1, Resource resource2)
    {
        return resource1.GetType() == resource2.GetType() && resource1.Count > resource2.Count;
    }
    public static bool operator <(Resource resource1, Resource resource2)
    {
        return resource1.GetType() == resource2.GetType() && resource1.Count < resource2.Count;
    }
    public static bool operator >(Resource resource, int i)
    {
        return resource.Count > i;
    }
    public static bool operator <(Resource resource, int i)
    {
        return resource.Count < i;
    }

    public static bool IsCategory(string label) => ResourceInfo.Any(resource => resource.Category == label);
    public static bool IsSubcategory(string label) => ResourceInfo.Any(resource => resource.Subcategory == label);
    public static bool IsCategoryOrSubcategory(string label) => IsCategory(label) || IsSubcategory(label);
}