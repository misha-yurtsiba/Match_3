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

    private SaveStarategy<LevelData> _saveStarategy = new JsonSaveStrayegy();
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
    public GameTile[,] GenerateBoard(int width, int height, Vector3 offset)
    {
        GameTile[,] gameTiles = new GameTile[width, height];

        _itemFactory = _obstacleFactory;

        for(int i = 0; i < height; i++)
        {
            SpawnOneItem(0, i, offset, gameTiles);
        }

        _itemFactory = _fruitFactory;

        for (int i = 1; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if(i == 4 && j == 4)
                {
                    _itemFactory = _bonusFactory;
                    SpawnOneItem(i,j,offset, gameTiles);
                    _itemFactory = _fruitFactory;
                }
                else
                {
                    SpawnOneItem(i,j,offset, gameTiles);
                }
            }
        }

        LevelData levelData = new LevelData();
        levelData.Level = 0;
        levelData.width = width;
        levelData.height = height;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                levelData.itemData.Add(new ItemData(gameTiles[i,j].curentItem.itemType, gameTiles[i, j].curentItem.Index));
            }
        }
        _saveStarategy.Save(levelData);

        return gameTiles;
    }

    private void SpawnOneItem(int i, int j, Vector3 offset, GameTile[,] gameTiles)
    {
        Item item = _itemFactory.Create(new Vector3(i, j, 0) + offset, gameTiles,-1);

        GameTile gameTile = Object.Instantiate(_gameTilePrefab, new Vector3(i, j, 0), Quaternion.identity);
        item.SetTile(gameTile);

        gameTile.curentItem = item;
        gameTile.xPos = i;
        gameTile.yPos = j;

        gameTiles[i, j] = gameTile;
    }


    public GameTile[,] GenerateBoardFromFile(int level, Vector3 offset)
    {
        LevelData levelData = _saveStarategy.Load(level);

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
