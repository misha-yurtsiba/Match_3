using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FruitFactory : ItemFactory
{
    private readonly FruitsPrefabs _fruitsPrefabs;
    private readonly ItemDestroyer _itemDestroyer;

    public FruitFactory(FruitsPrefabs fruitsPrefabs, ItemDestroyer itemDestroyer)
    {
        _fruitsPrefabs = fruitsPrefabs;
        _itemDestroyer = itemDestroyer;
    }

    public override Item Create(Vector3 position, GameTile [,] gameTiles,int index)
    {
        if(index < 0)
        {
            int randomIndex;

            do
            {
                randomIndex = Random.Range(0, _fruitsPrefabs.prefabList.Count);
            }
            while (CheckNaighbor((int)position.x, (int)position.y, randomIndex, gameTiles));
            Fruit fruit = Object.Instantiate(_fruitsPrefabs.prefabList[randomIndex], position, Quaternion.identity);
            fruit.Init(_itemDestroyer, ItemType.Fruit);

            return fruit;
        }
        else
        {
            Fruit fruit = Object.Instantiate(_fruitsPrefabs.prefabList[index], position, Quaternion.identity);
            fruit.Init(_itemDestroyer, ItemType.Fruit);

            return fruit;
        }
    }


    private bool CheckNaighbor(int x, int y,int index, GameTile[,] gameTiles)
    {
        if(y == 0 || y == 1)
        {
            if (x == 0 || x== 1) return false;

            Fruit fruit = gameTiles[x - 2, y].curentItem as Fruit;
            if ((fruit == null) || (fruit != null && fruit.Index != index)) return false;
        }
        else
        {
            if (x == 0)
            {
                Fruit fruit = gameTiles[x, y - 2].curentItem as Fruit;
                if ((fruit == null) || (fruit != null && fruit.Index != index)) return false;
            }
            else if(x == 1)
            {
                Fruit fruit = gameTiles[x, y - 2].curentItem as Fruit;
                if ((fruit == null) || (fruit != null && fruit.Index != index)) return false;
            }
            else
            {
                Fruit fruitLeft = gameTiles[x - 2, y].curentItem as Fruit;
                Fruit fruitDown = gameTiles[x, y - 2].curentItem as Fruit;

                if (fruitLeft == null || fruitDown == null || (fruitLeft != null && fruitDown != null && fruitLeft.Index != index && fruitDown.Index != index)) return false;
            }
        }
        return true;
    }
}
