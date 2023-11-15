using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator {
  public TerrainGenerator(int[][] tileValues, float tileWidth, float tileHeight) {
    int mapSize = tileValues.Length;

    TerrainData terrainData = new TerrainData();
    terrainData.heightmapResolution = mapSize;
    terrainData.size = new Vector3(mapSize * tileWidth, mapSize / 2, mapSize * tileHeight);

    float[,] heights = new float[mapSize, mapSize];
    for(int x = 0; x < mapSize; x++) {
      for(int y = 0; y < mapSize; y++) {
        switch(tileValues[x][y]) {
          case 0:
            heights[y, x] = 0.5f;
            break;

          case 1:
            heights[y, x] = 1;
            break;
        }
      }
    }

    terrainData.SetHeights(0, 0, heights);

    GameObject terrain = Terrain.CreateTerrainGameObject(terrainData);
    terrain.transform.position = new Vector3(-mapSize / 2 * tileWidth, -mapSize / 4, -mapSize / 2 * tileHeight);
  }
}