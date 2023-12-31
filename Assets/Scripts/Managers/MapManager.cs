using UnityEngine;

public class MapManager : MonoBehaviour {
  public float TileWidth { get; set; }
  public float TileHeight { get; set; }
  public int MapSize { get; set; }
  
  private MapGenerator mapGenerator;

  private void Awake() {
    TileWidth = 0.866f;
    TileHeight = 0.75f;
    MapSize = 128;

    mapGenerator = new MapGenerator(MapSize, 4444);
    mapGenerator.GenerateTopography(0.02f, 0.2f, 0.6f);
    TerrainGenerator terrainGenerator = new TerrainGenerator(mapGenerator.TileValues, TileWidth, TileHeight);
  }

  public MapGenerator.TileType[][] GetTileValues() {
    return mapGenerator.TileValues;
  }

  public Vector2 GetTileIndex(Vector3 absolutePosition) {
    Vector2 tileIndex = Vector2.zero;

    tileIndex.x = Mathf.FloorToInt(absolutePosition.x / TileWidth) + MapSize / 2;
    tileIndex.y = Mathf.FloorToInt(absolutePosition.z / TileHeight) + MapSize / 2;

    return tileIndex;
  }

  public Vector3 GetTilePosition(Vector2 tileIndex) {
    Vector3 tilePosition = Vector3.zero;

    tilePosition.x = tileIndex.x * TileWidth;
    if(tileIndex.y % 2 > 0) tilePosition.x += TileWidth / 2;
    tilePosition.x -= TileWidth * MapSize / 2;

    tilePosition.z = tileIndex.y * TileHeight;
    tilePosition.z -= TileHeight * MapSize / 2;

    return tilePosition;
  }
}