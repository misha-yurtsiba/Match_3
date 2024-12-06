using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusFactory : ItemFactory
{
    private readonly BonusPrefabs _bonusPrefabs; 
    public BonusFactory()
    {
        _bonusPrefabs = Resources.Load<BonusPrefabs>("Configs/BonusPrefabsConfig");
    }
    public override Item Create(Vector3 position, GameTile[,] gameTiles)
    {
        Bonus bonus = Object.Instantiate(_bonusPrefabs.prefabs[0],position,Quaternion.identity);
        bonus.Init(new DestroyNeighborSreategy());
        return bonus;
    }
}
