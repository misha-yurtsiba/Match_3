using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class FruitMover
{
    private readonly GameBoard _gameBoard;

    private List<UniTask> _tasks;

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

        _tasks.Add(MoveOneFruit(fruit1, gameTile2.transform.position, 0.2f, cancellationToken));
        _tasks.Add(MoveOneFruit(fruit2, gameTile1.transform.position, 0.2f, cancellationToken));

        await UniTask.WhenAll(_tasks);
    }


    public async UniTask MoveFruitDown(Dictionary<Fruit, GameTile> movedFruits, float spead,CancellationToken cancellationToken)
    {
        _tasks.Clear();

        foreach (KeyValuePair<Fruit, GameTile> pair in movedFruits)
        {
            _tasks.Add(MoveOneFruit(pair.Key, pair.Value.transform.position, spead, cancellationToken));
        }
        await UniTask.WhenAll(_tasks);

    }

    private async UniTask MoveOneFruit(Fruit fruit, Vector3 position, float time, CancellationToken cancellationToken)
    {
        for(float i = 0; i < 1; i += Time.deltaTime / time)
        {
            fruit.transform.position = Vector3.Lerp(fruit.transform.position, position + _gameBoard.itemOffset, i);
            await UniTask.Yield(cancellationToken);
        }
    }
}
