using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator
{
    private readonly GameTile _gameTilePrefab;
    private readonly FruitsPrefabs _fruitsPrefabs;

    public BoardGenerator(GameTile gameTilePrefab, FruitsPrefabs fruitsPrefabs)
    {
        _gameTilePrefab = gameTilePrefab;
        _fruitsPrefabs = fruitsPrefabs;
    }
    public GameTile[,] GenerateBoard(int width, int height)
    {
        GameTile[,] gameTiles = new GameTile[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int randomIndex = Random.Range(0, _fruitsPrefabs.prefabList.Count);
                Fruit fruit = Object.Instantiate(_fruitsPrefabs.prefabList[randomIndex], new Vector3(i,j,-0.5f), Quaternion.identity);

                gameTiles[i, j] = Object.Instantiate(_gameTilePrefab, new Vector3(i,j,0), Quaternion.identity);
                gameTiles[i, j].curentItem = fruit;
            }
        }

        return gameTiles;
    }
}
