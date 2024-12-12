using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

public abstract class SaveStarategy<TData>
{
    public abstract TData Load(int level);
    public abstract void Save(TData data);
}

public class JsonSaveStrayegy : SaveStarategy<LevelData>
{
    private readonly string _saveDirectiry = Path.Combine(Application.persistentDataPath, "Level");

    public override LevelData Load(int level)
    {
        string path = Path.Combine("LevelData", $"Level_{level}");

        string json = Resources.Load<TextAsset>(path).text;

        LevelData levelData = JsonUtility.FromJson<LevelData>(json);

        return levelData;

    }

    public override void Save(LevelData levelData)
    {
        string path = Path.Combine("Assets/_Project/Resources", $"Level_{levelData.Level}.json");

        string json = JsonUtility.ToJson(levelData);
        
        File.WriteAllText(path, json);
        Debug.Log(path);
    }
}
