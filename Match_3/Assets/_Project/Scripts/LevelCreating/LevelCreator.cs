using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class LevelCreator : EditorWindow
{
    private Item _spawnItem;
    private GameTile _spawnTile;

    private int _width;
    private int _height;

    private int _level;


    private GameTile [,] _gameTiles;
    private Camera sceneCamera;

    private bool _isSpawning = false;
    private bool _isBoardSpavned = false;

    [MenuItem("Tools/Object Spawner")]
    public static void ShowWindow()
    {
        GetWindow<LevelCreator>("Object Spawner");
    }

    private void OnGUI()
    {
        GUILayout.Label("Object Spawner", EditorStyles.boldLabel);

        EditorGUILayout.Space(2);

        _spawnItem = (Item)EditorGUILayout.ObjectField("Spawn item", _spawnItem, typeof(Item), false);
        
        _spawnTile = (GameTile)EditorGUILayout.ObjectField("Spawn game tile", _spawnTile, typeof(GameTile), false);
        
        EditorGUILayout.Space(2);

        _width = EditorGUILayout.IntField("Width", _width);
        _height = EditorGUILayout.IntField("Height", _height);
        _level = EditorGUILayout.IntField("Level", _level);

        if (GUILayout.Button("Spawn game board"))
            SpawnGameBoard();

        if (GUILayout.Button("Start Spawning"))
            StartSpawning();

        if (GUILayout.Button("Stop Spawning"))
            StopSpawning();
    }
    private void StartSpawning()
    {
        if (_spawnItem == null)
        {
            Debug.LogError("Object to spawn is not assigned!");
            return;
        }
        _isSpawning = true;
        SceneView.duringSceneGui += OnSceneGUI; 
        SceneView.RepaintAll();
    }

    private void StopSpawning()
    {
        _isSpawning = false;
        SceneView.duringSceneGui -= OnSceneGUI; 
        SceneView.RepaintAll(); 
    }

    private void SpawnGameBoard()
    {
        for (int i = 1; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                GameTile gameTile = Instantiate(_spawnTile, new Vector3(i, j, 0), Quaternion.identity);

            }
        }
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (!_isSpawning) return;

        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 0)
        {
            sceneCamera = sceneView.camera;

            if (sceneCamera == null)
            {
                Debug.LogError("Scene camera not found!");
                return;
            }

            Ray ray = sceneCamera.ScreenPointToRay(HandleUtility.GUIPointToScreenPixelCoordinate(e.mousePosition));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100) && hit.transform.TryGetComponent(out GameTile gameTile))
            {
                Instantiate(_spawnItem, gameTile.transform.position + new Vector3(0, 0, -0.5f), Quaternion.identity);
            }

            e.Use();
        }
    }
}


