using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard 
{
    private readonly BoardGenerator _boardGenerator;

    private GameTile[,] _board;

    public int x;
    public int y;
    public GameBoard(BoardGenerator boardGenerator)
    {
        _boardGenerator = boardGenerator;
    }

    public void Init()
    {
        x = 8;
        y = 8;
        _board = _boardGenerator.GenerateBoard(x, y);
    }

    public GameTile GetTile(int x, int y)
    {
        return _board[x, y];
    }
}
