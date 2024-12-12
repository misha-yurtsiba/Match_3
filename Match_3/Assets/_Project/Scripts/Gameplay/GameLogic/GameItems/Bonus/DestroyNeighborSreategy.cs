using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNeighborSreategy : BonusStrategy
{
    private readonly ItemDestroyer _itemDestroyer;
    private readonly List<Vector2Int> _directions;
    public DestroyNeighborSreategy(ItemDestroyer itemDestroyer)
    {
        _directions = new List<Vector2Int>()
        {
            Vector2Int.down,
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.left,
            new Vector2Int(1, 1),
            new Vector2Int(-1, -1),
            new Vector2Int(1, -1),
            new Vector2Int(-1, 1)
        };

        _itemDestroyer = itemDestroyer;
    }
    public override async UniTaskVoid Execute(GameBoard gameBoard, Item item)
    {
        foreach (Vector2Int direction in _directions)
        {
            int newX = item.CurentTile.xPos + direction.x;
            int newY = item.CurentTile.yPos + direction.y;

            if (newX >= 0 && newX < gameBoard.x && newY >= 0 && newY < gameBoard.y && (gameBoard._board[newX, newY].curentItem is Item neighborItem))
            {
                _itemDestroyer.DestroyOneItem(neighborItem);
                await UniTask.DelayFrame(1, cancellationToken: neighborItem.GetCancellationTokenOnDestroy());
            }
        }
    }
}
