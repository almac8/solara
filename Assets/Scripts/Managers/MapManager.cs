using UnityEngine;

public class MapManager : MonoBehaviour {
  public float TileWidth { get; set; }
  public float TileHeight { get; set; }
  public int MapSize { get; set; }
  
  private MapGenerator mapGenerator;

  private void Awake() {
    TileWidth = 1.73f;
    TileHeight = 1.44f;
    MapSize = 32;

    mapGenerator = new MapGenerator(MapSize, 4444);
    TerrainGenerator terrainGenerator = new TerrainGenerator(mapGenerator.TileValues, TileWidth, TileHeight);
  }

  public int[][] GetTileValues() {
    return mapGenerator.TileValues;
  }

  public Vector2 GetTileIndex(Vector3 absolutePosition) {
    Vector2 tileIndex = Vector2.zero;

    tileIndex.x = Mathf.FloorToInt(absolutePosition.x / TileWidth);
    tileIndex.y = Mathf.FloorToInt(absolutePosition.z / TileHeight);

    return tileIndex;
  }

  public Vector3 GetTilePosition(Vector2 tileIndex) {
    Vector3 tilePosition = Vector3.zero;

    tilePosition.x = tileIndex.x * TileWidth + TileWidth / 2;
    tilePosition.z = tileIndex.y * TileHeight + TileHeight / 2;

    return tilePosition;
  }
}