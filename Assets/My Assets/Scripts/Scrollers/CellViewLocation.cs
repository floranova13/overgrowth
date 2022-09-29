using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EnhancedUI.EnhancedScroller;
using TMPro;

/// <summary>
/// This is the view for the rows
/// </summary>
public class CellViewLocation : CellView
{
    /// <summary>
    /// An internal reference to the row data. We could have just
    /// used the base CellView's class member _data, but that would
    /// require us to cast it each time a location data field is needed.
    /// By referencing the row data, we can save some time accessing
    /// the fields.
    /// </summary>
    private LocationCellData _locationData;

    /// <summary>
    /// Links to the UI fields
    /// </summary>
    public TMP_Text locationText;
    public Image locationImage;

    /// <summary>
    /// Override of the base class's SetData function. This links the data
    /// and updates the UI
    /// </summary>
    /// <param name="data"></param>
    public override void SetData(Data data)
    {
        // call the base SetData to link to the underlying _data
        base.SetData(data);

        // cast the data as LocationCellData and store the reference
        _locationData = data as LocationCellData;

        // update the UI with the data fields
        locationText.text = _locationData.location.Name;
        locationImage.sprite = null; // TODO: SET CORRECT LOCATION SPRITE
    }
}