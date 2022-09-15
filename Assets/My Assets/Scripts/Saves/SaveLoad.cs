using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveLoad
{
    public static string path = @$"{Application.persistentDataPath}\Save";
    public static string savePath = @$"{path}\GameSave.sav";

    public static bool DoesSaveExist()
    {
        return !Directory.Exists(savePath) && File.Exists(savePath);
    }

    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // Save:
    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------

    public static void Save()
    {
        Debug.Log("Trying to Save");
        GameSave save = GameSave.s;
        BinaryFormatter bf = new();
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        FileStream file = File.Create(savePath);
        bf.Serialize(file, save);
        file.Close();
    }

    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // Load:
    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------

    public static void Load()
    {
        Debug.Log("Trying to Load");
        if (File.Exists(@Application.persistentDataPath + @"\Save\GameSave.sav"))
        {
            Debug.Log("Save exists.");
            BinaryFormatter bf = new();
            FileStream file = File.Open(
                @Application.persistentDataPath + @"\Save\GameSave.sav", FileMode.Open);
            GameSave.s = (GameSave)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            Debug.Log("Save didn't exist, so it is being created.");
            GameSave.s = new GameSave();
        }
    }

    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // Delete Save:
    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------

    public static void DeleteSave()
    {
        string path = @$"{Application.persistentDataPath}\Save\GameSave.sav";
        if (DoesSaveExist())
        {
            File.Delete(path);
        }
        Debug.Log("Deleted Save");
    }
}