using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
public class Bonus : Item, IMoveble
{
    public ObjectPool<BonusExplosion> bonusExplosionPool;

    private BonusStrategy _bonusStrategy;
    public Transform Transform
    {
        get => transform;
        set => transform.position = value.position;
    }

    public override UniTask DestroyItemAsync()
    {
        PlayDestroyParticle(bonusExplosionPool.Get());

        return base.DestroyItemAsync();
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