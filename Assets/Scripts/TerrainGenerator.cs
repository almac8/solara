using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator {
  public GameObject Terrains { get; private set; }

  public TerrainGenerator(MapGenerator.TileType[][] tileValues, float tileWidth, float tileHeight) {
    int mapSize = tileValues.Length;

    TerrainData terrainData = new TerrainData();
    terrainData.heightmapResolution = mapSize;
    terrainData.size = new Vector3(mapSize * tileWidth, mapSize / 2, mapSize * tileHeight);

    float[,] heights = new float[mapSize, mapSize];
    for(int x = 0; x < mapSize; x++) {
      for(int y = 0; y < mapSize; y++) {
        switch(tileValues[x][y]) {
          case MapGenerator.TileType.VALLEY:
            heights[y, x] = 0.0f;
            break;

          case MapGenerator.TileType.SEA_LEVEL:
            heights[y, x] = 0.5f;
            break;

          case MapGenerator.TileType.MOUNTAIN:
            heights[y, x] = 1.0f;
            break;
        }
      }
    }

    terrainData.SetHeights(0, 0, heights);

    Terrains = Terrain.CreateTerrainGameObject(terrainData);
    Terrains.transform.position = new Vector3(-mapSize / 2 * tileWidth, -mapSize / 4, -mapSize / 2 * tileHeight);
  }
}