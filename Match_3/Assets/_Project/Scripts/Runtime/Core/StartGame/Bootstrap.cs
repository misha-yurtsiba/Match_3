using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class Bootstrap : MonoBehaviour
{
    private ISceneChanger _sceneChanger;
    private ISaveStarategy _saveStarStrategy;

    [Inject]
    private void Construct(ISceneChanger sceneChanger)
    {
        _sceneChanger = sceneChanger;
        _saveStarStrategy = new JsonSaveFromFile();
    }

    private void Start()
    {
        Application.targetFrameRate = 60;

        CreateGameData();

        _sceneChanger.ChangeScene(Scenes.Menu);
    }

    private void CreateGameData()
    {
        GameData gameData = _saveStarStrategy.Load<GameData>(nameof(GameData));

        TextAsset[] _levelDatas = Resources.LoadAll<TextAsset>("LevelData");
       
        gameData ??= new GameData();

        for (int i = 0; i < _levelDatas.Length; i++)
        {
            if(!gameData.completedLevels.ContainsKey(i + 1))
            {
                gameData.completedLevels.Add(i + 1, false);
            }
        }

        gameData.isFirstRun = false;

        _saveStarStrategy.Save(gameData, nameof(GameData));

        Resources.UnloadUnusedAssets();
    }
}