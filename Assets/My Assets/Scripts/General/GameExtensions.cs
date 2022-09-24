using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace GameExtensions
{
    public static class GameExtensions
    {
        //public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        //{
        //	return source.Shuffle(new Random());
        //}

        //public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        //{
        //	if (source == null) throw new ArgumentNullException(nameof(source));
        //	if (rng == null) throw new ArgumentNullException(nameof(rng));

        //	return source.ShuffleIterator(rng);
        //}

        //private static IEnumerable<T> ShuffleIterator<T>(
        //	this IEnumerable<T> source, Random rng)
        //{
        //	var buffer = source.ToList();
        //	for (int i = 0; i < buffer.Count; i++)
        //	{
        //		int j = rng.Next(i, buffer.Count);
        //		yield return buffer[j];

        //		buffer[j] = buffer[i];
        //	}
        //}

        //public static T GetRandom<T>(this List<T> list)
        //{
        //	return list.Shuffle().ToList()[0];
        //}

        //public static List<T> GetRandom<T>(this List<T> list, int num)
        //{
        //	return list.Shuffle().Take(num).ToList();
        //}
    }

    public static class BlightsourceExtensions
    {
        //public static Blightsource GetBlightsourceByWeight(this List<Blightsource> sources)
        //{
        //	var indexesByWeight = new List<int>();

        //	for (int i = 0; i < sources.Count; i++)
        //	{
        //		indexesByWeight.AddRange(sources[i].rarity.GetWeight(i));
        //	}

        //	return sources[indexesByWeight.PickRandom()];
        //}

        // Add:
        // ------------------------------------------------------------------------------------------
        /// <summary>
        /// Add an Resource to a List, either by adding a new Resource list element or by increasing the
        /// count of an existing one.
        /// </summary>
        /// <param name="resourceList">The Resource list to add to</param>
        /// <param name="resource">The Resource to add</param>
        public static void Add(this List<Resource> resourceList, Resource resource, bool checkForSame)
        {
            Resource same = Resource.Same(resourceList, resource, false);
            if (Resource.Same(resourceList, resource, false).Count == 0)
            {
                resourceList.Add(resource);
            }
            else
            {
                Resource.Same(resourceList, resource, false).Count += resource.Count;
            }
        }

        // Add All:
        // ------------------------------------------------------------------------------------------
        /// <summary>
        /// Add a list of Items to another List, either by adding a new Item list element or by
        /// increasing the counts of an existing one.
        /// </summary>
        /// <param name="itemList">The Item list to add to</param>
        /// <param name="itemsToAdd">The Items to add</param>
        public static void AddAll(this List<Resource> resourceList, List<Resource> resourcesToAdd)
        {
            for (int i = 0; i < resourcesToAdd.Count; i++)
            {
                resourceList.Add(resourcesToAdd[i], true);
            }
        }

        // Intersecting:
        // ------------------------------------------------------------------------------------------
        /// <summary>
        /// Get all Items in a list that are also found in a second one.
        /// </summary>
        /// <param name="sourceList1">First Item list</param>
        /// <param name="sourceList2">Second Item list</param>
        /// <returns>Items from the first list found in the second</returns>
        //public static List<Blightsource> Intersecting(this List<Blightsource> sourceList1, List<Blightsource> sourceList2)
        //{
        //	var intersectingList = new List<Blightsource>();
        //	for (int i = 0; i < sourceList2.Count; i++)
        //	{
        //		Blightsource same = Blightsource.Same(sourceList1, sourceList2[i]);
        //		if (same.count > 0)
        //		{
        //			intersectingList.Add(same);
        //		}
        //	}
        //	return intersectingList;
        //}

        // Consolidate:
        // ------------------------------------------------------------------------------------------
        /// <summary>
        /// Combines Items with of the same name, adding their counts together into a first list.
        /// </summary>
        /// <param name="sourceList">Items to consolidate</param>
        /// <returns>Consolidated list of Items</returns>
        //public static List<Blightsource> Consolidate(this List<Blightsource> sourceList)
        //{
        //	var consolidatedList = new List<Blightsource>();
        //	sourceList.ForEach(x => consolidatedList.Add(x, true));
        //	return consolidatedList;
        //}

        //// Expand:
        //// ------------------------------------------------------------------------------------------
        ///// <summary>
        ///// Takes a list of Items and expands items with counts higher than one into multiple
        ///// elements.
        ///// </summary>
        ///// <param name="l">Item list to expand</param>
        ///// <returns>A new List of Items</returns>
        //public static List<Blightsource> Expand(this List<Blightsource> l)
        //{
        //	var expandedList = new List<Blightsource>();
        //	for (int i = 0; i < l.Count; i++)
        //	{
        //		if (l[i].count > 1)
        //		{
        //			var itemCopy = Blightsource.Copy(l[i]);
        //			for (int j = 0; j < itemCopy.count; j++)
        //			{
        //				expandedList.Add(new Item(itemCopy.Name, 1));
        //			}
        //		}
        //		else if (l[i].count == 1)
        //		{
        //			expandedList.Add(new Item(l[i].Name, 1));
        //		}
        //	}
        //	return expandedList;
        //}

        //// Multiply:
        //// ------------------------------------------------------------------------------------------
        ///// <summary>
        ///// Multiplies the counts of Items in a list and return a new list.
        ///// </summary>
        ///// <param name="l">Items to multiply</param>
        ///// <param name="m">Multiplier to apply</param>
        ///// <returns>New list of Items with multiplied counts</returns>
        //public static List<Item> Multiply(this List<Item> l, int m)
        //{
        //	var copyList = Item.CopyList(l);
        //	copyList.ForEach(x => x.count *= m);
        //	return copyList;
        //}
    }

    public static class NumberExtensions
    {
        // Clamp:
        // ------------------------------------------------------------------------------------------
        /// <summary>
        /// Clamps an int between a minimum and maximum value.
        /// </summary>
        /// <param name="val">int to clamp</param>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <returns>A clamped int</returns>
        public static int Clamp(this int val, int min, int max)
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        // ClampF:
        // ------------------------------------------------------------------------------------------
        /// <summary>
        /// Clamps a float between a minimum and maximum value.
        /// </summary>
        /// <param name="val">float to clamp</param>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <returns>A clamped float</returns>
        public static float ClampF(this float val, float min, float max)
        {
            if (val.CompareTo(min) < 0f) return min;
            else if (val.CompareTo(max) > 0f) return max;
            else return val;
        }

        // Percent Chance:
        // ------------------------------------------------------------------------------------------
        /// <summary>
        /// An easy way to check if a percentage chance passes.
        /// </summary>
        /// <param name="val">The percentage chance</param>
        /// <returns>True if the check passes, False otherwise</returns>
        public static bool PercentChance(this int val)
        {
            Random rnd = new Random();
            return (rnd.Next(0, 100) < val);
        }

        /// <summary>
        /// An easy way to check if a percentage chance passes.
        /// </summary>
        /// <param name="val">The percentage chance</param>
        /// <returns>True if the check passes, False otherwise</returns>
        public static bool PercentChance(this float val)
        {
            Random rnd = new();
            if (val == 0)
            {
                return false;
            }
            return rnd.NextDouble() < (val / 100d);
        }
    }

    public static class ListExtensions
    {
        // Pick Random:
        // ------------------------------------------------------------------------------------------
        /// <summary>
        /// Returns a random element from a list. If the list is null or empty, returns the default
        /// value for that type.
        /// </summary>
        /// <param name="source">The list to select from</param>
        /// <returns>A random element or default value</returns>
        public static T PickRandom<T>(this List<T> source)
        {
            System.Random rnd = new System.Random();
            if (source == null || source.Count == 0)
            {
                return default(T);
            }
            return source[rnd.Next(0, source.Count)];
        }

        // Pick Random Group:
        // ------------------------------------------------------------------------------------------
        /// <summary>
        /// Pick a selection of random elements from a list.
        /// </summary>
        /// <param name="source">The list to select from</param>
        /// <param name="n">The number of elements to select</param>
        /// <returns>A list with selected elements</returns>
        public static List<T> PickRandomGroup<T>(this List<T> source, int n)
        {
            var selectFrom = source;
            var selected = new List<T>();
            int nLeft = n;
            while (nLeft > 0 && selectFrom.Count > 0)
            {
                T randomSelection = selectFrom.PickRandom();
                selected.Add(randomSelection);
                selectFrom.Remove(randomSelection);
                nLeft--;
            }
            return selected;
        }

        // Shuffle:
        // ------------------------------------------------------------------------------------------
        /// <summary>
        /// Shuffle the elements of a list.
        /// </summary>
        /// <param name="list">The same list, only shuffled.</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rnd = new();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}

public enum Menu
{
    MainMenu, GameMenu
}

public enum Gender
{
    Male, Female, Nonbinary, None
}