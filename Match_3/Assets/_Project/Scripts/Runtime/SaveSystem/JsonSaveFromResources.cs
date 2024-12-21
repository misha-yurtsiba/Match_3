using UnityEngine;
using System.IO;

public class JsonSaveFromResources : ISaveStarategy
{
    private readonly string _saveDirectiry;

    public JsonSaveFromResources(string saveDirectiry = "Assets/_Project/Resources/LevelData")
    {
        _saveDirectiry = saveDirectiry;
    }

    public TData Load<TData>(string key)
    {
        string path = Path.Combine("LevelData", $"Level_{key}");

        string json = Resources.Load<TextAsset>(path).text;

        TData levelData = JsonUtility.FromJson<TData>(json);

        return levelData;

    }

    public void Save<TData>(TData levelData, string key)
    {
        string path = Path.Combine(_saveDirectiry, $"Level_{key}.json");

        string json = JsonUtility.ToJson(levelData);
        
        File.WriteAllText(path, json);
        Debug.Log(path);
    }
}
