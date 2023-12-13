using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class WorldGenerator : EditorWindow {
  private static WorldSettings worldSettings;

  private static string previousScene;
  private static WorldGenerator window;
  private static UnityEngine.SceneManagement.Scene scene;
  private static List<GameObject> tiles;
  private GameObject terrain;

  private MapGenerator mapGenerator;

  private bool worldFoldout;
  private bool tileFoldout;
  private bool displayFoldout;
  private bool topographyFoldout;
  
  private bool showTiles;
  private bool showTerrain;
  
  [MenuItem("Window/World Generator")]
  private static void ShowWindow() {
    tiles = new List<GameObject>();
    worldSettings = new WorldSettings();

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
      worldSettings.worldSeed = EditorGUILayout.IntField("World Seed: ", worldSettings.worldSeed);
      worldSettings.worldSize = EditorGUILayout.IntPopup("World Size: ", worldSettings.worldSize, worldSettings.mapSizeNames, worldSettings.mapSizeValues);
    }
    EditorGUILayout.EndFoldoutHeaderGroup();

    if(worldSettings.worldSeed != 0 && worldSettings.worldSize != 0) {
      tileFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(tileFoldout, "Tile");
      if(tileFoldout) {
        worldSettings.tileObject = (GameObject)EditorGUILayout.ObjectField(worldSettings.tileObject, typeof(Object), true);
        worldSettings.tileWidth = EditorGUILayout.FloatField("Tile Width: ", worldSettings.tileWidth);
        worldSettings.tileHeight = EditorGUILayout.FloatField("Tile Height: ", worldSettings.tileHeight);
      }
      EditorGUILayout.EndFoldoutHeaderGroup();
    }

    if(worldSettings.tileObject != null && worldSettings.tileWidth != 0 && worldSettings.tileHeight != 0) {
      topographyFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(topographyFoldout, "Topography");
      if(topographyFoldout) {
        worldSettings.topographyScale = EditorGUILayout.FloatField("Topography Scale: ", worldSettings.topographyScale);
        worldSettings.valleyCutoff = EditorGUILayout.FloatField("Topography Cutoff: ", worldSettings.valleyCutoff);
        worldSettings.mountainCutoff = EditorGUILayout.FloatField("Topography Cutoff: ", worldSettings.mountainCutoff);
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
    mapGenerator = new MapGenerator(worldSettings.worldSize, worldSettings.worldSeed);
    mapGenerator.GenerateTopography(worldSettings.topographyScale, worldSettings.valleyCutoff, worldSettings.mountainCutoff);

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

    float mapWidth = worldSettings.worldSize * worldSettings.tileWidth;
    float mapHeight = worldSettings.worldSize * worldSettings.tileHeight;

    for(int y = 0; y < tileValues.Length; y++) {
      for(int x = 0; x < tileValues.Length; x++) {
        if(tileValues[x][y] == 0) {
          var instance = (GameObject) PrefabUtility.InstantiatePrefab(worldSettings.tileObject, scene);

          Vector3 tilePosition = new Vector3(x * worldSettings.tileWidth, 0, y * worldSettings.tileHeight);
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

    TerrainGenerator terrainGenerator = new TerrainGenerator(tileValues, worldSettings.tileWidth, worldSettings.tileHeight);
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