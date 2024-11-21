using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemFactory 
{
    public abstract Item Create(Vector3 position, GameTile[,] gameTiles);
}
