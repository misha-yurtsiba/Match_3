using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public abstract class Item : MonoBehaviour
{
    private GameTile _curentTile;
    public GameTile CurentTile => _curentTile;
    public void SetTile(GameTile newTile) => _curentTile = newTile;

    virtual async public UniTask DestroyItemAsync()
    {
        Tween tween = transform.DOScale(Vector3.zero, 0.2f);
        await tween.AsyncWaitForCompletion();

        Destroy(gameObject);
    }
}

