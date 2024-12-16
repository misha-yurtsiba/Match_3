using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUIController : MonoBehaviour
{
    [SerializeField] private CompleteLevelView _completeLevelView;

    public void Init()
    {
        _completeLevelView.Init();
        _completeLevelView.gameObject.SetActive(false);
    }

    public void ActiveCompleteLevelPanel()
    {
        _completeLevelView.gameObject.SetActive(true);
        _completeLevelView.StartActiveAnimation();
    }
}
