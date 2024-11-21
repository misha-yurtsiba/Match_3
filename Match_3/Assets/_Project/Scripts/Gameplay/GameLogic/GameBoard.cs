using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard 
{
    private readonly BoardGenerator _boardGenerator;

    private GameTile[,] _board;

    public GameBoard(BoardGenerator boardGenerator)
    {
        _boardGenerator = boardGenerator;
    }

    public void Init()
    {
        _board = _boardGenerator.GenerateBoard(8, 8);
    }
}
