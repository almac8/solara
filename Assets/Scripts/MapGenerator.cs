using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator {
  private int[][] tileValues;

  public int Size { get; private set; }

  public int[][] TileValues {
    get {
      return tileValues;
    }

    private set {
      tileValues = value;
    }
  }

  public MapGenerator(int size) {
    Size = size + 1;

    GenerateBlankMap();
  }

  private void GenerateBlankMap() {
    TileValues = new int[Size][];

    for(int x = 0; x < Size; x++) {
      TileValues[x] = new int[Size];

      for(int y = 0; y < Size; y++) {
        TileValues[x][y] = 0;
      }
    }
  }

  private void GenerateRandomness() {
    TileValues = new int[Size][];

    for(int x = 0; x < Size; x++) {
      TileValues[x] = new int[Size];

      for(int y = 0; y < Size; y++) {
        TileValues[x][y] = Random.Range(0, 2);
      }
    }
  }
}