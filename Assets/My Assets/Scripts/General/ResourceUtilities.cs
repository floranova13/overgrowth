using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GameExtensions;

public class ResourceUtilities : Singleton<ResourceUtilities>
{
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
    // Get Item Sprite: 
    // ------------------------------------------------------------------------------------------
    public static Sprite GetResourceSprite(string name)
    {


        return null;
    }
}