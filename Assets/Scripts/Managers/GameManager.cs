using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SelectionManager))]
[RequireComponent(typeof(UnitManager))]
[RequireComponent(typeof(MapManager))]
[RequireComponent(typeof(UIManager))]
public class GameManager : MonoBehaviour {
  [SerializeField] private GameObject tile;
  
  public static GameManager Instance { get; private set; }
  public SelectionManager SelectionManager { get; private set; }
  public UnitManager UnitManager { get; private set; }
  public MapManager MapManager { get; private set; }
  public UIManager UIManager { get; private set; }

  private void Awake() {
    if(Instance != null && Instance != this) {
      Destroy(this);
      return;
    }

    Instance = this;
    SelectionManager = GetComponent<SelectionManager>();
    UnitManager = GetComponent<UnitManager>();
    MapManager = GetComponent<MapManager>();
    UIManager = GetComponent<UIManager>();

    MapGenerator mapGenerator = new MapGenerator(256, 32);
    InstantiateMap(mapGenerator.TileValues);
  }

  private void InstantiateMap(int[][] map) {
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
}