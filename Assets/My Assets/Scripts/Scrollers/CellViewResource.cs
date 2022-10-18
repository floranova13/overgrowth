using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EnhancedUI.EnhancedScroller;
using TMPro;
using System;
using System.Collections.Generic;

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

    /// <summary>
    /// Links to the UI fields
    /// </summary>
    public TMP_Text NameText;
    public Image CellImage;
    public Image ResourceImage;
    public Resource resource;

    public static Color32 CommonColor = new(224, 224, 224, 255);
    public static Color32 UncommonColor = new(84, 214, 134, 255);
    public static Color32 RareColor = new(85, 192, 214, 255);
    public static Color32 WondrousColor = new(182, 84, 214, 255);

    /// <summary>
    /// Override of the base class's SetData function. This links the data
    /// and updates the UI
    /// </summary>
    /// <param name="data"></param>
    public override void SetData(Data data)
    {
        Dictionary<String, Color> colorDict = new() {
        { "Common", CommonColor },
        { "Uncommon", UncommonColor },
        { "Rare", RareColor },
        { "Wondrous", WondrousColor },
      };
        // call the base SetData to link to the underlying _data
        base.SetData(data);
        // cast the data as rowData and store the reference
        resource = (data as ResourceCellData).resource;
        CellImage.color = colorDict[resource.Rarity.GetRarityText()];
        NameText.text = resource.Name;
        ResourceImage.sprite = ResourceUtilities.Instance.GetBaseResourceSprite(resource.Name);
    }
}