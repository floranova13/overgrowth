using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeekerController : Singleton<SeekerController>
{
    public Location selectedLocation;
    // public Contract selectedContract;

    public ScrollerController LocationsScrollerController;
    public ScrollerController ContractsScrollerController;
    public GameObject LocationsScroller;
    public GameObject ContractsScroller;
    public Canvas LocationInfoCanvas;
    public Canvas ContractInfoCanvas;

    public TMP_Text LocationNameText;
    public TMP_Text LocationTerritoryText;
    public TMP_Text LocationThreatText;
    public TMP_Text LocationDescriptionText;
    public TMP_Text MerchantInventoryText;
    public TMP_Text ContractNameText;
    public TMP_Text ContractLocationText;
    public TMP_Text ContractDifficultyText;
    public TMP_Text ContractRewardText;
    public TMP_Text ContractDescriptionText;

    public Canvas SeekerCanvas;

    public Button SeekerMenuButton;


    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    private void Reset()
    {
        selectedLocation = null;
        // selectedContract = null;
        LocationNameText.text = "";
        LocationTerritoryText.text = "";
        LocationThreatText.text = "";
        LocationDescriptionText.text = "";
        ContractNameText.text = "";
        ContractLocationText.text = "";
        ContractDifficultyText.text = "";
        ContractRewardText.text = "";
        ContractDescriptionText.text = "";
    }

    public void SelectLocation(Location location)
    {
        selectedLocation = location;
        SetLocationInfo();
    }

    private void SetLocationInfo()
    {
        LocationNameText.text = selectedLocation.Name;
        LocationTerritoryText.text = selectedLocation.Territory;
        LocationThreatText.text = $"Threat: {selectedLocation.Threat}";
        LocationDescriptionText.text = selectedLocation.Description;
        ContractNameText.text = "";
        ContractLocationText.text = "";
        ContractDifficultyText.text = "";
        ContractRewardText.text = "";
        ContractDescriptionText.text = "";
    }

    // public void SelectContract(Contract contract)
    // {
    //     selectedContract = contract;
    //     SetContractInfo();
    // }

    private void SetContractInfo()
    {
        ContractNameText.text = "";
        ContractLocationText.text = "";
        ContractDifficultyText.text = "";
        ContractRewardText.text = "";
        ContractDescriptionText.text = "";
    }

    public void OpenMenu()
    {
        SeekerMenuButton.interactable = false;
        Reset();
        ContractsScroller.SetActive(false);
        ContractInfoCanvas.gameObject.SetActive(false);
        LocationsScroller.SetActive(true);
        LocationInfoCanvas.gameObject.SetActive(true);
        SeekerCanvas.gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        SeekerCanvas.gameObject.SetActive(false);
    }
}
