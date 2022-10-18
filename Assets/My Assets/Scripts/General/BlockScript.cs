using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public static class BlockScript
{

    // Variables: 
    // ------------------------------------------------------------------------------------------

    /// <summary>
    /// The current number of input blocks active.
    /// </summary>
    private static List<string> blocks;
    private static bool initialized;

    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // X:
    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------

    // Initialize: 
    // ------------------------------------------------------------------------------------------
    public static void Initialize()
    {
        blocks = new List<string>();
        initialized = true;
    }

    // Unblocked: 
    // ------------------------------------------------------------------------------------------
    /// <summary>
    /// Checks if input is blocked. Optional Input checks for a specific block name.
    /// </summary>
    /// <returns>True if input unblocked, False otherwise.</returns>
    public static bool Unblocked(string blockName = "")
    {
        if (!initialized) { Initialize(); }
        if (blockName != "")
        {
            bool unblockedCheck = blocks.Where(x => x == blockName).ToList().Count == 0;
            if (!unblockedCheck)
            {
                Debug.Log($"Input Blocked, Block '{blockName}' Present");
            }
            return unblockedCheck;
        }
        if (blocks.Count != 0)
        {
            Debug.Log("Input Blocked");
        }
        return blocks.Count == 0;
    }
    /// <summary>
    /// Checks if input is blocked by any of the named blocks.
    /// </summary>
    /// <returns>True if input unblocked, False otherwise.</returns>
    public static bool Unblocked(List<string> blockNames)
    {
        bool unblockedCheck = blocks.Where(x => blockNames.Contains(x)).ToList().Count == 0;
        if (!unblockedCheck)
        {
            Debug.Log("Input Blocked [" + string.Join(", ", blockNames) + "]");
        }
        return unblockedCheck;
        //return blocks.Where(x => blockNames.Contains(x)).ToList().Count == 0;
    }
    // Add: 
    // ------------------------------------------------------------------------------------------
    public static void Add(string blockName)
    {
        if (!initialized) { Initialize(); }
        blocks.Add(blockName);
    }
    public static void Add(List<string> blockNames)
    {
        if (!initialized) { Initialize(); }
        blocks = blocks.Concat(blockNames).ToList();
    }
    // Remove: 
    // ------------------------------------------------------------------------------------------
    public static void Remove(string blockName, int times = 1)
    {
        if (!initialized) { Initialize(); }
        if (times != 1)
        {
            for (int i = 0; i < times; i++)
            {
                Remove(blockName);
            }
        }
        blocks.Remove(blockName);
    }
    public static void Remove(List<string> blockNames)
    {
        if (!initialized) { Initialize(); }
        blockNames.ForEach(x => Remove(x));
    }

    // : 
    // ------------------------------------------------------------------------------------------

}
