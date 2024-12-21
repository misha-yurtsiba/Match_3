using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;


public class LoadingCurtain : MonoBehaviour
{
    [SerializeField] private Image _loadCurtain;

    private void Start()
    {
        _loadCurtain.gameObject.SetActive(true);
    }
    public async UniTask ActiveLoadingCurtain()
    {
        _loadCurtain.gameObject.SetActive(true);
        Tween tween = _loadCurtain.DOFade(1, 1);

        await tween.AsyncWaitForCompletion();
    }

    public void DisactiveLoadingCurtain(Action callback)
    {
        _loadCurtain.DOFade(0, 1)
           .OnComplete(() => 
           {
               callback?.Invoke();
               _loadCurtain.gameObject.SetActive(false);
           });
    }
}
