using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingCurtain : MonoBehaviour
{
    [SerializeField] private Image _loadCurtain;

    private void Start()
    {
        _loadCurtain.gameObject.SetActive(true);
    }
    public void ActiveLoadingCurtain(Action callback)
    {
        _loadCurtain.gameObject.SetActive(true);
        _loadCurtain.DOFade(1,1)
            .OnComplete(() => callback?.Invoke());
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
