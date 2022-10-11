using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defective.JSON;

public static class DebugUtilities
{

    public static List<string> GetResourceNames()
    {
        List<string> resourceNames = new();
        TextAsset infoJSON = Resources.Load<TextAsset>("ResourceInfo");
        List<JSONObject> jsonList = JSONObject.Create(infoJSON.text).list[0].list;

        for (int i = 0; i < jsonList.Count; i++)
        {
            resourceNames.Add(jsonList[i][0].stringValue);
        }

        return resourceNames;
    }

    public static List<(string, List<string>)> GetLocationResources()
    {
        List<(string, List<string>)> locationResources = new();
        TextAsset infoJSON = Resources.Load<TextAsset>("LocationInfo");
        List<JSONObject> jsonList = JSONObject.Create(infoJSON.text).list[0].list;

        for (int i = 0; i < jsonList.Count; i++)
        {
            List<string> resourceNames = new();
            List<JSONObject> resourceModifierList = jsonList[i][3].list;

            for (int j = 0; j < resourceModifierList.Count; j++)
            {
                resourceNames.Add(resourceModifierList[j].stringValue);
            }

            locationResources.Add((jsonList[i][0].stringValue, resourceNames));
        }

        return locationResources;
    }

    public static List<(string, List<string>)> GetContractResources()
    {
        List<(string, List<string>)> contractResources = new();
        TextAsset infoJSON = Resources.Load<TextAsset>("ContractInfo");
        List<JSONObject> jsonList = JSONObject.Create(infoJSON.text).list[0].list;

        for (int i = 0; i < jsonList.Count; i++)
        {
            for (int j = 0; j < jsonList[i][1].list.Count; j++)
            {
                List<string> resourceNames = new();
                List<JSONObject> resourceModifierList = jsonList[i][1].list[j][7].list;

                for (int k = 0; resourceModifierList != null && k < resourceModifierList.Count; k++)
                {
                    resourceNames.Add(resourceModifierList[k].stringValue);
                }

                contractResources.Add((jsonList[i][1].list[j][0].stringValue, resourceNames));
            }


        }

        return contractResources;
    }

    public static void CheckForMisspelledResourceNames()
    {
        List<string> resourceNames = GetResourceNames();
        List<(string, List<string>)> locationResources = GetLocationResources();
        List<(string, List<string>)> contractResources = GetContractResources();

        for (int i = 0; i < locationResources.Count; i++)
        {
            for (int j = 0; j < locationResources[i].Item2.Count; j++)
            {
                if (!resourceNames.Contains(locationResources[i].Item2[j]))
                {
                    Debug.Log($"Location: {locationResources[i].Item1}, Resource: {locationResources[i].Item2}");
                }
            }
        }
        for (int i = 0; i < contractResources.Count; i++)
        {
            for (int j = 0; j < contractResources[i].Item2.Count; j++)
            {
                if (!resourceNames.Contains(contractResources[i].Item2[j]))
                {
                    Debug.Log($"Contract: {contractResources[i].Item1}, Resource: {contractResources[i].Item2}");
                }
            }
        }
    }

}
