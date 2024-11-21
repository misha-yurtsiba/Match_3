using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator
{
    private readonly GameTile _gameTilePrefab;
    private readonly FruitFactory _fruitFactory;

    private ItemFactory _itemFactory;
    public BoardGenerator(GameTile gameTilePrefab, FruitFactory fruitFactory)
    {
        _gameTilePrefab = gameTilePrefab;
        _fruitFactory = fruitFactory;

        _itemFactory = _fruitFactory;
    }
    public GameTile[,] GenerateBoard(int width, int height)
    {
        GameTile[,] gameTiles = new GameTile[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Item item = _itemFactory.Create(new Vector3(i, j, -0.5f), gameTiles);

                gameTiles[i, j] = Object.Instantiate(_gameTilePrefab, new Vector3(i,j,0), Quaternion.identity);
                gameTiles[i, j].curentItem = item;
            }
        }

        return gameTiles;
    }


}
