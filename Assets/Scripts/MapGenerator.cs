using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator {
  private int[][] tileValues;

  public int Size { get; private set; }
  public int Seed { get; private set; }

  public int[][] TileValues {
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
    TileValues = new int[Size][];

    for(int x = 0; x < Size; x++) {
      TileValues[x] = new int[Size];
    }
  }

  public void GenerateTopography(float scale, float cutoff) {
    Vector2 offset = new Vector2(Random.value * Size, Random.value * Size);

    for(int y = 0; y < Size; y++) {
      for(int x = 0; x < Size; x++) {
        float noiseSample = Mathf.PerlinNoise(offset.x + (x*scale), offset.y + (y*scale));
        if(noiseSample > cutoff) {
          TileValues[x][y] = 1;
        } else {
          TileValues[x][y] = 0;
        }
      }
    }
  }
}