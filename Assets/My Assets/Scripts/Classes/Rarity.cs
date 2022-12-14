using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;
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

    public Rarity(string rarityVal, string origin)
    {
        if (RarityNames.Contains(rarityVal))
        {
            rarity = RarityNames.IndexOf(rarityVal);
        }
        else
        {
            rarity = 0;
            Debug.Log($"Rarity {rarityVal} not in the list of possible Rarities! Origin: {origin}");
        }
    }

    public List<int> GetWeight(int i, int modifier = 0)
    {
        var weightList = new List<int>();

        for (int j = 0; j <= RarityWeights[RarityNames[rarity]] + modifier; j++)
        {
            weightList.Add(i);
        }

        // Debug.Log($"Rarity - GetWeight| Weights: {string.Join(',', (weightList.Select(weight => weight.ToString()).ToList()))}");

        return weightList;
    }

    public override string ToString()
    {
        return GetRarityText();
    }
}