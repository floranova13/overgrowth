using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarketController : Singleton<MarketController>
{
    [HideInInspector] public Merchant selectedMerchant;
    [HideInInspector] public Resource selectedResource;

    public ScrollerController MarketScrollerController;
    public GameObject MerchantInfoCanvas;
    public GameObject InventoryInfoCanvas;

    public TMP_Text MerchantNameText;
    public TMP_Text MerchantCategoryText;
    public TMP_Text MerchantSubcategoryText;
    public TMP_Text MerchantRarityText;
    public TMP_Text MerchantInventoryText;
    public TMP_Text MerchantCostMarginsText;
    public TMP_Text MerchantDescriptionText;
    public TMP_Text ResourceNameText;
    public TMP_Text ResourceCategoryText;
    public TMP_Text ResourceSubcategoryText;
    public TMP_Text ResourceRarityText;
    public TMP_Text ResourceCountText;
    public TMP_Text ResourcePriceText;
    public TMP_Text ResourceDescriptionText;

    public GameObject MarketCanvas;

    public Button MarketMenuButton;


    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    private void Reset()
    {
        selectedMerchant = null;
        selectedResource = null;
        ClearMerchantInfo();
        ClearResourceInfo();
    }

    public void ClearMerchantInfo()
    {
        MerchantNameText.text = "";
        MerchantCategoryText.text = "";
        MerchantSubcategoryText.text = "";
        MerchantRarityText.text = "";
        MerchantInventoryText.text = "";
        MerchantCostMarginsText.text = "";
        MerchantDescriptionText.text = "";
    }

    public void ClearResourceInfo()
    {
        ResourceNameText.text = "";
        ResourceCategoryText.text = "";
        ResourceSubcategoryText.text = "";
        ResourceRarityText.text = "";
        ResourceCountText.text = "";
        ResourcePriceText.text = "";
        ResourceDescriptionText.text = "";
    }

    public void SelectMerchant(Merchant merchant)
    {
        selectedMerchant = merchant;
        SetMerchantInfo();
    }

    public void ShowMerchantStock()
    {
        if (selectedMerchant != null)
        {
            ClearMerchantInfo();
            ClearResourceInfo();
            MerchantInfoCanvas.gameObject.SetActive(false);
            InventoryInfoCanvas.gameObject.SetActive(true);
            MarketScrollerController.RefreshScroller("Merchant Inventory");
        }
    }

    private void SetMerchantInfo()
    {
        MerchantNameText.text = selectedMerchant.Name;
        MerchantCategoryText.text = selectedMerchant.Category.Primary;
        MerchantSubcategoryText.text = selectedMerchant.Category.Secondary;
        MerchantRarityText.text = selectedMerchant.Rarity.GetRarityText();
        MerchantInventoryText.text = selectedMerchant.GetInventoryText(); ;
        MerchantCostMarginsText.text = selectedMerchant.GetProficiencyText();
        MerchantDescriptionText.text = selectedMerchant.Description;
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
        Debug.Log("MarketController - OpenMenu| Opening Market Menu");
        MarketMenuButton.interactable = false;
        MarketCanvas.SetActive(true);
        Reset();
        InventoryInfoCanvas.SetActive(false);
        MerchantInfoCanvas.SetActive(true);
        StartCoroutine(OpenOrCloseCoroutine());
    }

    public void CloseMenu()
    {
        StartCoroutine(OpenOrCloseCoroutine(false));
    }

    private IEnumerator OpenOrCloseCoroutine(bool open = true)
    {
        float delay = 0.1f;
        if (open)
        {
            yield return new WaitForSeconds(delay);
            MarketScrollerController.RefreshScroller("Merchants");
        }
        else
        {
            MarketScrollerController.RefreshScroller(newType: "");
            yield return new WaitForSeconds(delay);
            MarketCanvas.SetActive(false);
        }
    }
}
