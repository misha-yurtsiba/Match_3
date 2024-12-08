using Cysharp.Threading.Tasks;
public abstract class BonusStrategy
{
    public abstract UniTaskVoid Execute(GameBoard gameBoard,Item item);

    protected void DestroyOneItem(Item item, GameBoard gameBoard)
    {
        if (item == null) return;

        if (item is Bonus bonus)
            bonus.DestroyAction(gameBoard);
        else
            item.DestroyItemAsync().Forget();
    }
}
