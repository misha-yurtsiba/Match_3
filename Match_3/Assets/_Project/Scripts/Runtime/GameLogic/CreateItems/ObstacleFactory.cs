using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : ItemFactory
{
    private readonly ObstaclePrefabs _obstaclePrefabs;
    private readonly ItemDestroyer _itemDestroyer;
    private readonly IItemProvider _itemProvider;

    private readonly ObjectPool<BoxExplosion> _explosionPool;

    public ObstacleFactory(ItemDestroyer itemDestroyer, ObstecleProvider obstecleProvider, ObjectPool<BoxExplosion> explosionPool)
    {
        _itemDestroyer = itemDestroyer;
        _itemProvider = obstecleProvider;
        _explosionPool = explosionPool;
        
        _obstaclePrefabs = Resources.Load<ObstaclePrefabs>("Configs/ObstaclePrefabs");

    }

    public override Item Create(Vector3 position, GameTile[,] gameTiles,int index)
    {
        Obstacle obstacle = Object.Instantiate(_obstaclePrefabs.obstaclePrefabs[0], position, Quaternion.identity);
        obstacle.Init(_itemDestroyer,ItemType.Obstacle,_itemProvider);
        obstacle.boxExplosionPool = _explosionPool;

        _itemProvider.AddItem(obstacle);

        return obstacle;
    }
}
