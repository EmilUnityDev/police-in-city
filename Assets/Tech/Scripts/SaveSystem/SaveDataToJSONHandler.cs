using UnityEngine;
using System.IO;

public static class SaveDataToJSONHandler
{
    private static string _path = Application.persistentDataPath + "/saveData.json";

    public static SaveData Load()
    {
        if (!File.Exists(_path)) return null;
        string json = File.ReadAllText(_path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        return data;
    }
    public static void Save(SaveData data)
    {
        if (data == null) return;
        string dataJson = JsonUtility.ToJson(data);

        File.WriteAllText(_path, dataJson);
    }

    public static void Clear()
    {
        if (!File.Exists(_path)) return;
        File.Delete(_path);
    }
}