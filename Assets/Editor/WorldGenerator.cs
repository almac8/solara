using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class WorldGenerator : EditorWindow {
  private const int MIN_MAP_SIZE = 2;
  private const int MAX_MAP_SIZE = 64;

  string[] mapSizeNames = new string[] {
    "Small",
    "Medium",
    "Large"
  };
  
  int[] mapSizeValues = new int[] {
    32,
    64,
    128
  };

  private static string previousScene;
  private static WorldGenerator window;
  private static UnityEngine.SceneManagement.Scene scene;
  private static List<GameObject> tiles;
  private GameObject terrain;
  
  private int worldSeed;
  private GameObject tileObject;
  private int mapSize;
  private float tileWidth;
  private float tileHeight;
  private bool showTiles;
  private bool showTerrain;
  
  [MenuItem("Window/World Generator")]
  private static void ShowWindow() {
    tiles = new List<GameObject>();

    window = GetWindow<WorldGenerator>();
    window.titleContent = new GUIContent("World Generator");
    window.Show();

    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
		string currentScene = EditorSceneManager.GetActiveScene().path;
    previousScene = currentScene;
		
		scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
		SceneView.lastActiveSceneView.FrameSelected();
  }
  
  private void OnGUI() {
    EditorGUILayout.BeginVertical();
    worldSeed = EditorGUILayout.IntField("World Seed: ", worldSeed);
    tileObject = (GameObject)EditorGUILayout.ObjectField(tileObject, typeof(Object), true);
    mapSize = EditorGUILayout.IntPopup("Map Size: ", mapSize, mapSizeNames, mapSizeValues);
    tileWidth = EditorGUILayout.FloatField("Tile Width: ", tileWidth);
    tileHeight = EditorGUILayout.FloatField("Tile Height: ", tileHeight);
    EditorGUILayout.EndVertical();

    if(GUILayout.Button("Generate")) {
      GenerateWorld();
      ToggleTiles();
      ToggleTiles();
      ToggleTerrain();
      ToggleTerrain();
    }

    if(GUILayout.Button("Toggle Tiles")) ToggleTiles();
    if(GUILayout.Button("Toggle Terrain")) ToggleTerrain();

    if(GUILayout.Button("Close")) {
      EditorSceneManager.OpenScene(previousScene);
      window.Close();
    }
  }

  private void GenerateWorld() {
    if(worldSeed == 0) {
      Debug.LogWarning("World Seed has not been set\nUsing random value");
      worldSeed = (int)(Random.value * 100000000);
    }

    if(tileObject == null) {
      Debug.LogError("Tile has not been assigned");
      return;
    }

    if(tiles.Count > 0) {
      foreach (GameObject tile in tiles) {
        GameObject.DestroyImmediate(tile);
      }

      tiles = new List<GameObject>();
    }

    MapGenerator mapGenerator = new MapGenerator(mapSize, worldSeed);
    mapGenerator.GenerateMountainous();

    int[][] tileValues = mapGenerator.TileValues;
    if(tileValues == null) {
      Debug.LogError("Map has not been Generated yet");
      return;
    }

    float mapWidth = mapSize * tileWidth;
    float mapHeight = mapSize * tileHeight;

    for(int y = 0; y < tileValues.Length; y++) {
      for(int x = 0; x < tileValues.Length; x++) {
        if(tileValues[x][y] == 0) {
          var instance = (GameObject) PrefabUtility.InstantiatePrefab(tileObject, scene);

          Vector3 tilePosition = new Vector3(x * tileWidth, 0, y * tileHeight);
          tilePosition.x -= mapWidth / 2;
          tilePosition.z -= mapHeight / 2;
          instance.transform.position = tilePosition;

          tiles.Add(instance);
        }
      }
    }

    if(terrain != null) {
      GameObject.DestroyImmediate(terrain);
    }

    TerrainGenerator terrainGenerator = new TerrainGenerator(tileValues, tileWidth, tileHeight);
    terrain = terrainGenerator.Terrains;
  }

  private void ToggleTiles() {
    if(tiles.Count == 0) {
      Debug.LogError("Tiles have not been Generated");
      return;
    }

    foreach (GameObject tile in tiles) {
      tile.SetActive(showTiles);
    }

    showTiles = !showTiles;
  }

  private void ToggleTerrain() {
    if(terrain == null) {
      Debug.LogError("Terrain has not been Generated");
      return;
    }

    terrain.SetActive(showTerrain);
    showTerrain = !showTerrain;
  }
}