using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusFactory : ItemFactory
{
    private readonly BonusPrefabs _bonusPrefabs;
    private readonly ItemDestroyer _itemDestroyer;
    private readonly IReadOnlyList<BonusStrategy> _bonusStrategies;
    public BonusFactory(ItemDestroyer itemDestroyer)
    {
        _itemDestroyer = itemDestroyer;
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
        Bonus bonus = Object.Instantiate(_bonusPrefabs.prefabs[index],position,Quaternion.identity);
        bonus.SetBonus(_bonusStrategies[Random.Range(0, _bonusStrategies.Count)]);
        bonus.Init(_itemDestroyer,ItemType.Bonus);
        
        return bonus;
    }
}
