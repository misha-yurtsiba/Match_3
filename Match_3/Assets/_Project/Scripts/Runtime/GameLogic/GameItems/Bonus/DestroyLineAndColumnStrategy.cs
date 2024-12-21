using Cysharp.Threading.Tasks;
using UnityEngine;

public class DestroyLineAndColumnStrategy : BonusStrategy
{
    private readonly ItemDestroyer _itemDestroyer;

    public DestroyLineAndColumnStrategy(ItemDestroyer itemDestroyer)
    {
        _itemDestroyer = itemDestroyer;
    }
    public override async UniTaskVoid Execute(GameBoard gameBoard, Item item)
    {
        for (int i = 1; i < Mathf.Max(gameBoard.x,gameBoard.y); i++)
        {
            int x = item.CurentTile.xPos;

            if (x + i < gameBoard.x)
                _itemDestroyer.DestroyOneItem(gameBoard._board[x + i, item.CurentTile.yPos].curentItem);

            if (x - i >= 0)
                _itemDestroyer.DestroyOneItem(gameBoard._board[x - i, item.CurentTile.yPos].curentItem);

            if (x + i < gameBoard.x)
                _itemDestroyer.DestroyOneItem(gameBoard._board[item.CurentTile.xPos, x + i].curentItem);

            if (x - i >= 0)
                _itemDestroyer.DestroyOneItem(gameBoard._board[item.CurentTile.xPos, x - i].curentItem);

            await UniTask.DelayFrame(1, cancellationToken: item.GetCancellationTokenOnDestroy());
        }
    }
}
