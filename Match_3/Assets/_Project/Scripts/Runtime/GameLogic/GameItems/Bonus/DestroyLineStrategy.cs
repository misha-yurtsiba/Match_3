public class DestroyLineStrategy : BonusStrategy
{
    private readonly ItemDestroyer _itemDestroyer;

    public DestroyLineStrategy(ItemDestroyer itemDestroyer)
    {
        _itemDestroyer = itemDestroyer;
    }
    public override void Execute(GameBoard gameBoard,Item item)
    {
        for(int i = 1; i < gameBoard.x; i++)
        {
            int x = item.CurentTile.xPos;

            if (x + i < gameBoard.x)
                _itemDestroyer.DestroyOneItem(gameBoard._board[x + i, item.CurentTile.yPos].curentItem);

            if (x - i >= 0)
                _itemDestroyer.DestroyOneItem(gameBoard._board[x - i, item.CurentTile.yPos].curentItem);
        }
    }
}
