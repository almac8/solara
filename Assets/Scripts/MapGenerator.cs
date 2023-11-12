using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator {
  private int[][] tileValues;

  public int Width { get; private set; }
  public int Height { get; private set; }

  public int[][] TileValues {
    get {
      return tileValues;
    }

    private set {
      tileValues = value;
    }
  }

  public MapGenerator(int width, int height) {
    Width = width;
    Height = height;

    TileValues = new int[Width][];
    for(int i = 0; i < Width; i++) {
      TileValues[i] = new int[Height];
    }
  }

  public override string ToString() {
    string mapString = "";
    
    for(int y = 0; y < Height; y++) {
      for(int x = 0; x < Width; x++) {
        mapString = mapString + "0";
      }

      mapString = mapString + "\n";
    }

    return mapString;
  }
}