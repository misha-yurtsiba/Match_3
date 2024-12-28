using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fruit : Item, IMoveble
{
    public bool isMatched = false;

    public ObjectPool<SmokeExplosion> smokeExplosionPool;
    
    public Transform Transform
    {
        get =>  transform; 
        set =>  transform.position = value.position; 
    }
    override public void DestroyAction(GameBoard gameBoard)
    {
        IEnumerable obstacles = CheckNeibghorTiles(gameBoard._board);

        foreach (Item item in obstacles)
        {
            if(item is Obstacle obstacle)
                _itemDestroyer.DestroyOneItem(obstacle);

            if(item is Bonus bonus)
                bonus.DestroyAction(gameBoard);
        }
    }

    public async override UniTask DestroyItemAsync()
    {
        PlayDestroyParticle();

        await base.DestroyItemAsync();
    }

    private void PlayDestroyParticle()
    {
        SmokeExplosion smokeExplosion = smokeExplosionPool.Get();

        smokeExplosion.transform.position = transform.position;

        smokeExplosion.Play();
    }

    private IEnumerable<Item> CheckNeibghorTiles(GameTile[,] gameBoard)
    {
        int x = gameBoard.GetLength(0);
        int y = gameBoard.GetLength(1);

        List<Vector2Int> directions = new List<Vector2Int>()
        {
            Vector2Int.down,
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.left,
            new Vector2Int(1, 1),
            new Vector2Int(-1, -1),
            new Vector2Int(1, -1),
            new Vector2Int(-1, 1)
        };

        List<Item> obstacles = new List<Item>();

        foreach(Vector2Int direction in directions)
        {
            int newX = CurentTile.xPos + direction.x;
            int newY = CurentTile.yPos + direction.y;

            if(newX >= 0 && newX < x && newY >= 0 && newY < y && (gameBoard[newX,newY].curentItem is Obstacle obstacle))
            {
                obstacles.Add(obstacle);
                return obstacles;
            }

            else if (newX >= 0 && newX < x && newY >= 0 && newY < y && (gameBoard[newX, newY].curentItem is Bonus bonus))
            {
                obstacles.Add(bonus);
                return obstacles;
            }
        }

        return obstacles;
    }
}
