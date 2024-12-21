using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : ItemFactory
{
    private readonly Obstacle _obstaclePrefab;
    private readonly ItemDestroyer _itemDestroyer;
    private readonly IItemProvider _itemProvider;

    public ObstacleFactory(ItemDestroyer itemDestroyer, ObstecleProvider obstecleProvider)
    {
        _itemDestroyer = itemDestroyer;
        _obstaclePrefab = Resources.Load<Obstacle>("Box");
        _itemProvider = obstecleProvider;
    }

    public override Item Create(Vector3 position, GameTile[,] gameTiles,int index)
    {
        Obstacle obstacle = Object.Instantiate(_obstaclePrefab, position, Quaternion.identity);
        obstacle.Init(_itemDestroyer,ItemType.Obstacle,_itemProvider);
        _itemProvider.AddItem(obstacle);

        return obstacle;
    }
}
