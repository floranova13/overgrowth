using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Defective.JSON;
using GameExtensions;

[Serializable]
public class Location
{
    public static List<Location> Locations;

    public string Name { get; }
    public string Territory { get; }
    public int Threat { get; }
    public List<(string resource, int modifier)> AvailableResources { get; }
    public string Description { get; }

    public Location(string name, string territory, int threat, List<(string resource, int modifier)> availableResources, string description)
    {
        Name = name;
        Territory = territory;
        Threat = threat;
        AvailableResources = availableResources;
        Description = description;
    }

    public static void ReadFromJSON()
    {
        TextAsset infoJSON = Resources.Load<TextAsset>("LocationsInfo");
        JSONObject jsonObject = JSONObject.Create(infoJSON.text).list[0];
        Locations = new List<Location>();

        for (int i = 0; i < jsonObject.list.Count; i++)
        {
            Location newLocation = new(
                jsonObject[i][0].stringValue, jsonObject[i][1].stringValue,
                jsonObject[i][2].intValue,
                jsonObject[3].list.Select(result => (resource: result[0].stringValue, modifier: result[1].intValue)).ToList(),
                jsonObject[i][4].stringValue
                );
            Locations.Add(newLocation);
        }
    }

    /// <summary>
    /// Gets a random Location
    /// </summary>
    /// <returns>The random Location</returns>
    public static Location GetRandomLocation() => Locations.PickRandom();

    /// <summary>
    /// Gets a random Location from a list of possible Location names
    /// </summary>
    /// <returns>The random Location</returns>
    public static Location GetRandomLocation(List<string> names)
    {
        return Locations.Where(Location => names.Contains(Location.Name)).ToList().PickRandom();
    }

    public static Location GetLocation(string locationName)
    {
        return Locations.FirstOrDefault(location => location.Name == locationName);
    }
}