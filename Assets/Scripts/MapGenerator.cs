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

    TileValues = GenerateMountains();
  }

  private int[][] GenerateBlankMap() {
    int[][] tiles = new int[Size][];

    for(int x = 0; x < Size; x++) {
      tiles[x] = new int[Size];

      for(int y = 0; y < Size; y++) {
        tiles[x][y] = 0;
      }
    }

    return tiles;
  }

  private int[][] GenerateRandomness() {
    int[][] tiles = new int[Size][];

    for(int x = 0; x < Size; x++) {
      tiles[x] = new int[Size];

      for(int y = 0; y < Size; y++) {
        tiles[x][y] = Random.Range(0, 2);
      }
    }

    return tiles;
  }

  private int[][] GenerateMountains() {
    int[][] tiles = new int[Size][];

    for(int x = 0; x < Size; x++) {
      tiles[x] = new int[Size];

      for(int y = 0; y < Size; y++) {
        tiles[x][y] = Random.Range(0, 2);
      }
    }

    for(int i = 0; i < 2; i++) {
      for(int x = 1; x < Size-1; x++) {
        for(int y = 1; y < Size-1; y++) {
          int count = 0;

          count += tiles[x-1][y-1];
          count += tiles[x  ][y-1];
          count += tiles[x+1][y-1];

          count += tiles[x-1][y  ];
          count += tiles[x  ][y  ];
          count += tiles[x+1][y  ];

          count += tiles[x-1][y+1];
          count += tiles[x  ][y+1];
          count += tiles[x+1][y+1];

          if(count > 6) {
            tiles[x][y] = 1;
          }

          if(count < 5) {
            tiles[x][y] = 0;
          }

        }
      }
    }

    return tiles;
  }
}