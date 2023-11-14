using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator {
  public TerrainGenerator(int[][] tileValues, float tileWidth, float tileHeight) {
    int mapSize = tileValues.Length;

    TerrainData terrainData = new TerrainData();
    terrainData.size = new Vector3(mapSize * tileWidth, mapSize / 2, mapSize * tileHeight);

    float[,] heights = new float[33, 33];
    for(int x = 0; x < 32; x++) {
      for(int y = 0; y < 32; y++) {
        switch(tileValues[x][y]) {
          case 0:
            heights[x, y] = 0;
            break;

          case 1:
            heights[x, y] = 1;
            break;
        }
      }
    }

    terrainData.SetHeights(0, 0, heights);

    GameObject terrain = Terrain.CreateTerrainGameObject(terrainData);
  }
}