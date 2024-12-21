using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class JsonSaveFromFile : ISaveStarategy
{
    public TData Load<TData>(string key)
    {
        string path = Path.Combine(Application.persistentDataPath, $"{key}.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            TData levelData = JsonConvert.DeserializeObject<TData>(json);

            return levelData;
        }
        else
        {
            return default;
        }
    }
    public void Save<TData>(TData levelData, string key)
    {
        string path = Path.Combine(Application.persistentDataPath, $"{key}.json");

        string json = JsonConvert.SerializeObject(levelData);

        File.WriteAllText(path, json);
        Debug.Log(path);
    }
}
