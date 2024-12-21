using System.Collections;
using UnityEngine;
public class Bonus : Item, IMoveble
{
    private BonusStrategy _bonusStrategy;
    public Transform Transform
    {
        get => transform;
        set => transform.position = value.position;
    }
    public void SetBonus(BonusStrategy bonusStrategy)
    {
        _bonusStrategy = bonusStrategy;
    }

    public override void DestroyAction(GameBoard gameBoard)
    {
        _bonusStrategy.Execute(gameBoard,this);

        _itemDestroyer.DestroyOneItem(this);
    }
}