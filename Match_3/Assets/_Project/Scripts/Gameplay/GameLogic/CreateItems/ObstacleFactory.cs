using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : ItemFactory
{
    private readonly Obstacle _obstaclePrefab;

    public ObstacleFactory()
    {
        _obstaclePrefab = Resources.Load<Obstacle>("Box");
    }

    public override Item Create(Vector3 position, GameTile[,] gameTiles)
    {
        return Object.Instantiate(_obstaclePrefab, position, Quaternion.identity);
    }
}
