using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator {
  public enum TileType {
    VALLEY,
    SEA_LEVEL,
    MOUNTAIN
  };

  private TileType[][] tileValues;

  public int Size { get; private set; }
  public int Seed { get; private set; }

  public TileType[][] TileValues {
    get {
      return tileValues;
    }

    private set {
      tileValues = value;
    }
  }

  public MapGenerator(int size, int worldSeed) {
    Size = size + 1;
    Seed = worldSeed;
    Random.InitState(Seed);
    GenerateBlank();
  }

  private void GenerateBlank() {
    TileValues = new TileType[Size][];

    for(int x = 0; x < Size; x++) {
      TileValues[x] = new TileType[Size];
    }
  }

  public void GenerateTopography(float scale, float valleyCutoff, float mountainCutoff) {
    Vector2 offset = new Vector2(Random.value * Size, Random.value * Size);

    for(int y = 0; y < Size; y++) {
      for(int x = 0; x < Size; x++) {
        TileValues[x][y] = TileType.SEA_LEVEL;

        float noiseSample = Mathf.PerlinNoise(offset.x + (x*scale), offset.y + (y*scale));
        if(noiseSample < valleyCutoff) {
          TileValues[x][y] = TileType.VALLEY;
        } else if(noiseSample > mountainCutoff) {
          TileValues[x][y] = TileType.MOUNTAIN;
        }
      }
    }
  }
}