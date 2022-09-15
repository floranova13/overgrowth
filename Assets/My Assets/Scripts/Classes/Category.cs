using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Category
{
    private readonly string category;
    private readonly string subcategory;
    public string Categories { get => $"{category}, {subcategory}"; }
    public string Primary { get => category; }
    public string Secondary { get => subcategory; }

    public Category(string primary, string secondary)
    {
        category = primary;
        subcategory = secondary;
    }

}
