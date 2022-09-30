using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public Canvas PossessionsCanvas;

    public Button PossessionsMenuButton;


    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    private void Reset()
    {
        selectedResource = null;
        ResourceNameText.text = "";
        ResourceCategoryText.text = "";
        ResourceSubcategoryText.text = "";
        ResourceRarityText.text = "";
        ResourceCountText.text = "";
        ResourcePriceText.text = "";
        ResourceDescriptionText.text = "";
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

    public void OpenMenu()
    {
        PossessionsMenuButton.interactable = false;
        Reset();
        PossessionsCanvas.gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        PossessionsCanvas.gameObject.SetActive(false);
    }
}
