using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusFactory : ItemFactory
{
    private readonly BonusPrefabs _bonusPrefabs;
    private readonly ItemDestroyer _itemDestroyer;
    private readonly ObjectPool<BonusExplosion> _bounsExplosionPool;
    private readonly IReadOnlyList<BonusStrategy> _bonusStrategies;
    public BonusFactory(ItemDestroyer itemDestroyer, ObjectPool<BonusExplosion> bounsExplosionPool)
    {
        _itemDestroyer = itemDestroyer;
        _bounsExplosionPool = bounsExplosionPool;

        _bonusPrefabs = Resources.Load<BonusPrefabs>("Configs/BonusPrefabsConfig");

        _bonusStrategies = new List<BonusStrategy>()
        {
            new DestroyColumnStrategy(_itemDestroyer),
            new DestroyLineStrategy(_itemDestroyer),
            new DestroyLineAndColumnStrategy(_itemDestroyer),
            new DestroyNeighborSreategy(_itemDestroyer)
        };
    }
    public override Item Create(Vector3 position, GameTile[,] gameTiles,int index)
    {
        Bonus bonus;

        if (index < 0)
            index = Random.Range(0, _bonusPrefabs.prefabs.Length);

        bonus = Object.Instantiate(_bonusPrefabs.prefabs[index],position,Quaternion.identity);
        
        bonus.SetBonus(_bonusStrategies[Random.Range(0, _bonusStrategies.Count)]);
        bonus.bonusExplosionPool = _bounsExplosionPool;
        bonus.Init(_itemDestroyer,ItemType.Bonus);
        
        return bonus;
    }
}
