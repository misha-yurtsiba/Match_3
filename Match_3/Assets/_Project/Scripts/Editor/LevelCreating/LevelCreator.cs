using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


public class LevelCreator : EditorWindow
{
    private Item _spawnItem;
    private GameTile _spawnTile;
    private Camera sceneCamera;
    private List<Item> _items = new List<Item>();
    private GameTile [,] _gameTiles;

    private int _width;
    private int _height;
    private int _level;
    private int _seconds;
    private int _minutes;
    private int _buttonHaight = 25;

    private string _saveFolder = "Assets/_Project/Resources/LevelData";

    private bool _isSpawning = false;
    private bool _isDeleting = false;
    private bool _isBoardSpavned = false;


    [MenuItem("Tools/Object Spawner")]
    public static void ShowWindow()
    {
        GetWindow<LevelCreator>("Object Spawner");
    }

    private void OnGUI()
    {
        GUILayout.Label("Object Spawner", EditorStyles.boldLabel);

        EditorGUILayout.Space(10);

        _spawnItem = (Item)EditorGUILayout.ObjectField("Spawn item", _spawnItem, typeof(Item), false);
        
        _spawnTile = (GameTile)EditorGUILayout.ObjectField("Spawn game tile", _spawnTile, typeof(GameTile), false);
        
        EditorGUILayout.Space(10);
        _width = EditorGUILayout.IntField("Width", _width);
        _height = EditorGUILayout.IntField("Height", _height);
        _level = EditorGUILayout.IntField("Level", _level);

        if (GUILayout.Button("Spawn game board", GUILayout.Height(_buttonHaight)))
            SpawnGameBoard();

        if (GUILayout.Button("Delete board", GUILayout.Height(_buttonHaight)))
            DeleteBoard();

        EditorGUILayout.Space(10);
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Start spawning", GUILayout.Height(_buttonHaight)))
            StartSpawning();
        
        if (GUILayout.Button("Stop spawning",GUILayout.Height(_buttonHaight)))
            StopSpawning();

        GUILayout.EndHorizontal();

        EditorGUILayout.Space(10);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Start deleting", GUILayout.Height(_buttonHaight)))
            StartDeleting();

        if (GUILayout.Button("Stop deleting", GUILayout.Height(_buttonHaight)))
            StopDeleting();
        GUILayout.EndHorizontal();
        
        EditorGUILayout.Space(10);

        _minutes = EditorGUILayout.IntField("Minutes", _minutes);
        _seconds = EditorGUILayout.IntField("Seconds", _seconds);


        EditorGUILayout.Space(10);

        _saveFolder = EditorGUILayout.TextField("SaveFolder", _saveFolder);

        EditorGUILayout.Space(10);

        if (GUILayout.Button("Save level", GUILayout.Height(_buttonHaight)))
            SaveLevel();




    }
    private void StartSpawning()
    {
        if (_spawnItem == null)
        {
            Debug.LogError("Object to spawn is not assigned!");
            return;
        }

        if (_isDeleting)
        {
            _isDeleting = false;
            SceneView.duringSceneGui -= DelateItem;
        }
        
        _isSpawning = true;
        SceneView.duringSceneGui += SpawnItem;
        SceneView.RepaintAll();
    }

    private void StopSpawning()
    {
        _isSpawning = false;
        SceneView.duringSceneGui -= SpawnItem; 
        SceneView.RepaintAll(); 
    }

    private void StartDeleting()
    {
        if (_isSpawning)
        {
            _isSpawning = false;
            SceneView.duringSceneGui -= SpawnItem;
        }
        
        _isDeleting = true;
        SceneView.duringSceneGui += DelateItem;
        SceneView.RepaintAll();
    }

    private void StopDeleting()
    {
        _isDeleting = false;
        SceneView.duringSceneGui -= DelateItem;
        SceneView.RepaintAll();
    }

    private void SpawnGameBoard()
    {
        Debug.Log(_isBoardSpavned);
        if (_isBoardSpavned) return;

        _gameTiles = new GameTile[_width, _height];

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                GameTile gameTile = Instantiate(_spawnTile, new Vector3(i - 0.5f, j - 0.5f, 0), Quaternion.identity);
                _gameTiles[i,j] = gameTile;
            }
        }

        _isBoardSpavned = true;
    }

    private void DeleteBoard()
    {
        _isBoardSpavned = false;
        if (_gameTiles == null) return;

        foreach(Item item in _items)
            if(item != null)
                DestroyImmediate(item.gameObject);

        for (int i = 0; i < _gameTiles.GetLength(0); i++)
        {
            for (int j = 0; j < _gameTiles.GetLength(1); j++)
            {
                if (_gameTiles[i, j] != null)
                    DestroyImmediate(_gameTiles[i, j].gameObject);
            }
        }
        _gameTiles = null;
        _items.Clear();
    }

    private void SpawnItem(SceneView sceneView)
    {
        if (!_isSpawning) return;

        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 0)
        {
            GameTile gameTile = GetGameTile(sceneView,e);
            if (gameTile == null || gameTile.curentItem != null) return;

            Item item = Instantiate(_spawnItem, gameTile.transform.position + new Vector3(0, 0, -0.5f), Quaternion.identity);
            gameTile.curentItem = item;
            _items.Add(item);

            e.Use();
        }
    }

    private void DelateItem(SceneView sceneView)
    {
        if (!_isDeleting) return;

        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 0)
        {
            GameTile gameTile = GetGameTile(sceneView, e);

            if (gameTile == null || gameTile.curentItem == null) return;

            _items.Remove(gameTile.curentItem);
            DestroyImmediate(gameTile.curentItem.gameObject);
            gameTile.curentItem = null;

            e.Use();
        }
    }

    private GameTile GetGameTile(SceneView sceneView, Event e)
    {
        sceneCamera = sceneView.camera;

        if (sceneCamera == null)
        {
            Debug.LogError("Scene camera not found!");
            return null;
        }

        Ray ray = sceneCamera.ScreenPointToRay(HandleUtility.GUIPointToScreenPixelCoordinate(e.mousePosition));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100) && hit.transform.TryGetComponent(out GameTile gameTile))
            return gameTile;
        else
            return null;
    } 

    private void SaveLevel()
    {
        if (_gameTiles == null) return;

        LevelData levelData = new LevelData();

        levelData.Level = _level;
        levelData.width = _gameTiles.GetLength(0);
        levelData.height = _gameTiles.GetLength(1);
        levelData.gameTime = _minutes * 60 + _seconds;

        for (int i = 0; i < _gameTiles.GetLength(0); i++)
        {
            for (int j = 0; j < _gameTiles.GetLength(1); j++)
            {
                levelData.itemData.Add(new ItemData(_gameTiles[i, j].curentItem.itemType, _gameTiles[i, j].curentItem.Index));
            }
        }

        ISaveStarategy saveStrayegy = new JsonSaveFromResources(_saveFolder);
        saveStrayegy.Save(levelData,_level.ToString());
    }
}