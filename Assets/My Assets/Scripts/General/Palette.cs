using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Palette
{
	public static Color shade0 = new Color(96, 40, 112); // #602870
	public static Color shade1 = new Color(157, 114, 169); // #9D72A9
	public static Color shade2 = new Color(125, 73, 140); // #7D498C
	public static Color shade3 = new Color(69, 16, 84); // #451054
	public static Color shade4 = new Color(44, 2, 56); // #2C0238

	public enum Shade
	{
		Base, Lightest, Lighter, Darker, Darkest
	}

	public static Dictionary<Shade, Color> p = new Dictionary<Shade, Color>()
	{
		{ Shade.Base, shade0 },
		{ Shade.Lightest, shade1 },
		{ Shade.Lighter, shade2 },
		{ Shade.Darker, shade3 },
		{ Shade.Darkest, shade4 },
	};
}