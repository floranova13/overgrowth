using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeekerController : Singleton<SeekerController>
{
    public Location selectedLocation;
    public Contract selectedContract;

    public ScrollerController SeekerScrollerController;

    public GameObject SeekerCanvas;
    public GameObject LocationInfoCanvas;
    public GameObject ContractInfoCanvas;

    public TMP_Text LocationNameText;
    public TMP_Text LocationTerritoryText;
    public TMP_Text LocationThreatText;
    public TMP_Text LocationResourcesTitleText;
    public TMP_Text LocationResourcesText;
    public TMP_Text LocationDescriptionText;
    public TMP_Text ContractNameText;
    public TMP_Text ContractLocationText;
    public TMP_Text ContractDifficultyText;
    public TMP_Text ContractRewardText;
    public TMP_Text ContractDescriptionText;



    // Start is called before the first frame update
    void Start()
    {
    }

    private void Reset()
    {
        selectedLocation = null;
        selectedContract = null;
        ClearLocationInfo();
        ClearContractInfo();
    }

    public void ClearLocationInfo()
    {
        LocationNameText.text = "";
        LocationTerritoryText.text = "";
        LocationThreatText.text = "";
        LocationResourcesTitleText.text = "";
        LocationResourcesText.text = "";
        LocationDescriptionText.text = "";
    }

    public void ClearContractInfo()
    {
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
        LocationResourcesTitleText.text = "Discovered Resources";
        LocationResourcesText.text = ""; // TODO: SHOW RESOURCES BASED ON RESEARCH AND EXPERIENCE
        LocationDescriptionText.text = selectedLocation.Description;
    }

    public void SelectContract(Contract contract)
    {
        selectedContract = contract;
        SetContractInfo();
    }

    private void SetContractInfo()
    {
        ContractNameText.text = selectedContract.Name;
        ContractLocationText.text = $"Location: {selectedContract.Location.Name}";
        ContractDifficultyText.text = $"DIFFICULTY"; // TODO: CALCULATE DIFFICULTY BASED ON EXPERIENCE AND RESEARCH
        ContractRewardText.text = "REWARDS"; // TODO: CALCULATE DIFFICULTY BASED ON EXPERIENCE AND RESEARCH
        ContractDescriptionText.text = selectedContract.Description;
    }

    public void OpenMenu()
    {
        Debug.Log("SeekerController - OpenMenu| Opening Seeker Menu");
        SeekerCanvas.SetActive(true);
        Reset();
        LocationInfoCanvas.SetActive(true);
        ContractInfoCanvas.SetActive(false);
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
            SeekerScrollerController.RefreshScroller("Locations");
        }
        else
        {
            SeekerScrollerController.RefreshScroller("");
            yield return new WaitForSeconds(delay);
            SeekerCanvas.SetActive(false);
        }
    }
}
