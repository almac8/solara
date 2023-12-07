using UnityEngine;

public class HexmapRenderer : MonoBehaviour {
  [SerializeField] private MapManager hexmap;
  [SerializeField] private GameObject tile;

  [SerializeField] private float TileWidth = 0.866f;
  [SerializeField] private float TileHeight = 0.75f;

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
          offset.x += x * TileWidth;
          offset.z += y * TileHeight;
          if(y % 2 == 0) offset.x += TileWidth / 2f;

          Instantiate(tile, offset, tile.transform.rotation, tileMap.transform);
        }
      }
    }
  }
}