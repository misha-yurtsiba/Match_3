using System;
using System.Collections.Generic;
using UnityEngine;
public interface IItemProvider
{
    public int Count { get; }
    public void AddItem(Item item);
    public void RemoveItem(Item item);

    public event Action<int> OnValueChanged;
} 

public class ObstecleProvider : IItemProvider
{
    private List<Item> _items = new List<Item>();

    public event Action<int> OnValueChanged;

    public int Count
    {
        get => _items.Count;
    }

    public void AddItem(Item item)
    {
        _items.Add(item);
        OnValueChanged?.Invoke(_items.Count);
    }

    public void RemoveItem(Item item)
    {
        _items.Remove(item);
        OnValueChanged?.Invoke(_items.Count);
    }

}
public class GameBoard 
{
    private readonly BoardGenerator _boardGenerator;
    public readonly Vector3 itemOffset = new Vector3(0, 0, -0.5f);

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
