using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator
{
    private readonly GameTile _gameTilePrefab;
    private readonly FruitFactory _fruitFactory;
    private readonly ObstacleFactory _obstacleFactory;

    private ItemFactory _itemFactory;
    public BoardGenerator(GameTile gameTilePrefab, FruitFactory fruitFactory, ObstacleFactory obstacleFactory)
    {
        _gameTilePrefab = gameTilePrefab;
        _fruitFactory = fruitFactory;
        _obstacleFactory = obstacleFactory;

    }
    public GameTile[,] GenerateBoard(int width, int height, Vector3 offset)
    {
        GameTile[,] gameTiles = new GameTile[width, height];

        _itemFactory = _obstacleFactory;

        for(int i = 0; i < height; i++)
        {
            SpawnOneItem(0, i, offset, gameTiles);
        }

        _itemFactory = _fruitFactory;

        for (int i = 1; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                SpawnOneItem(i,j,offset, gameTiles);
            }
        }

        return gameTiles;
    }

    private void SpawnOneItem(int i, int j, Vector3 offset, GameTile[,] gameTiles)
    {
        Item item = _itemFactory.Create(new Vector3(i, j, 0) + offset, gameTiles);

        GameTile gameTile = Object.Instantiate(_gameTilePrefab, new Vector3(i, j, 0), Quaternion.identity);
        item.SetTile(gameTile);

        gameTile.curentItem = item;
        gameTile.xPos = i;
        gameTile.yPos = j;

        gameTiles[i, j] = gameTile;
    }

}
