using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

public class WorldGeneratorSceneManager {
  private string userScenePath;
  private UnityEngine.SceneManagement.Scene worldGeneratorScene;
  
  private static List<GameObject> tiles;
  private GameObject terrain;

  private bool showTiles;
  private bool showTerrain;

  public WorldGeneratorSceneManager() {
    tiles = new List<GameObject>();
    terrain = null;
    showTiles = true;
    showTerrain = true;
  }

  public void OpenScene() {
    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
		userScenePath = EditorSceneManager.GetActiveScene().path;
    worldGeneratorScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
    SceneView.lastActiveSceneView.FrameSelected();
  }

  public void CloseScene() {
    EditorSceneManager.OpenScene(userScenePath);
  }

  public void RefreshScene() {
    if(tiles.Count > 0) {
      foreach (GameObject tile in tiles) {
        GameObject.DestroyImmediate(tile);
      }

      tiles = new List<GameObject>();
    }

    MapGenerator.TileType[][] tileValues = WorldGenerator.MapGenerator.TileValues;
    if(tileValues == null) {
      Debug.LogError("Map Generator does not contain any tile data");
      return;
    }

    float mapWidth = WorldGenerator.WorldSettings.worldSize * WorldGenerator.WorldSettings.tileWidth;
    float mapHeight = WorldGenerator.WorldSettings.worldSize * WorldGenerator.WorldSettings.tileHeight;

    for(int y = 0; y < tileValues.Length; y++) {
      for(int x = 0; x < tileValues.Length; x++) {
        if(tileValues[x][y] == 0) {
          var instance = (GameObject) PrefabUtility.InstantiatePrefab(WorldGenerator.WorldSettings.tileObject, worldGeneratorScene);

          Vector3 tilePosition = new Vector3(x * WorldGenerator.WorldSettings.tileWidth, 0, y * WorldGenerator.WorldSettings.tileHeight);
          tilePosition.x -= mapWidth / 2;
          tilePosition.z -= mapHeight / 2;
          instance.transform.position = tilePosition;

          tiles.Add(instance);
        }
      }
    }

    if(terrain != null) GameObject.DestroyImmediate(terrain);

    TerrainGenerator terrainGenerator = new TerrainGenerator(tileValues, WorldGenerator.WorldSettings.tileWidth, WorldGenerator.WorldSettings.tileHeight);
    terrain = terrainGenerator.Terrains;
  }

  public void ToggleTiles() {
    showTiles = !showTiles;
    foreach (GameObject tile in tiles) tile.SetActive(showTiles);
  }

  public void ToggleTerrain() {
    showTerrain = !showTerrain;
    terrain.SetActive(showTerrain);
  }
}