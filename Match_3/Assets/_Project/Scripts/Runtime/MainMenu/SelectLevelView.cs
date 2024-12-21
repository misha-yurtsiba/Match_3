using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

public class SelectedLevel
{
    public static int Level;
}
public class SelectLevelView : MonoBehaviour
{
    public Button closeButton;

    [SerializeField] private LevelButtonView _levelButtonPrefab;
    [SerializeField] private GameObject _content;

    private List<LevelButtonController> _buttons = new List<LevelButtonController>();

    private ISceneChanger _sceneChanger;
    private ISaveStarategy _saveStrategy = new JsonSaveFromFile();

    private bool _canUnlockNextLevel;

    [Inject]
    private void Construct(ISceneChanger sceneChanger)
    {
        _sceneChanger = sceneChanger;

        _canUnlockNextLevel = true;
    }
    public void SpawnLevelBattons()
    {
        GameData gameData = _saveStrategy.Load<GameData>(nameof(GameData));

        for (int i = 0; i < gameData.completedLevels.Count; i++)
        {
            LevelButtonView levelButton = Instantiate(_levelButtonPrefab,_content.transform,_content.transform);
            LevelButtonModel levelButtonModel = new LevelButtonModel(i + 1, false);
            LevelButtonController levelButtonController = new LevelButtonController(levelButtonModel, levelButton, _sceneChanger);
            
            levelButton.Init(levelButtonController, !_canUnlockNextLevel);
            levelButtonController.ShowLevel();

            _canUnlockNextLevel = gameData.completedLevels[i +1];
            Debug.Log(_canUnlockNextLevel);

            _buttons.Add(levelButtonController);
        }

        gameObject.SetActive(false);
    }
}
