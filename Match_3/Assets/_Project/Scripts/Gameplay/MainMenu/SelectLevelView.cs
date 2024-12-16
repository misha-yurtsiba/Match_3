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
    [SerializeField] private LevelButtonView _levelButtonPrefab;
    [SerializeField] private GameObject _content;

    private List<LevelButtonPresenter> _buttons = new List<LevelButtonPresenter>();
    private TextAsset[] _levelDatas;

    private ISceneChanger _sceneChanger;

    [Inject]
    private void Construct(ISceneChanger sceneChanger)
    {
        _sceneChanger = sceneChanger;
    }
    public void SpawnLevelBattons()
    {
        _levelDatas = Resources.LoadAll<TextAsset>("LevelData");

        for (int i = 0; i < _levelDatas.Length; i++)
        {
            LevelData data = JsonUtility.FromJson<LevelData>(_levelDatas[i].text);

            LevelButtonView levelButton = Instantiate(_levelButtonPrefab,_content.transform,_content.transform);
            LevelButtonModel levelButtonModel = new LevelButtonModel(data.Level, false);
            LevelButtonPresenter levelButtonPresenter = new LevelButtonPresenter(levelButtonModel, levelButton, _sceneChanger);
            
            levelButton.Init(levelButtonPresenter);
            levelButtonPresenter.ShowLevel();

            _buttons.Add(levelButtonPresenter);  
        }

        gameObject.SetActive(false);

        foreach(var levelData in _levelDatas)
            Resources.UnloadAsset(levelData);
    }
}
