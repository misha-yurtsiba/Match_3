using Cysharp.Threading.Tasks;

public class DestroyLineStrategy : BonusStrategy
{
    private readonly ItemDestroyer _itemDestroyer;

    public DestroyLineStrategy(ItemDestroyer itemDestroyer)
    {
        _itemDestroyer = itemDestroyer;
    }
    public override async UniTaskVoid Execute(GameBoard gameBoard,Item item)
    {
        for(int i = 1; i < gameBoard.x; i++)
        {
            int x = item.CurentTile.xPos;

            if (x + i < gameBoard.x)
                _itemDestroyer.DestroyOneItem(gameBoard._board[x + i, item.CurentTile.yPos].curentItem);

            if (x - i >= 0)
                _itemDestroyer.DestroyOneItem(gameBoard._board[x - i, item.CurentTile.yPos].curentItem);

            await UniTask.DelayFrame(2, cancellationToken: item.GetCancellationTokenOnDestroy());
        }
    }
}
