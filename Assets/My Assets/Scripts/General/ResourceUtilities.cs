using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GameExtensions;

public class ResourceUtilities : Singleton<ResourceUtilities>
{
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
    public Sprite OrganicSamples_Flourishflora0_Sprite;
    public Sprite OrganicSamples_Flourishflora1_Sprite;
    public Sprite OrganicSamples_Flourishflora2_Sprite;
    public Sprite OrganicSamples_Flourishflora3_Sprite;
    public Sprite OrganicSamples_Mosses_Sprite;
    public Sprite OrganicSamples_Mosses0_Sprite;
    public Sprite OrganicSamples_Mosses1_Sprite;
    public Sprite OrganicSamples_Mosses2_Sprite;
    public Sprite OrganicSamples_Mosses3_Sprite;
    public Sprite OrganicSamples_Falshrooms_Sprite;
    public Sprite OrganicSamples_Falshrooms0_Sprite;
    public Sprite OrganicSamples_Falshrooms1_Sprite;
    public Sprite OrganicSamples_Falshrooms2_Sprite;
    public Sprite OrganicSamples_Falshrooms3_Sprite;
    public Sprite OrganicSamples_Maremolds_Sprite;
    public Sprite OrganicSamples_Maremolds0_Sprite;
    public Sprite OrganicSamples_Maremolds1_Sprite;
    public Sprite OrganicSamples_Maremolds2_Sprite;
    public Sprite OrganicSamples_Maremolds3_Sprite;
    public Sprite OrganicSamples_FlesherFungi_Sprite;
    public Sprite OrganicSamples_FlesherFungi0_Sprite;
    public Sprite OrganicSamples_FlesherFungi1_Sprite;
    public Sprite OrganicSamples_FlesherFungi2_Sprite;
    public Sprite OrganicSamples_FlesherFungi3_Sprite;
    public Sprite OrganicSamples_Trees_Sprite;
    public Sprite OrganicSamples_Trees0_Sprite;
    public Sprite OrganicSamples_Trees1_Sprite;
    public Sprite OrganicSamples_Trees2_Sprite;
    public Sprite OrganicSamples_Trees3_Sprite;
    public Sprite OrganicSamples_Waveskellen_Sprite;
    public Sprite OrganicSamples_Waveskellen0_Sprite;
    public Sprite OrganicSamples_Waveskellen1_Sprite;
    public Sprite OrganicSamples_Waveskellen2_Sprite;
    public Sprite OrganicSamples_Waveskellen3_Sprite;
    public Sprite OrganicSamples_Stranglevines_Sprite;
    public Sprite OrganicSamples_Stranglevines0_Sprite;
    public Sprite OrganicSamples_Stranglevines1_Sprite;
    public Sprite OrganicSamples_Stranglevines2_Sprite;
    public Sprite OrganicSamples_Stranglevines3_Sprite;
    public Sprite OrganicSamples_Stillferns_Sprite;
    public Sprite OrganicSamples_Stillferns0_Sprite;
    public Sprite OrganicSamples_Stillferns1_Sprite;
    public Sprite OrganicSamples_Stillferns2_Sprite;
    public Sprite OrganicSamples_Stillferns3_Sprite;

    // Geomaterials Subcategories Sprites: 
    // ------------------------------------------------------------------------------------------
    public Sprite Geomaterials_Rocks_Sprite;
    public Sprite Geomaterials_Rocks0_Sprite;
    public Sprite Geomaterials_Rocks1_Sprite;
    public Sprite Geomaterials_Rocks2_Sprite;
    public Sprite Geomaterials_Rocks3_Sprite;
    public Sprite Geomaterials_MetallicOres_Sprite;
    public Sprite Geomaterials_MetallicOres0_Sprite;
    public Sprite Geomaterials_MetallicOres1_Sprite;
    public Sprite Geomaterials_MetallicOres2_Sprite;
    public Sprite Geomaterials_MetallicOres3_Sprite;
    public Sprite Geomaterials_IndustrialMinerals_Sprite;
    public Sprite Geomaterials_IndustrialMinerals0_Sprite;
    public Sprite Geomaterials_IndustrialMinerals1_Sprite;
    public Sprite Geomaterials_IndustrialMinerals2_Sprite;
    public Sprite Geomaterials_IndustrialMinerals3_Sprite;
    public Sprite Geomaterials_FuelMinerals_Sprite;
    public Sprite Geomaterials_FuelMinerals0_Sprite;
    public Sprite Geomaterials_FuelMinerals1_Sprite;
    public Sprite Geomaterials_FuelMinerals2_Sprite;
    public Sprite Geomaterials_FuelMinerals3_Sprite;
    public Sprite Geomaterials_GemMinerals_Sprite;
    public Sprite Geomaterials_GemMinerals0_Sprite;
    public Sprite Geomaterials_GemMinerals1_Sprite;
    public Sprite Geomaterials_GemMinerals2_Sprite;
    public Sprite Geomaterials_GemMinerals3_Sprite;
    public Sprite Geomaterials_Metals_Sprite;
    public Sprite Geomaterials_Metals0_Sprite;
    public Sprite Geomaterials_Metals1_Sprite;
    public Sprite Geomaterials_Metals2_Sprite;
    public Sprite Geomaterials_Metals3_Sprite;


    // Equipment Subcategories Sprites: 
    // ------------------------------------------------------------------------------------------
    public Sprite Equipment_GuardianWeapons_Sprite;
    public Sprite Equipment_GuardianWeapons0_Sprite;
    public Sprite Equipment_GuardianWeapons1_Sprite;
    public Sprite Equipment_GuardianWeapons2_Sprite;
    public Sprite Equipment_GuardianWeapons3_Sprite;
    public Sprite Equipment_SeekerWeapons_Sprite;
    public Sprite Equipment_SeekerWeapons0_Sprite;
    public Sprite Equipment_SeekerWeapons1_Sprite;
    public Sprite Equipment_SeekerWeapons2_Sprite;
    public Sprite Equipment_SeekerWeapons3_Sprite;
    public Sprite Equipment_GuardianArmor_Sprite;
    public Sprite Equipment_GuardianArmor0_Sprite;
    public Sprite Equipment_GuardianArmor1_Sprite;
    public Sprite Equipment_GuardianArmor2_Sprite;
    public Sprite Equipment_GuardianArmor3_Sprite;
    public Sprite Equipment_SeekerArmor_Sprite;
    public Sprite Equipment_SeekerArmor0_Sprite;
    public Sprite Equipment_SeekerArmor1_Sprite;
    public Sprite Equipment_SeekerArmor2_Sprite;
    public Sprite Equipment_SeekerArmor3_Sprite;
    public Sprite Equipment_Utilities_Sprite;
    public Sprite Equipment_Utilities0_Sprite;
    public Sprite Equipment_Utilities1_Sprite;
    public Sprite Equipment_Utilities2_Sprite;
    public Sprite Equipment_Utilities3_Sprite;
    public Sprite Equipment_AnalysisGear_Sprite;
    public Sprite Equipment_AnalysisGear0_Sprite;
    public Sprite Equipment_AnalysisGear1_Sprite;
    public Sprite Equipment_AnalysisGear2_Sprite;
    public Sprite Equipment_AnalysisGear3_Sprite;
    public Sprite Equipment_SeekerBoosters_Sprite;
    public Sprite Equipment_SeekerBoosters0_Sprite;
    public Sprite Equipment_SeekerBoosters1_Sprite;
    public Sprite Equipment_SeekerBoosters2_Sprite;
    public Sprite Equipment_SeekerBoosters3_Sprite;
    public Sprite Equipment_EnvironmentalGear_Sprite;
    public Sprite Equipment_EnvironmentalGear0_Sprite;
    public Sprite Equipment_EnvironmentalGear1_Sprite;
    public Sprite Equipment_EnvironmentalGear2_Sprite;
    public Sprite Equipment_EnvironmentalGear3_Sprite;


    // Personal items Subcategories Sprites: 
    // ------------------------------------------------------------------------------------------
    public Sprite PersonalItems_Accords_Sprite;
    public Sprite PersonalItems_Accords0_Sprite;
    public Sprite PersonalItems_Accords1_Sprite;
    public Sprite PersonalItems_Accords2_Sprite;
    public Sprite PersonalItems_Accords3_Sprite;

    // ------------------------------------------------------------------------------------------
    // Get Item Sprite: 
    // ------------------------------------------------------------------------------------------
    public Sprite GetResourceSprite(string resourceName)
    {
        Resource resource = new(resourceName);

        // ---- Organic Samples Subcategories ------
        Dictionary<string, Sprite> flourishfloraDictionary = new()
        {
          { "Common", OrganicSamples_Flourishflora0_Sprite },
          { "Uncommon", OrganicSamples_Flourishflora1_Sprite },
          { "Rare", OrganicSamples_Flourishflora2_Sprite },
          { "Wondrous", OrganicSamples_Flourishflora3_Sprite },
        };
        Dictionary<string, Sprite> mossesDictionary = new()
        {
          { "Common", OrganicSamples_Mosses0_Sprite },
          { "Uncommon", OrganicSamples_Mosses1_Sprite },
          { "Rare", OrganicSamples_Mosses2_Sprite },
          { "Wondrous", OrganicSamples_Mosses3_Sprite },
        };
        Dictionary<string, Sprite> falshroomsDictionary = new()
        {
          { "Common", OrganicSamples_Falshrooms0_Sprite },
          { "Uncommon", OrganicSamples_Falshrooms1_Sprite },
          { "Rare", OrganicSamples_Falshrooms2_Sprite },
          { "Wondrous", OrganicSamples_Falshrooms3_Sprite },
        };
        Dictionary<string, Sprite> maremoldsDictionary = new()
        {
          { "Common", OrganicSamples_Maremolds0_Sprite },
          { "Uncommon", OrganicSamples_Maremolds1_Sprite },
          { "Rare", OrganicSamples_Maremolds2_Sprite },
          { "Wondrous", OrganicSamples_Maremolds3_Sprite },
        };
        Dictionary<string, Sprite> flesherFungiDictionary = new()
        {
          { "Common", OrganicSamples_FlesherFungi0_Sprite },
          { "Uncommon", OrganicSamples_FlesherFungi1_Sprite },
          { "Rare", OrganicSamples_FlesherFungi2_Sprite },
          { "Wondrous", OrganicSamples_FlesherFungi3_Sprite },
        };
        Dictionary<string, Sprite> treesDictionary = new()
        {
          { "Common", OrganicSamples_Trees0_Sprite },
          { "Uncommon", OrganicSamples_Trees1_Sprite },
          { "Rare", OrganicSamples_Trees2_Sprite },
          { "Wondrous", OrganicSamples_Trees3_Sprite },
        };
        Dictionary<string, Sprite> waveskellenDictionary = new()
        {
          { "Common", OrganicSamples_Waveskellen0_Sprite },
          { "Uncommon", OrganicSamples_Waveskellen1_Sprite },
          { "Rare", OrganicSamples_Waveskellen2_Sprite },
          { "Wondrous", OrganicSamples_Waveskellen3_Sprite },
        };
        Dictionary<string, Sprite> stranglevinesDictionary = new()
        {
          { "Common", OrganicSamples_Stranglevines0_Sprite },
          { "Uncommon", OrganicSamples_Stranglevines1_Sprite },
          { "Rare", OrganicSamples_Stranglevines2_Sprite },
          { "Wondrous", OrganicSamples_Stranglevines3_Sprite },
        };
        Dictionary<string, Sprite> stillfernsDictionary = new()
        {
          { "Common", OrganicSamples_Stillferns0_Sprite },
          { "Uncommon", OrganicSamples_Stillferns1_Sprite },
          { "Rare", OrganicSamples_Stillferns2_Sprite },
          { "Wondrous", OrganicSamples_Stillferns3_Sprite },
        };
        // ---- Geomaterials Subcategories ------
        Dictionary<string, Sprite> rocksDictionary = new()
        {
          { "Common", Geomaterials_Rocks0_Sprite },
          { "Uncommon", Geomaterials_Rocks1_Sprite },
          { "Rare", Geomaterials_Rocks2_Sprite },
          { "Wondrous", Geomaterials_Rocks3_Sprite },
        };
        Dictionary<string, Sprite> metallicOresDictionary = new()
        {
          { "Common", Geomaterials_MetallicOres0_Sprite },
          { "Uncommon", Geomaterials_MetallicOres1_Sprite },
          { "Rare", Geomaterials_MetallicOres2_Sprite },
          { "Wondrous", Geomaterials_MetallicOres3_Sprite },
        };
        Dictionary<string, Sprite> industrialMineralsDictionary = new()
        {
          { "Common", Geomaterials_IndustrialMinerals0_Sprite },
          { "Uncommon", Geomaterials_IndustrialMinerals1_Sprite },
          { "Rare", Geomaterials_IndustrialMinerals2_Sprite },
          { "Wondrous", Geomaterials_IndustrialMinerals3_Sprite },
        };
        Dictionary<string, Sprite> fuelMineralsDictionary = new()
        {
          { "Common", Geomaterials_FuelMinerals0_Sprite },
          { "Uncommon", Geomaterials_FuelMinerals1_Sprite },
          { "Rare", Geomaterials_FuelMinerals2_Sprite },
          { "Wondrous", Geomaterials_FuelMinerals3_Sprite },
        };
        Dictionary<string, Sprite> gemMineralsDictionary = new()
        {
          { "Common", Geomaterials_GemMinerals0_Sprite },
          { "Uncommon", Geomaterials_GemMinerals1_Sprite },
          { "Rare", Geomaterials_GemMinerals2_Sprite },
          { "Wondrous", Geomaterials_GemMinerals3_Sprite },
        };
        Dictionary<string, Sprite> metalsDictionary = new()
        {
          { "Common", Geomaterials_Metals0_Sprite },
          { "Uncommon", Geomaterials_Metals1_Sprite },
          { "Rare", Geomaterials_Metals2_Sprite },
          { "Wondrous", Geomaterials_Metals3_Sprite },
        };
        // ---- Equipment Subcategories ------
        Dictionary<string, Sprite> guardianWeaponsDictionary = new()
        {
          { "Common", Equipment_GuardianWeapons0_Sprite },
          { "Uncommon", Equipment_GuardianWeapons1_Sprite },
          { "Rare", Equipment_GuardianWeapons2_Sprite },
          { "Wondrous", Equipment_GuardianWeapons3_Sprite },
        };
        Dictionary<string, Sprite> seekerWeaponsDictionary = new()
        {
          { "Common", Equipment_SeekerWeapons0_Sprite },
          { "Uncommon", Equipment_SeekerWeapons1_Sprite },
          { "Rare", Equipment_SeekerWeapons2_Sprite },
          { "Wondrous", Equipment_SeekerWeapons3_Sprite },
        };
        Dictionary<string, Sprite> guardianArmorDictionary = new()
        {
          { "Common", Equipment_GuardianArmor0_Sprite },
          { "Uncommon", Equipment_GuardianArmor1_Sprite },
          { "Rare", Equipment_GuardianArmor2_Sprite },
          { "Wondrous", Equipment_GuardianArmor3_Sprite },
        };
        Dictionary<string, Sprite> seekerArmorDictionary = new()
        {
          { "Common", Equipment_SeekerArmor0_Sprite },
          { "Uncommon", Equipment_SeekerArmor1_Sprite },
          { "Rare", Equipment_SeekerArmor2_Sprite },
          { "Wondrous", Equipment_SeekerArmor3_Sprite },
        };
        Dictionary<string, Sprite> utilitiesDictionary = new()
        {
          { "Common", Equipment_Utilities0_Sprite },
          { "Uncommon", Equipment_Utilities1_Sprite },
          { "Rare", Equipment_Utilities2_Sprite },
          { "Wondrous", Equipment_Utilities3_Sprite },
        };
        Dictionary<string, Sprite> analysisGearDictionary = new()
        {
          { "Common", Equipment_AnalysisGear0_Sprite },
          { "Uncommon", Equipment_AnalysisGear1_Sprite },
          { "Rare", Equipment_AnalysisGear2_Sprite },
          { "Wondrous", Equipment_AnalysisGear3_Sprite },
        };
        Dictionary<string, Sprite> seekerBoostersDictionary = new()
        {
          { "Common", Equipment_SeekerBoosters0_Sprite },
          { "Uncommon", Equipment_SeekerBoosters1_Sprite },
          { "Rare", Equipment_SeekerBoosters2_Sprite },
          { "Wondrous", Equipment_SeekerBoosters3_Sprite },
        };
        Dictionary<string, Sprite> environmentalGearDictionary = new()
        {
          { "Common", Equipment_EnvironmentalGear0_Sprite },
          { "Uncommon", Equipment_EnvironmentalGear1_Sprite },
          { "Rare", Equipment_EnvironmentalGear2_Sprite },
          { "Wondrous", Equipment_EnvironmentalGear3_Sprite },
        };
        // ---- Personal Items Subcategories ------
        Dictionary<string, Sprite> accordsDictionary = new()
        {
          { "Common", PersonalItems_Accords0_Sprite },
          { "Uncommon", PersonalItems_Accords1_Sprite },
          { "Rare", PersonalItems_Accords2_Sprite },
          { "Wondrous", PersonalItems_Accords3_Sprite },
        };

        // ---- Categories ------
        Dictionary<string, Dictionary<string, Sprite>> organicSamplesDictionary = new() {
          { "Flourishflora", flourishfloraDictionary },
          { "Mosses", mossesDictionary },
          { "Falshrooms", falshroomsDictionary },
          { "Maremolds", maremoldsDictionary },
          { "Flesher Fungi", flesherFungiDictionary },
          { "Trees", treesDictionary },
          { "Waveskellen", waveskellenDictionary },
          { "Stranglevines", stranglevinesDictionary },
          { "Stillferns", stillfernsDictionary }
        };
        Dictionary<string, Dictionary<string, Sprite>> geomaterialsDictionary = new() {
          { "Rocks", rocksDictionary },
          { "Metallic Ores", metallicOresDictionary },
          { "Industrial Minerals", industrialMineralsDictionary },
          { "Fuel Minerals", fuelMineralsDictionary },
          { "Gem Minerals", gemMineralsDictionary },
          { "Metals", metalsDictionary },
        };
        Dictionary<string, Dictionary<string, Sprite>> equipmentDictionary = new() {
          { "Guardian Weapons", guardianWeaponsDictionary },
          { "Seeker Weapons", seekerWeaponsDictionary },
          { "Guardian Armor", guardianArmorDictionary },
          { "Seeker Armor", seekerArmorDictionary },
          { "Utilities", utilitiesDictionary },
          { "Analysis Gear", analysisGearDictionary },
          { "Seeker Boosters", seekerBoostersDictionary },
          { "Environmental Gear", environmentalGearDictionary }
        };
        Dictionary<string, Dictionary<string, Sprite>> personalItemsDictionary = new() {
          { "Accords", guardianWeaponsDictionary }
        };

        try
        {
            switch (resource.Category.Primary)
            {
                case "Organic Samples":
                    return organicSamplesDictionary[resource.Category.Secondary][resource.Rarity.GetRarityText()];
                case "Geomaterials":
                    return geomaterialsDictionary[resource.Category.Secondary][resource.Rarity.GetRarityText()];
                case "Equipment":
                    return equipmentDictionary[resource.Category.Secondary][resource.Rarity.GetRarityText()];
                case "Personal Items":
                    return personalItemsDictionary[resource.Category.Secondary][resource.Rarity.GetRarityText()];
                default:
                    break;
            }
        }
        catch (System.Exception)
        {
            Debug.LogError($"Resource dailed to be created: {resource}");
            throw;
        }
        return null;
    }

    public Sprite GetBaseResourceSprite(string resourceName)
    {
        Resource resource = new Resource(resourceName);
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
            Debug.LogError($"Resource dailed to be created: {resource}");
            throw;
        }
        return null;
    }
}