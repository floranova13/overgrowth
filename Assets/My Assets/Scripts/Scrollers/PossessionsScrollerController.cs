using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using System.Linq;

public class PossessionsScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{
    /// <summary>
    /// Internal representation of our data. Note that the scroller will never see
    /// this, so it separates the data from the layout using MVC principles.
    /// </summary>
    private List<ResourceCell> _data;

    /// <summary>
    /// This is our scroller we will be a delegate for
    /// </summary>
    public EnhancedScroller scroller;
    public ResourceCellView ResourceCellViewPrefab;

    public void RefreshScroller()
    {
        if (scroller.gameObject.activeInHierarchy)
        {
            StartCoroutine(LoadData());
        }
    }

    public void ClearScroller() {
      scroller.Delegate = this;
      scroller.ClearAll();
    }

    void Start()
    {
    }

    /// <summary>
    /// Populates the data with a lot of records
    /// </summary>
    private IEnumerator LoadData()
    {
        float betweenDelay = 0.05f;
        _data = new List<ResourceCell>();

        for (int i = 0; i < GameSave.s.resources.Count; i++)
        {
            _data.Add(new ResourceCell() { resource = GameSave.s.resources[i] });
            yield return new WaitForSeconds(betweenDelay);
        }

        scroller.Delegate = this;
        yield return new WaitForSeconds(betweenDelay);
        scroller.ReloadData();
    }

    #region EnhancedScroller Handlers

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        if (_data == null) return 0;
        return _data.Count;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 90f;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        ResourceCellView cellView = scroller.GetCellView(ResourceCellViewPrefab) as ResourceCellView;
        cellView.name = "[Resource] " + _data[dataIndex].resource.Name;
        return cellView;
    }

    #endregion
}