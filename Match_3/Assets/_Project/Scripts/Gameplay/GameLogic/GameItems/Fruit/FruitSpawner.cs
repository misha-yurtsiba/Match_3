using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using DG.Tweening;

public class FruitSpawner
{
    private readonly ItemFactory _itemFactory;
    private readonly BonusFactory _bonusFactory;
    private readonly GameBoard _gameBoard;

    private List<GameTile> _emptyTiles = new List<GameTile>();
    private List<UniTask> _tasks = new List<UniTask>();

    private float _spawnChance = 0.025f;

    public FruitSpawner(FruitFactory fruitFactory, GameBoard gameBoard, BonusFactory bonusFactory)
    {
        _itemFactory = fruitFactory;
        _gameBoard = gameBoard;
        _bonusFactory = bonusFactory;
    }

    public async UniTask SpawnDeletedFruits(CancellationToken token)
    {
        _tasks.Clear();
        _emptyTiles.Clear();

        for (int i = 0; i < _gameBoard.x; i++)
        {
            for (int j = 0; j < _gameBoard.y; j++)
            {
                if (_gameBoard.GetTile(i, j).curentItem == null)
                    _emptyTiles.Add(_gameBoard.GetTile(i, j));
            }
        }

        foreach (GameTile gameTile in _emptyTiles)
        {
            Item item;
            float randomNum = Random.value;

            if(1 - _spawnChance <= randomNum)
                item = _bonusFactory.Create(new Vector3(gameTile.xPos, gameTile.yPos, 0) + _gameBoard.itemOffset, _gameBoard._board, 0);
            else
                item = _itemFactory.Create(new Vector3(gameTile.xPos, gameTile.yPos, 0) + _gameBoard.itemOffset, _gameBoard._board, -1);

            gameTile.curentItem = item;
            item.SetTile(gameTile);

            _tasks.Add(FruitSpawnAnimation(item, token));

            await UniTask.DelayFrame(2,cancellationToken: token);
        }

        await UniTask.WhenAll(_tasks).AttachExternalCancellation(token);
    }

    private async UniTask FruitSpawnAnimation(Item fruit, CancellationToken cancellationToken)
    {
        Vector3 scale = fruit.transform.localScale;
        fruit.transform.localScale = Vector3.zero;

        Tween tween = fruit.transform.DOScale(scale, 0.2f);

        await tween.AsyncWaitForCompletion();
    }
}
