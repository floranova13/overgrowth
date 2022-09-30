using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarketController : Singleton<MarketController>
{
    public Merchant selectedMerchant;

    public TMP_Text MerchantNameText;
    public TMP_Text MerchantCategoryText;
    public TMP_Text MerchantSubcategoryText;
    public TMP_Text MerchantRarityText;
    public TMP_Text MerchantInventoryText;
    public TMP_Text MerchantCostMarginsText;
    public TMP_Text MerchantDescriptionText;

    public Canvas MarketCanvas;

    public Button MarketMenuButton;


    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    private void Reset()
    {
        selectedMerchant = null;
        MerchantNameText.text = "";
        MerchantCategoryText.text = "";
        MerchantSubcategoryText.text = "";
        MerchantRarityText.text = "";
        MerchantInventoryText.text = "";
        MerchantCostMarginsText.text = "";
        MerchantDescriptionText.text = "";
    }

    public void SelectMerchant(Merchant merchant)
    {
        selectedMerchant = merchant;
        SetMerchantInfo();
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

    public void OpenMenu()
    {
        MarketMenuButton.interactable = false;
        Reset();
        MarketCanvas.gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        MarketCanvas.gameObject.SetActive(false);
    }
}
