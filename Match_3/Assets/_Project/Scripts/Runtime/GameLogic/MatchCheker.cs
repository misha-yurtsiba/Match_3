using System.Collections.Generic;

public class MatchCheker
{
    private readonly GameBoard _gameBoard;
    private readonly List<Fruit> _fruits;
    public MatchCheker(GameBoard gameBoard)
    {
        _gameBoard = gameBoard;

        _fruits = new List<Fruit>();
    }
    public List<Fruit> FindMatch()
    {
        _fruits.Clear();

        for(int i = 0; i < _gameBoard.x; i++)
        {
            for(int j = 0; j < _gameBoard.y; j++)
            {
                Fruit curentFruit = _gameBoard.GetTile(i, j).curentItem as Fruit;

                if (i > 0 && i < _gameBoard.x - 1 && curentFruit != null)
                {
                    Fruit leftFruit = _gameBoard.GetTile(i -1, j).curentItem as Fruit;
                    Fruit rightFruit = _gameBoard.GetTile(i + 1, j).curentItem as Fruit;

                    if(leftFruit != null && rightFruit != null && curentFruit.Index == leftFruit.Index && curentFruit.Index == rightFruit.Index)
                    {
                        curentFruit.isMatched = true;
                        leftFruit.isMatched = true;
                        rightFruit.isMatched = true;
                    }
                }

                if (j > 0 && j < _gameBoard.y - 1 && curentFruit != null)
                {
                    Fruit upFruit = _gameBoard.GetTile(i, j + 1).curentItem as Fruit;
                    Fruit downFruit = _gameBoard.GetTile(i, j - 1).curentItem as Fruit;

                    if (upFruit != null && downFruit != null && curentFruit.Index == upFruit.Index && curentFruit.Index == downFruit.Index)
                    {
                        curentFruit.isMatched = true;
                        upFruit.isMatched = true;
                        downFruit.isMatched = true;
                    }
                }
            }
        }

        for(int i = 0; i < _gameBoard.x; i++)
        {
            for(int j = 0; j < _gameBoard.y; j++)
            {
                Fruit fruit = _gameBoard.GetTile(i, j).curentItem as Fruit;

                if(fruit != null && fruit.isMatched == true)
                    _fruits.Add(fruit);
            }
        }
                
        return _fruits;
    }
}
