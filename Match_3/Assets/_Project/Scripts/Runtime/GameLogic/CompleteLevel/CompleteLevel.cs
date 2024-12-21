using System;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class CompleteLevel : IDisposable
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly ISaveStarategy _saveStarategy = new JsonSaveFromFile();
    private readonly IItemProvider _itemProvider;

    private bool _isGameOver = false;
    public CompleteLevel(GameplayStateMachine gameplayStateMachine, IItemProvider itemProvider)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _itemProvider = itemProvider;

        _itemProvider.OnValueChanged += CheckGameOver;
    }

    public void Dispose()
    {
        _itemProvider.OnValueChanged -= CheckGameOver;
    }

    private void CheckGameOver(int itemCount)
    {
        if (itemCount == 0)
        {
            _isGameOver = true;
            SaveLevelCompletion();
        }
    }
    private void SaveLevelCompletion()
    {
        GameData gameData = _saveStarategy.Load<GameData>(nameof(GameData));
        gameData.completedLevels[SelectedLevel.Level] = true;
        _saveStarategy.Save(gameData, nameof(GameData));
    }
    public bool IsLevelComplet() => _isGameOver;
}