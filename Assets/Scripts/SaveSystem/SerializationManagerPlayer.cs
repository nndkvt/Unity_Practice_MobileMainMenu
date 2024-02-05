using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializationManagerPlayer : SerializationManager
{
    private static string SavePlayerData = "player.save";

    public static bool Save(SaveData saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (!Directory.Exists(SavePath))
        {
            Directory.CreateDirectory(SavePath);
        }

        string path = SavePath + "/" + SavePlayerData;

        FileStream file = File.Create(path);
        formatter.Serialize(file, saveData);
        file.Close();

        Debug.Log($"Saved to {path}");

        return true;
    }

    public static SaveData Load()
    {
        if (!File.Exists(SavePath + "/" + SavePlayerData))
        {
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream file = File.Open(SavePath + "/" + SavePlayerData, FileMode.Open);

        try
        {
            SaveData save = (SaveData)formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogErrorFormat("Something went wrong while loading save {0}", SavePath + "/" + SavePlayerData);
            file.Close();
            return null;
        }
    }
}
