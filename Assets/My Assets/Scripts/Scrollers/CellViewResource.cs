using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EnhancedUI.EnhancedScroller;
using TMPro;

/// <summary>
/// This is the view for the rows
/// </summary>
public class CellViewResource : CellView
{
    /// <summary>
    /// An internal reference to the row data. We could have just
    /// used the base CellView's class member _data, but that would
    /// require us to cast it each time a row data field is needed.
    /// By referencing the row data, we can save some time accessing
    /// the fields.
    /// </summary>
    private ResourceCellData _resourceData;

    /// <summary>
    /// Links to the UI fields
    /// </summary>
    public TMP_Text nameText;
    public Image resourceImage;

    /// <summary>
    /// Override of the base class's SetData function. This links the data
    /// and updates the UI
    /// </summary>
    /// <param name="data"></param>
    public override void SetData(Data data)
    {
        // call the base SetData to link to the underlying _data
        base.SetData(data);

        // cast the data as rowData and store the reference
        _resourceData = data as ResourceCellData;

        // update the UI with the data fields
        nameText.text = _resourceData.resource.Name;
        resourceImage.sprite = ResourceUtilities.Instance.GetBaseResourceSprite(_resourceData.resource.Name); 
    }
}