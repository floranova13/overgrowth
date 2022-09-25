using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Rarity
{
    /// <summary>
    /// The higher the number, the rarer the resource.
    /// </summary>
    public int rarity;

    public static List<string> RarityNames = new() { "Common", "Uncommon", "Rare", "Wondrous" };
    public static Dictionary<string, int> RarityWeights = new() { { "Common", 27 }, { "Uncommon", 9 }, { "Rare", 3 }, { "Wondrous", 1 } };

    public string GetRarityText() { return RarityNames[rarity]; }

    public Rarity(int rarityVal)
    {
        rarity = rarityVal;
    }

    public Rarity(string rarityVal)
    {
        if (RarityNames.Contains(rarityVal))
        {
            rarity = RarityNames.IndexOf(rarityVal);
        }
        else
        {
            rarity = 0;
        }
        // if (rarityVal == "")
        // {
        //     rarity = 0;
        // }

        // if (RarityNames.Contains(rarityVal.ToLower()))
        // {
        //     rarity = RarityNames.IndexOf(rarityVal);
        // }
        // else if (int.TryParse(rarityVal, out int i))
        // {

        //     rarity = int.Parse(rarityVal);
        // }
        // else
        // {
        //     Debug.Log($"Rarity: {rarityVal} is not valid!");
        //     rarity = 0;
        // }
    }

    public List<int> GetWeight(int i)
    {
        var weightList = new List<int>();

        for (int j = 0; j < rarity; j++)
        {
            weightList.Add(i);
        }

        return weightList;
    }

    public override string ToString()
    {
        return GetRarityText();
    }
}