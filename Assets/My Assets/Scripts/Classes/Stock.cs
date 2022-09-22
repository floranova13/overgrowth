using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using GameExtensions;

[Serializable]
public class Stock
{
    public List<string> PossibleInventory { get; }
    public int InventorySize { get; }
    public int InventoryRefreshInterval { get; }
    public int CostMargins { get; }
    private int MaxBudget { get; }

    public List<Resource> Inventory { get; private set; }
    public int Budget { get; private set; }
    public int RefreshCount { get; private set; }

    public Stock(List<string> possibleInventory, int inventorySize, int inventoryRefreshInterval, int costMargins, int maxBudget)
    {
        PossibleInventory = possibleInventory;
        InventorySize = inventorySize;
        InventoryRefreshInterval = inventoryRefreshInterval;
        CostMargins = costMargins;
        MaxBudget = maxBudget;
        Budget = maxBudget;
        RefreshCount = inventoryRefreshInterval;
        SetRandomInventory();
    }

    public void SetRandomInventory()
    {
        Inventory = Resource.GetRandomResources(InventorySize, PossibleInventory);
    }

    public void MakeTransaction(Resource resource, bool isBuying)
    {
         int price = GetAdjustedPrice(resource, isBuying);

        if (isBuying)
        {
            GameSave.s.resources.Add(resource, true);
            GameSave.s.petals -= price;
        }
        else
        {
            Resource foundResource = Resource.Same(GameSave.s.resources, resource);
            foundResource -= resource;
            GameSave.s.petals += price;
        }
    }

    public int GetAdjustedPrice(Resource resource, bool isBuying)
    {
        // TODO: CHANGE SO THAT THERE IS A CURVE, THE HIGHER THE MARGIN, THE LESS IT IMPACTS BUYING/SELLING
        double modifier = (isBuying ? 1 : -1) * 0.05 + 1;

        return Mathf.RoundToInt((float)modifier) * resource.Count * resource.Price;
    }

    public void DayPassed()
    {
        RefreshCount--;
        if (RefreshCount <= 0)
        {
            SetRandomInventory();
            RefreshCount = InventoryRefreshInterval;
        }
    }
}