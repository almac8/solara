using UnityEngine;

public class HexmapRenderer : MonoBehaviour {
  [SerializeField] private MapManager hexmap;
  [SerializeField] private GameObject tile;

  private void Start() {
    MapGenerator.TileType[][] tiles = hexmap.GetTileValues();
    GameObject tileMap = new GameObject();
    tileMap.name = "Tilemap";
    
    tileMap.transform.position = new Vector3(
      -hexmap.MapSize / 2 * hexmap.TileWidth,
      0,
      -hexmap.MapSize / 2 * hexmap.TileHeight
    );

    for(int x = 0; x < tiles.Length; x++) {
      for(int y = 0; y < tiles[x].Length; y++) {
        if(tiles[x][y] == 0) {
          Vector3 offset = tileMap.transform.position;
          offset.x += x * 1.73f;
          offset.z += y * 1.44f;
          if(y % 2 == 0) offset.x += 1.73f / 2f;

          Instantiate(tile, offset, tile.transform.rotation, tileMap.transform);
        }
      }
    }
  }
}