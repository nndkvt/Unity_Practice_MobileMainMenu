using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializationManagerRewards : SerializationManager
{
    private static string SavePlayerRewards = "rewards.save";

    public static bool Save(SaveDataRewards saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (!Directory.Exists(SavePath))
        {
            Directory.CreateDirectory(SavePath);
        }

        string path = SavePath + "/" + SavePlayerRewards;

        FileStream file = File.Create(path);
        formatter.Serialize(file, saveData);
        file.Close();

        return true;
    }

    public static SaveDataRewards Load()
    {
        if (!File.Exists(SavePath + "/" + SavePlayerRewards))
        {
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream file = File.Open(SavePath + "/" + SavePlayerRewards, FileMode.Open);

        try
        {
            SaveDataRewards save = (SaveDataRewards)formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogErrorFormat("Something went wrong while loading save {0}", SavePath + "/" + SavePlayerRewards);
            file.Close();
            return null;
        }
    }
}
