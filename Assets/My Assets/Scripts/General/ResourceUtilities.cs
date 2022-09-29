using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GameExtensions;

public class ResourceUtilities : Singleton<ResourceUtilities>
{
    // Sprites taken from fantasyiconsmegapack~~~~~~~~~~~~~~~~~~~~~

    // General Sprites: 
    // ------------------------------------------------------------------------------------------
    public Sprite Resource_Sprite;
    public Sprite Petals_Sprite;

    // Category Sprites: 
    // ------------------------------------------------------------------------------------------
    public Sprite OrganicSamples_Sprite;
    public Sprite Geomaterials_Sprite;
    public Sprite Equipment_Sprite;
    public Sprite PersonalItems_Sprite;

    // Organic Samples Subcategories Sprites: 
    // ------------------------------------------------------------------------------------------
    public Sprite OrganicSamples_Flourishflora_Sprite;
    public Sprite OrganicSamples_Mosses_Sprite;
    public Sprite OrganicSamples_Falshrooms_Sprite;
    public Sprite OrganicSamples_Maremolds_Sprite;
    public Sprite OrganicSamples_FlesherFungi_Sprite;
    public Sprite OrganicSamples_Trees_Sprite;
    public Sprite OrganicSamples_Waveskellen_Sprite;
    public Sprite OrganicSamples_Stranglevines_Sprite;
    public Sprite OrganicSamples_Stillferns_Sprite;

    // Geomaterials Subcategories Sprites: 
    // ------------------------------------------------------------------------------------------
    public Sprite Geomaterials_Rocks_Sprite;
    public Sprite Geomaterials_MetallicOres_Sprite;
    public Sprite Geomaterials_IndustrialMinerals_Sprite;
    public Sprite Geomaterials_FuelMinerals_Sprite;
    public Sprite Geomaterials_GemMinerals_Sprite;
    public Sprite Geomaterials_Gemstones_Sprite;
    public Sprite Geomaterials_Metals_Sprite;


    // Equipment Subcategories Sprites: 
    // ------------------------------------------------------------------------------------------
    public Sprite Equipment_GuardianWeapons_Sprite;
    public Sprite Equipment_SeekerWeapons_Sprite;
    public Sprite Equipment_GuardianArmor_Sprite;
    public Sprite Equipment_SeekerArmor_Sprite;
    public Sprite Equipment_Utilities_Sprite;
    public Sprite Equipment_AnalysisGear_Sprite;
    public Sprite Equipment_SeekerBoosters_Sprite;
    public Sprite Equipment_EnvironmentalGear_Sprite;


    // Personal items Subcategories Sprites: 
    // ------------------------------------------------------------------------------------------
    public Sprite PersonalItems_Accords_Sprite;

    // ------------------------------------------------------------------------------------------
    // Get Resource Sprite: 
    // ------------------------------------------------------------------------------------------
    public Sprite GetBaseResourceSprite(string resourceName)
    {
        Resource resource = new(resourceName);
        Dictionary<string, Sprite> organicSamplesDictionary = new() {
          { "Flourishflora", OrganicSamples_Flourishflora_Sprite },
          { "Mosses", OrganicSamples_Mosses_Sprite },
          { "Falshrooms", OrganicSamples_Falshrooms_Sprite },
          { "Maremolds", OrganicSamples_Maremolds_Sprite },
          { "Flesher Fungi", OrganicSamples_FlesherFungi_Sprite },
          { "Trees", OrganicSamples_Trees_Sprite },
          { "Waveskellen", OrganicSamples_Waveskellen_Sprite },
          { "Stranglevines", OrganicSamples_Stranglevines_Sprite },
          { "Stillferns", OrganicSamples_Stillferns_Sprite }
        };
        Dictionary<string, Sprite> geomaterialsDictionary = new() {
          { "Rocks", Geomaterials_Rocks_Sprite },
          { "Metallic Ores", Geomaterials_MetallicOres_Sprite },
          { "Industrial Minerals", Geomaterials_IndustrialMinerals_Sprite },
          { "Fuel Minerals", Geomaterials_FuelMinerals_Sprite },
          { "Gem Minerals", Geomaterials_GemMinerals_Sprite },
          { "Gemstones", Geomaterials_Gemstones_Sprite },
          { "Metals", Geomaterials_Metals_Sprite },
        };
        Dictionary<string, Sprite> equipmentDictionary = new() {
          { "Guardian Weapons", Equipment_GuardianWeapons_Sprite },
          { "Seeker Weapons", Equipment_SeekerWeapons_Sprite },
          { "Guardian Armor", Equipment_GuardianArmor_Sprite },
          { "Seeker Armor", Equipment_SeekerArmor_Sprite },
          { "Utilities", Equipment_Utilities_Sprite },
          { "Analysis Gear", Equipment_AnalysisGear_Sprite },
          { "Seeker Boosters", Equipment_SeekerBoosters_Sprite },
          { "Environmental Gear", Equipment_EnvironmentalGear_Sprite }
        };
        Dictionary<string, Sprite> personalItemsDictionary = new() {
          { "Accords", PersonalItems_Accords_Sprite }
        };

        try
        {
            switch (resource.Category.Primary)
            {
                case "Organic Samples":
                    return organicSamplesDictionary[resource.Category.Secondary];
                case "Geomaterials":
                    return geomaterialsDictionary[resource.Category.Secondary];
                case "Equipment":
                    return equipmentDictionary[resource.Category.Secondary];
                case "Personal Items":
                    return personalItemsDictionary[resource.Category.Secondary];
                default:
                    break;
            }
        }
        catch (System.Exception)
        {
            Debug.LogError($"Resource failed to be created: {resource}");
            throw;
        }
        return null;
    }
}