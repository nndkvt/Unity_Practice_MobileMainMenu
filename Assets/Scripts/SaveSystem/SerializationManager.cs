using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public abstract class SerializationManager
{
    protected static string SavePath = Application.persistentDataPath + "/save";
}
