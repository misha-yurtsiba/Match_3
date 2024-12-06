using Cysharp.Threading.Tasks;

public class DestroyColumnStrategy : BonusStrategy
{
    public override async UniTaskVoid Execute(GameBoard gameBoard, Item item)
    {
        for (int i = 1; i < gameBoard.y; i++)
        {
            int y = item.CurentTile.yPos;

            if (y + i < gameBoard.x)
                gameBoard._board[item.CurentTile.xPos, y + i].curentItem?.DestroyItemAsync().Forget();

            if (y - i >= 0)
                gameBoard._board[item.CurentTile.xPos, y - i].curentItem?.DestroyItemAsync().Forget();

            await UniTask.DelayFrame(2, cancellationToken: item.GetCancellationTokenOnDestroy());
        }
    }
}
