using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PossessionsController : Singleton<PossessionsController>
{
    public Resource selectedResource;
    public ScrollerController ScrollerController;

    public TMP_Text ResourceNameText;
    public TMP_Text ResourceCategoryText;
    public TMP_Text ResourceSubcategoryText;
    public TMP_Text ResourceRarityText;
    public TMP_Text ResourceCountText;
    public TMP_Text ResourcePriceText;
    public TMP_Text ResourceDescriptionText;

    public GameObject PossessionsCanvas;

    public Button PossessionsMenuButton;


    // Start is called before the first frame update
    void Start()
    {
    }

    private void Reset()
    {
        selectedResource = null;
        ClearResourceInfo();
    }

    private void ClearResourceInfo()
    {
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
        Debug.Log("PossessionsController - OpenMenu| Opening Possessions Menu");
        PossessionsCanvas.SetActive(true);
        Reset();
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
            ScrollerController.RefreshScroller("Resources");
        }
        else
        {
            ScrollerController.RefreshScroller("");
            yield return new WaitForSeconds(delay);
            PossessionsCanvas.SetActive(false);
        }
    }
}
