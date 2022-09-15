using System.Collections.Generic;
using System;
using System.Collections;

[Serializable]
public class Rarity
{
    /// <summary>
    /// The higher the number, the rarer the resource.
    /// </summary>
    public int rarity;

    public static List<string> RarityNames = new List<string>() { "Common", "Uncommon", "Rare", "Wondrous" };
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
            rarity = int.Parse(rarityVal);
        }
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

    /// <summary>
    /// Gets a random Flora decided by the weighted rarities.
    /// </summary>
    /// <returns>The random Flora</returns>
    public T GetRandomFlora(IEnumerable<T> listToPickFrom)
    {
        List<T> newList = new();
        List<int> indexList = new();

        for (int i = 0; i < FloraInfo.Count; i++)
        {
            T currentElement = new(FloraInfo[i][0]);
            floraList.Add(currentElement);
            indexList = indexList.Concat(flora.rarity.GetWeight(i)).ToList();
        }

        return floraList[indexList.PickRandom()];
    }
}