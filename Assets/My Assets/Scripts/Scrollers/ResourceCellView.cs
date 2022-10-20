using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using TMPro;
using System;
using System.Linq;

public class ResourceCellView : EnhancedScrollerCellView
{
    public TMP_Text NameText;
    public Image CellImage;
    public Image ResourceImage;

    public static Color32 CommonColor = new(224, 224, 224, 255);
    public static Color32 UncommonColor = new(84, 214, 134, 255);
    public static Color32 RareColor = new(85, 192, 214, 255);
    public static Color32 WondrousColor = new(182, 84, 214, 255);

    [HideInInspector] public Resource resource;

    public void SetData(ResourceCell data)
    {
        Dictionary<string, Color> colorDict = new() {
        { "Common", CommonColor },
        { "Uncommon", UncommonColor },
        { "Rare", RareColor },
        { "Wondrous", WondrousColor },
      };
        // cast the data as rowData and store the reference
        resource = data.resource;
        CellImage.color = colorDict[resource.Rarity.GetRarityText()];
        NameText.text = resource.Name;
        ResourceImage.sprite = ResourceUtilities.Instance.GetBaseResourceSprite(resource.Name);
    }
}
