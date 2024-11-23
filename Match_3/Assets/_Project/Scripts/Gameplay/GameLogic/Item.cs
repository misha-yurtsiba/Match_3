using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private GameTile _curentTile;
    public GameTile CurentTile => _curentTile;
    

    public void SetTile(GameTile newTile) => _curentTile = newTile;
}
