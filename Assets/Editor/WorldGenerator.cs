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
    128,
    256,
    512
  };

  private static string previousScene;
  private static WorldGenerator window;
  private static UnityEngine.SceneManagement.Scene scene;
  private static List<GameObject> tiles;
  private GameObject terrain;

  private MapGenerator mapGenerator;

  private bool worldFoldout;
  private int worldSeed;
  private int mapSize;

  private bool tileFoldout;
  private GameObject tileObject;
  private float tileWidth;
  private float tileHeight;
  
  private bool displayFoldout;
  private bool showTiles;
  private bool showTerrain;

  private bool topographyFoldout;
  private float topographyScale;
  private float valleyCutoff;
  private float mountainCutoff;
  
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
    
    worldFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(worldFoldout, "World");
    if(worldFoldout) {
      worldSeed = EditorGUILayout.IntField("World Seed: ", worldSeed);
      mapSize = EditorGUILayout.IntPopup("Map Size: ", mapSize, mapSizeNames, mapSizeValues);
    }
    EditorGUILayout.EndFoldoutHeaderGroup();

    if(worldSeed != 0 && mapSize != 0) {
      tileFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(tileFoldout, "Tile");
      if(tileFoldout) {
        tileObject = (GameObject)EditorGUILayout.ObjectField(tileObject, typeof(Object), true);
        tileWidth = EditorGUILayout.FloatField("Tile Width: ", tileWidth);
        tileHeight = EditorGUILayout.FloatField("Tile Height: ", tileHeight);
      }
      EditorGUILayout.EndFoldoutHeaderGroup();
    }

    if(tileObject != null && tileWidth != 0 && tileHeight != 0) {
      topographyFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(topographyFoldout, "Topography");
      if(topographyFoldout) {
        topographyScale = EditorGUILayout.FloatField("Topography Scale: ", topographyScale);
        valleyCutoff = EditorGUILayout.FloatField("Topography Cutoff: ", valleyCutoff);
        mountainCutoff = EditorGUILayout.FloatField("Topography Cutoff: ", mountainCutoff);
        if(GUILayout.Button("Generate Topography")) GenerateTopography();
      }
      EditorGUILayout.EndFoldoutHeaderGroup();
    }

    if(tiles.Count != 0) {
      displayFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(displayFoldout, "Display");
      if(displayFoldout) {
        if(GUILayout.Button("Toggle Tiles")) ToggleTiles();
        if(GUILayout.Button("Toggle Terrain")) ToggleTerrain();
      }
      EditorGUILayout.EndFoldoutHeaderGroup();
    }

    if(GUILayout.Button("Close")) {
      EditorSceneManager.OpenScene(previousScene);
      window.Close();
    }

    EditorGUILayout.EndVertical();
  }

  private void GenerateTopography() {
    mapGenerator = new MapGenerator(mapSize, worldSeed);
    mapGenerator.GenerateTopography(topographyScale, valleyCutoff, mountainCutoff);

    UpdateVisuals();
  }

  private void UpdateVisuals() {
    if(tiles.Count > 0) {
      foreach (GameObject tile in tiles) {
        GameObject.DestroyImmediate(tile);
      }

      tiles = new List<GameObject>();
    }

    MapGenerator.TileType[][] tileValues = mapGenerator.TileValues;
    if(tileValues == null) {
      Debug.LogError("Map Generator does not contain any tile data");
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