using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using DG.Tweening;

public class FruitMover
{
    private readonly GameBoard _gameBoard;

    private List<UniTask> _tasks;

    private Vector3 _fillingOffset = new Vector3(0, -0.3f, 0);
    public FruitMover(GameBoard gameBoard)
    {
        _gameBoard = gameBoard;

        _tasks = new List<UniTask>();
    }
    public async UniTask SwapFruitsAsync(Fruit fruit1, Fruit fruit2, CancellationToken cancellationToken)
    {
        _tasks.Clear();

        GameTile gameTile1 = fruit1.CurentTile;
        GameTile gameTile2 = fruit2.CurentTile;

        fruit1.SetTile(gameTile2);
        gameTile2.curentItem = fruit1;

        fruit2.SetTile(gameTile1);
        gameTile1.curentItem = fruit2;

        _tasks.Add(SwapOneFruit(fruit1, gameTile2.transform.position, 0.2f, cancellationToken));
        _tasks.Add(SwapOneFruit(fruit2, gameTile1.transform.position, 0.2f, cancellationToken));

        await UniTask.WhenAll(_tasks).AttachExternalCancellation(cancellationToken);
    }

    public async UniTask MoveFruitDown(Dictionary<IMoveble, GameTile> movedFruits, float spead,CancellationToken cancellationToken)
    {
        _tasks.Clear();

        foreach (KeyValuePair<IMoveble, GameTile> pair in movedFruits)
        {
            _tasks.Add(MoveOneFruit(pair.Key, pair.Value.transform.position, spead, cancellationToken));
        }
        await UniTask.WhenAll(_tasks).AttachExternalCancellation(cancellationToken);

    }

    private async UniTask MoveOneFruit(IMoveble fruit, Vector3 position, float spead, CancellationToken cancellationToken)
    {
        Tween tween = fruit.Transform.DOMove(position + _gameBoard.itemOffset + _fillingOffset, spead * 2).SetSpeedBased().SetEase(Ease.InQuad);
        await tween.AsyncWaitForCompletion().AsUniTask().AttachExternalCancellation(cancellationToken);
        
        tween = fruit.Transform.DOMove(position + _gameBoard.itemOffset, spead * 2).SetSpeedBased().SetEase(Ease.Linear);
        UniTask uniTask = tween.AsyncWaitForCompletion().AsUniTask();
        await uniTask.AttachExternalCancellation(cancellationToken);
    }

    private async UniTask SwapOneFruit(Fruit fruit, Vector3 position, float time, CancellationToken cancellationToken)
    {
        for(float i = 0; i < 1; i += Time.deltaTime / time)
        {
            fruit.transform.position = Vector3.Lerp(fruit.transform.position, position + _gameBoard.itemOffset, i);
            if (cancellationToken.IsCancellationRequested) return;
            await UniTask.Yield(cancellationToken);
        }
    }
}
