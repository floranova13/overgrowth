using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PossessionsController : Singleton<PossessionsController>
{
    public Resource selectedResource;

    public TMP_Text ResourceNameText;
    public TMP_Text ResourceCategoryText;
    public TMP_Text ResourceSubcategoryText;
    public TMP_Text ResourceRarityText;
    public TMP_Text ResourceCountText;
    public TMP_Text ResourcePriceText;
    public TMP_Text ResourceDescriptionText;


    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    private void Reset()
    {
        selectedResource = null;
    }

    public void SelectResource(Resource resource)
    {
        selectedResource = resource;
        SetResourceInfo();
    }

    private void SetResourceInfo()
    {
        ResourceNameText.text = selectedResource.Name;
        ResourceCategoryText.text = selectedResource.Category.Primary;
        ResourceSubcategoryText.text = selectedResource.Category.Secondary;
        ResourceRarityText.text = selectedResource.Rarity.GetRarityText();
        ResourceCountText.text = $"Count: {selectedResource.Count}";
        ResourcePriceText.text = $"Base Price: {selectedResource.Price}";
        ResourceDescriptionText.text = selectedResource.Description;
    }
}
