using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : ItemFactory
{
    private readonly Obstacle _obstaclePrefab;
    private readonly ItemDestroyer _itemDestroyer;

    public ObstacleFactory(ItemDestroyer itemDestroyer)
    {
        _itemDestroyer = itemDestroyer;
        _obstaclePrefab = Resources.Load<Obstacle>("Box");
    }

    public override Item Create(Vector3 position, GameTile[,] gameTiles)
    {
        Obstacle obstacle = Object.Instantiate(_obstaclePrefab, position, Quaternion.identity);
        obstacle.Init(_itemDestroyer);

        return obstacle;
    }
}
