using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
[System.Serializable]
public static class TextAssetOperations
{
	// ------------------------------------------------------------------------------------------
	// ------------------------------------------------------------------------------------------
	// :
	// ------------------------------------------------------------------------------------------
	// ------------------------------------------------------------------------------------------

	// Read Text:
	// ------------------------------------------------------------------------------------------
	/// <summary>
	/// Read text of text asset and return as a list of strings. Uses 'File.ReadAllLines'.
	/// </summary>
	public static List<string> ReadText(string path)
	{
		return File.ReadAllLines(path).ToList();
	}

	// Read Text Asset:
	// ------------------------------------------------------------------------------------------
	/// <summary>
	/// Read text of text asset and return as a list of strings. Unlike with the similar
	/// 'ReadText' method, this method ignores 'commented out' or lines less than
	/// two chars in length. Uses 'Resources.Load'.
	/// </summary>
	public static List<string> ReadTextAsset(string path, bool ignoreLines = true)
	{
		TextAsset asset = Resources.Load<TextAsset>(path);
		if (asset == null)
		{
			Debug.Log(path + "Asset is Null");

			return new List<string>();
		}
		var assetLines = asset.text.Split('\n').ToList();
		if (ignoreLines)
		{
			assetLines = assetLines
							.Where(x => x.Length > 2 && x.Substring(0, 2) != "//")
							.Select(x => x.Trim())
							.ToList();
		}
		else
		{
			assetLines = assetLines.Select(x => x.Trim()).ToList();
		}
		return assetLines;
	}

	// X:
	// ------------------------------------------------------------------------------------------

	// X:
	// ------------------------------------------------------------------------------------------
}