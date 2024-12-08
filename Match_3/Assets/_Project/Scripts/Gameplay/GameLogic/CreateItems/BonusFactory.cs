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
            new DestroyColumnStrategy(),
            new DestroyLineStrategy(),
            //new DestroyLineAndColumnStrategy(),
            new DestroyNeighborSreategy()
        };
    }
    public override Item Create(Vector3 position, GameTile[,] gameTiles)
    {
        Bonus bonus = Object.Instantiate(_bonusPrefabs.prefabs[0],position,Quaternion.identity);
        //bonus.Init(_bonusStrategies[Random.Range(0,_bonusStrategies.Count)]);
        
        bonus.SetBonus(new DestroyLineAndColumnStrategy(_itemDestroyer));
        bonus.Init(_itemDestroyer);
        
        return bonus;
    }
}
