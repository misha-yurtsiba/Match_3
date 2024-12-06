using Cysharp.Threading.Tasks;

public class DestroyLineStrategy : BonusStrategy
{
    public override async UniTaskVoid Execute(GameBoard gameBoard,Item item)
    {
        for(int i = 1; i < gameBoard.x; i++)
        {
            int x = item.CurentTile.xPos;

            if (x + i < gameBoard.x)
                gameBoard._board[x + i, item.CurentTile.yPos].curentItem?.DestroyItemAsync().Forget();

            if (x - i >= 0)
                gameBoard._board[x - i, item.CurentTile.yPos].curentItem?.DestroyItemAsync().Forget();

            await UniTask.DelayFrame(2, cancellationToken: item.GetCancellationTokenOnDestroy());
        }
    }
}
