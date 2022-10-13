using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using GameExtensions;

[Serializable]
public class Stock
{
    private Merchant Merchant { get; }
    public List<string> PossibleInventory { get; }
    public int InventorySize { get; }
    public int InventoryRefreshInterval { get; }
    public int CostMargins { get; }
    private int MaxBudget { get; }

    public List<Resource> Inventory { get; private set; }
    public int Budget { get; private set; }
    public int RefreshCount { get; private set; }

    public Stock(List<string> possibleInventory, int inventorySize, int inventoryRefreshInterval, int costMargins, int maxBudget, Merchant merchant)
    {
        PossibleInventory = possibleInventory;
        InventorySize = inventorySize;
        InventoryRefreshInterval = inventoryRefreshInterval;
        CostMargins = costMargins;
        MaxBudget = maxBudget;
        Budget = maxBudget;
        RefreshCount = inventoryRefreshInterval;
        Merchant = merchant;
        SetRandomInventory();
    }

    public void SetRandomInventory()
    {
        Debug.Log($"Stock - SetRandomInventory| Possible Inventory: {string.Join(',', PossibleInventory)}");
        Debug.Log($"Stock - SetRandomInventory| Possible Inventory, Unpacked: {string.Join(',', Resource.GetResourceNames(PossibleInventory))}");
        Inventory = Resource.GetRandomResources(InventorySize, Resource.GetResourceNames(PossibleInventory));
    }

    public int GetAdjustedPrice(Resource resource, bool isBuying)
    {
        // TODO: CHANGE BASED ON REPUTATION
        // TODO: CHANGE SO THAT THERE IS A CURVE, THE HIGHER THE MARGIN, THE LESS IT IMPACTS BUYING/SELLING
        int reputation = Merchant.Reputation;
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
            Budget = MaxBudget;
        }
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
}