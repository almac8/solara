using UnityEngine;

public class HexmapRenderer : MonoBehaviour {
  [SerializeField] private MapManager hexmap;
  [SerializeField] private GameObject tile;

  [SerializeField] private float TileWidth = 0.866f;
  [SerializeField] private float TileHeight = 0.75f;

  private TerrainCollider terrainCollider;

  private void Start() {
    MapGenerator.TileType[][] tiles = hexmap.GetTileValues();
    GameObject tileMap = new GameObject();
    tileMap.name = "Tilemap";
    terrainCollider = Terrain.activeTerrain.GetComponent<TerrainCollider>();

    for(int y = 0; y < tiles.Length; y++) {
      for(int x = 0; x < tiles[y].Length; x++) {
        Vector3 tilePosition = GameManager.Instance.MapManager.GetTilePosition(new Vector2(x, y));
        Instantiate(tile, tilePosition, tile.transform.rotation, tileMap.transform);
      }
    }
  }
}