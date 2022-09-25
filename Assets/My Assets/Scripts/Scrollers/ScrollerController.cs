using UnityEngine;
using System.Collections;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;

public class ScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{
    /// <summary>
    /// Internal representation of our data. Note that the scroller will never see
    /// this, so it separates the data from the layout using MVC principles.
    /// </summary>
    private List<Data> _data;

    /// <summary>
    /// This is our scroller we will be a delegate for
    /// </summary>
    public EnhancedScroller scroller;

    public EnhancedScrollerCellView resourceCellViewPrefab;
    public EnhancedScrollerCellView merchantCellViewPrefab;
    public EnhancedScrollerCellView locationCellViewPrefab;
    public EnhancedScrollerCellView textCellViewPrefab;

    /// <summary>
    /// The base path to the resources folder where sprites are located
    /// </summary>
    public string resourcePath;

    void Start()
    {
        // tell the scroller that this script will be its delegate
        scroller.Delegate = this;

        LoadData();
    }

    /// <summary>
    /// Populates the data with a lot of records
    /// </summary>
    private void LoadData()
    {
        // create some data
        // note we are using different data class fields for the header, row, and footer rows. This works due to polymorphism.

        _data = new List<Data>();

        _data.Add(new TextData() { label = "Test Cell" });
        _data.Add(new TextData() { label = "Test Cell" });
        _data.Add(new TextData() { label = "Test Cell" });

        // tell the scroller to reload now that we have the data
        scroller.ReloadData();
    }

    #region EnhancedScroller Handlers

    /// <summary>
    /// This tells the scroller the number of cells that should have room allocated. This should be the length of your data array.
    /// </summary>
    /// <param name="scroller">The scroller that is requesting the data size</param>
    /// <returns>The number of cells</returns>
    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        // in this example, we just pass the number of our data elements
        return _data.Count;
    }

    /// <summary>
    /// This tells the scroller what the size of a given cell will be. Cells can be any size and do not have
    /// to be uniform. For vertical scrollers the cell size will be the height. For horizontal scrollers the
    /// cell size will be the width.
    /// </summary>
    /// <param name="scroller">The scroller requesting the cell size</param>
    /// <param name="dataIndex">The index of the data that the scroller is requesting</param>
    /// <returns>The size of the cell</returns>
    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        // we will determine the cell height based on what kind of row it is


        if (_data[dataIndex] is ResourceCellData)
        {
            return 90f;
        }
        else if (_data[dataIndex] is MerchantCellData)
        {
            return 100f;
        }
        else if (_data[dataIndex] is LocationCellData)
        {
            return 100f;
        }
        else
        {
            return 70f;
        }
    }

    /// <summary>
    /// Gets the cell to be displayed. You can have numerous cell types, allowing variety in your list.
    /// Some examples of this would be headers, footers, and other grouping cells.
    /// </summary>
    /// <param name="scroller">The scroller requesting the cell</param>
    /// <param name="dataIndex">The index of the data that the scroller is requesting</param>
    /// <param name="cellIndex">The index of the list. This will likely be different from the dataIndex if the scroller is looping</param>
    /// <returns>The cell for the scroller to use</returns>
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        CellView cellView;

        // determin what cell view to get based on the type of the data row

        if (_data[dataIndex] is ResourceCellData)
        {
            // get a resource cell prefab from the scroller, recycling old cells if possible
            cellView = scroller.GetCellView(resourceCellViewPrefab) as CellViewResource;

            // optional for clarity: set the cell's name to something to indicate this is a Resource item
            cellView.name = "[Resource] " + (_data[dataIndex] as ResourceCellData).Name;
        }
        else if (_data[dataIndex] is MerchantCellData)
        {
            // get a merchant cell prefab from the scroller, recycling old cells if possible
            cellView = scroller.GetCellView(merchantCellViewPrefab) as CellViewMerchant;

            // optional for clarity: set the cell's name to something to indicate this is a Merchant item
            cellView.name = "[Merchant] " + (_data[dataIndex] as MerchantCellData).Name;
        }
        else if (_data[dataIndex] is LocationCellData)
        {
            // get a location cell prefab from the scroller, recycling old cells if possible
            cellView = scroller.GetCellView(locationCellViewPrefab) as CellViewLocation;

            // optional for clarity: set the cell's name to something to indicate this is a Location item
            cellView.name = "[Location] " + (_data[dataIndex] as LocationCellData).Name;
        }
        else
        {
            // get a text cell prefab from the scroller, recycling old cells if possible
            cellView = scroller.GetCellView(textCellViewPrefab) as CellViewText;

            // optional for clarity: set the cell's name to something to indicate this is a text item
            cellView.name = "[Text]";
        }

        // set the cell view's data. We can do this because we declared a single SetData function
        // in the CellView base class, saving us from having to call this for each cell type
        cellView.SetData(_data[dataIndex]);

        // return the cellView to the scroller
        return cellView;
    }

    #endregion
}