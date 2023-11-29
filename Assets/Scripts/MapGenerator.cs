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
  }

  public void GenerateBlank() {
    TileValues = new int[Size][];

    for(int x = 0; x < Size; x++) {
      TileValues[x] = new int[Size];
    }
  }

  public void GenerateNoise() {
    TileValues = new int[Size][];

    for(int x = 0; x < Size; x++) {
      TileValues[x] = new int[Size];

      for(int y = 0; y < Size; y++) {
        TileValues[x][y] = Random.Range(0, 2);
      }
    }
  }

  public void GenerateMountainous() {
    GenerateNoise();

    for(int i = 0; i < 2; i++) {
      for(int x = 1; x < Size-1; x++) {
        for(int y = 1; y < Size-1; y++) {
          int count = 0;

          count += TileValues[x-1][y-1];
          count += TileValues[x  ][y-1];
          count += TileValues[x+1][y-1];

          count += TileValues[x-1][y  ];
          count += TileValues[x  ][y  ];
          count += TileValues[x+1][y  ];

          count += TileValues[x-1][y+1];
          count += TileValues[x  ][y+1];
          count += TileValues[x+1][y+1];

          if(count > 6) {
            TileValues[x][y] = 1;
          }

          if(count < 5) {
            TileValues[x][y] = 0;
          }

        }
      }
    }
  }
}