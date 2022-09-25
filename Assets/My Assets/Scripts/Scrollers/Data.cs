using UnityEngine;
using System.Collections;

/// <summary>
/// This set of classes store information about the different rows.
/// The base data class has no members, but is useful for polymorphism.
/// </summary>
public class Data
{
}

/// <summary>
/// This is the data that simple lists will use. These lists do not need click functionality.
/// </summary>
public class TextCellData : Data
{
    /// <summary>
    /// The string to display.
    /// </summary>
    public string label;
}

/// <summary>
/// This is the data that will store information about a Resource.
/// </summary>
public class ResourceCellData : Data
{
    /// <summary>
    /// The Resource.
    /// </summary>
    public Resource resource;
}

/// <summary>
/// This is the data that will store information about a Merchant.
/// </summary>
public class MerchantCellData : Data
{
    /// <summary>
    /// The Merchant.
    /// </summary>
    public Merchant merchant;
}

/// <summary>
/// This is the data that will store information about a Location.
/// </summary>
public class LocationCellData : Data
{
    /// <summary>
    /// The Location.
    /// </summary>
    public Location location;
}
