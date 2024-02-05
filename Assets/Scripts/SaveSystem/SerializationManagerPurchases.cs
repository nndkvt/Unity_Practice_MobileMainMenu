using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializationManagerPurchases : SerializationManager
{
    private static string SavePlayerPurchases = "purchases.save";

    public static bool Save(SaveDataPurchases saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (!Directory.Exists(SavePath))
        {
            Directory.CreateDirectory(SavePath);
        }

        string path = SavePath + "/" + SavePlayerPurchases;

        FileStream file = File.Create(path);
        formatter.Serialize(file, saveData);
        file.Close();

        return true;
    }

    public static SaveDataPurchases Load()
    {
        if (!File.Exists(SavePath + "/" + SavePlayerPurchases))
        {
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream file = File.Open(SavePath + "/" + SavePlayerPurchases, FileMode.Open);

        try
        {
            SaveDataPurchases save = (SaveDataPurchases)formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogErrorFormat("Something went wrong while loading save {0}", SavePath + "/" + SavePlayerPurchases);
            file.Close();
            return null;
        }
    }
}
