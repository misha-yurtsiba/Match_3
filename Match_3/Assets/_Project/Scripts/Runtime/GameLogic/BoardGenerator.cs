using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator
{
    private readonly GameTile _gameTilePrefab;

    private readonly FruitFactory _fruitFactory;
    private readonly ObstacleFactory _obstacleFactory;
    private readonly BonusFactory _bonusFactory;

    private ItemFactory _itemFactory;

    private Dictionary<ItemType, ItemFactory> _itemFactories;

    private ISaveStarategy _saveStarategy = new JsonSaveFromResources();
    public BoardGenerator(GameTile gameTilePrefab, FruitFactory fruitFactory, ObstacleFactory obstacleFactory, BonusFactory bonusFactory)
    {
        _gameTilePrefab = gameTilePrefab;
        _fruitFactory = fruitFactory;
        _obstacleFactory = obstacleFactory;
        _bonusFactory = bonusFactory;

        _itemFactories = new Dictionary<ItemType, ItemFactory>()
        {
            { ItemType.Fruit,fruitFactory},
            { ItemType.Bonus,bonusFactory},
            { ItemType.Obstacle,obstacleFactory}
        };
    }
    public GameTile[,] GenerateBoardFromFile(LevelData levelData, Vector3 offset)
    {

        int width = levelData.width;
        int height = levelData.height;
        GameTile[,] gameTiles = new GameTile[width, height];

        int p = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                SpawnOneItemFromData(i, j, offset, gameTiles, levelData.itemData[p]);
                p++;
            }
        }

        return gameTiles;
    }

    private void SpawnOneItemFromData(int i, int j, Vector3 offset, GameTile[,] gameTiles, ItemData itemData)
    {
        Item item = _itemFactories[itemData.itemType].Create(new Vector3(i, j, 0) + offset, gameTiles, itemData.index);

        GameTile gameTile = Object.Instantiate(_gameTilePrefab, new Vector3(i, j, 0), Quaternion.identity);
        item.SetTile(gameTile);

        gameTile.curentItem = item;
        gameTile.xPos = i;
        gameTile.yPos = j;

        gameTiles[i, j] = gameTile;
    }
}
