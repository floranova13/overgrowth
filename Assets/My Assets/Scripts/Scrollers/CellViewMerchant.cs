using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EnhancedUI.EnhancedScroller;
using TMPro;

/// <summary>
/// This is the view for the rows
/// </summary>
public class CellViewMerchant : CellView
{
    /// <summary>
    /// An internal reference to the row data. We could have just
    /// used the base CellView's class member _data, but that would
    /// require us to cast it each time a row data field is needed.
    /// By referencing the row data, we can save some time accessing
    /// the fields.
    /// </summary>
    private MerchantCellData _merchantData;

    /// <summary>
    /// Links to the UI fields
    /// </summary>
    public TMP_Text NameText;
    public TMP_Text SpecializationText;
    public Image MerchantImage;
    [HideInInspector] public Merchant merchant;

    /// <summary>
    /// Override of the base class's SetData function. This links the data
    /// and updates the UI
    /// </summary>
    /// <param name="data"></param>
    public override void SetData(Data data)
    {
        // call the base SetData to link to the underlying _data
        base.SetData(data);

        // cast the data as MerchantCellData and store the reference
        _merchantData = data as MerchantCellData;
        merchant = _merchantData.merchant;
        // Debug.Log($"CellViewMerchant - SetData| {merchant.Name}");
        // Debug.Log($"CellViewMerchant - SetData| {merchant.Category.Secondary}");

        NameText.text = merchant.Citizen.Name;
        SpecializationText.text = merchant.Name;
        MerchantImage.sprite = null; // TODO: SET CORRECT MERCHANT SPRITE
    }
}