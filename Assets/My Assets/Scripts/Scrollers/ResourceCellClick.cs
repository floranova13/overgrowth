using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResourceCellClick : Selectable
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (BlockScript.Unblocked())
        {
            CellViewResource cellView = GetComponent<CellViewResource>();
            // Play Sound Effect
            //MasterAudio.PlaySound("ButtonConfirm", 1f);
            PossessionsController.Instance.SelectResource(cellView.resource);
        }
    }
}
