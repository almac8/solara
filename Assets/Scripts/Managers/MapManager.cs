using UnityEngine;

public class MapManager : MonoBehaviour {
  [SerializeField] private GameObject tile;
  
  private float tileWidth = 1.73f;
  private float tileHeight = 1.44f;

  private void Awake() {
    MapGenerator mapGenerator = new MapGenerator(256, 32);
    InstantiateMapTiles(mapGenerator.TileValues);
  }

  private void InstantiateMapTiles(int[][] map) {
    for(int x = 0; x < map.Length; x++) {
      for(int y = 0; y < map[x].Length; y++) {
        if(map[x][y] == 0) {
          Vector3 offset = transform.position;
          offset.x = x * 1.73f;
          offset.z = y * 1.44f;
          if(y % 2 == 0) offset.x += 1.73f / 2f;

          Instantiate(tile, offset, tile.transform.rotation, transform);
        }
      }
    }
  }

  public Vector2 GetTileIndex(Vector3 absolutePosition) {
    Vector2 tileIndex = Vector2.zero;

    tileIndex.x = Mathf.FloorToInt(absolutePosition.x / tileWidth);
    tileIndex.y = Mathf.FloorToInt(absolutePosition.z / tileHeight);

    return tileIndex;
  }

  public Vector3 GetTilePosition(Vector2 tileIndex) {
    Vector3 tilePosition = Vector3.zero;

    tilePosition.x = tileIndex.x * tileWidth + tileWidth / 2;
    tilePosition.z = tileIndex.y * tileHeight + tileHeight / 2;

    return tilePosition;
  }
}