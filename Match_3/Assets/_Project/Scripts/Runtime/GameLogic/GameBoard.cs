using UnityEngine;
public class GameBoard 
{
    public readonly Vector3 itemOffset = new Vector3(0, 0, -0.5f);

    private readonly BoardGenerator _boardGenerator;

    public GameTile[,] _board;

    public int x;
    public int y;

    public GameBoard(BoardGenerator boardGenerator)
    {
        _boardGenerator = boardGenerator;
    }

    public void Init(LevelData levelData)
    {
        _board = _boardGenerator.GenerateBoardFromFile(levelData,itemOffset);

        x = levelData.width;
        y = levelData.height;
    }

    public GameTile GetTile(int x, int y)
    {
        return _board[x, y];
    }
}
