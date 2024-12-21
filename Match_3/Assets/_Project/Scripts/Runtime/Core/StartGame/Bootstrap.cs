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

        GameData gameData = _saveStarStrategy.Load<GameData>(nameof(GameData));

        if (gameData == null)
            CreateGameData();

        _sceneChanger.ChangeScene(Scenes.Menu);
    }

    private void CreateGameData()
    {
        TextAsset[] _levelDatas = Resources.LoadAll<TextAsset>("LevelData");
        GameData gameData = new GameData();

        for (int i = 0; i < _levelDatas.Length; i++)
        {
            gameData.completedLevels.Add(i + 1, false);
        }

        gameData.isFirstRun = false;

        _saveStarStrategy.Save(gameData, nameof(GameData));
    }
}
