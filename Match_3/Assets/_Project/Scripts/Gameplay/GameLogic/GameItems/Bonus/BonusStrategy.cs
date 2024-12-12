using Cysharp.Threading.Tasks;
public abstract class BonusStrategy
{
    public abstract UniTaskVoid Execute(GameBoard gameBoard,Item item);

}
