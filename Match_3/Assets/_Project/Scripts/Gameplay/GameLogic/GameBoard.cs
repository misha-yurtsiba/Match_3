using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard 
{
    private readonly BoardGenerator _boardGenerator;
    private readonly GameTile _gameTilePrefab;
    private readonly FruitsPrefabs _fruitsPrefabs;

    private GameTile[,] _board;

    public GameBoard(GameTile gameTilePrefab, FruitsPrefabs fruitsPrefabs)
    {
        _gameTilePrefab = gameTilePrefab;
        _fruitsPrefabs = fruitsPrefabs;

        _boardGenerator = new BoardGenerator(_gameTilePrefab, _fruitsPrefabs);
    }

    public void Init()
    {
        _board = _boardGenerator.GenerateBoard(8, 8);
    }
}
