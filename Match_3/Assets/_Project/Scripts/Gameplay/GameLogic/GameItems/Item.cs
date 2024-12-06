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
        if(CurentTile.curentItem == null) return;
        else _curentTile.curentItem = null;
        Tween tween = transform.DOScale(Vector3.zero, 0.2f);
        await tween.AsyncWaitForCompletion().AsUniTask().AttachExternalCancellation(gameObject.GetCancellationTokenOnDestroy());

        Destroy(gameObject);
    }

    virtual public void DestroyAction(GameBoard gameBoard)
    {
        
    }
}

