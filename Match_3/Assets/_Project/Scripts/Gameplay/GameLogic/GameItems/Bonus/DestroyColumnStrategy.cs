using Cysharp.Threading.Tasks;

public class DestroyColumnStrategy : BonusStrategy
{
    private readonly ItemDestroyer _itemDestroyer;

    public DestroyColumnStrategy(ItemDestroyer itemDestroyer)
    {
        _itemDestroyer = itemDestroyer;
    }
    public override async UniTaskVoid Execute(GameBoard gameBoard, Item item)
    {
        for (int i = 1; i < gameBoard.y; i++)
        {
            int y = item.CurentTile.yPos;

            if (y + i < gameBoard.x)
                _itemDestroyer.DestroyOneItem(gameBoard._board[item.CurentTile.xPos, y + i].curentItem);

            if (y - i >= 0)
                _itemDestroyer.DestroyOneItem(gameBoard._board[item.CurentTile.xPos, y - i].curentItem);

            await UniTask.DelayFrame(2, cancellationToken: item.GetCancellationTokenOnDestroy());
        }
    }
}
