using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : MonoBehaviour {
  [SerializeField] private CameraManager cameraManager;
  [SerializeField] private GameObject floorReference;
  [SerializeField] private GameObject cameraCornerReference;
  [SerializeField] private GameObject wallReference;

  [SerializeField] private int numTilesWidth;
  [SerializeField] private int numTilesHeight;
  [SerializeField] private float tileOffset;

  private GameObject[,] tiles;

  private void Awake() {
    tiles = new GameObject[numTilesWidth, numTilesHeight];
  }

  public void SetTile(int x, int y, Blueprint.TileType tile, Quaternion rotation) {
    Vector3 offset = new Vector3(x * tileOffset, 0, y * tileOffset);

    GameObject newTile;
    
    switch (tile) {
      case Blueprint.TileType.BLANK:
        newTile = null;
        break;

      case Blueprint.TileType.FLOOR:
        newTile = GameObject.Instantiate(floorReference, offset, rotation, transform);
        break;
      
      case Blueprint.TileType.FLOOR_CORNER:
        newTile = GameObject.Instantiate(floorReference, offset, rotation, transform);
        break;

      case Blueprint.TileType.CAMERA_CORNER:
        newTile = GameObject.Instantiate(cameraCornerReference, offset, rotation, transform);
        cameraManager.RegisterCamera(newTile.GetComponentInChildren<Camera>(true));
        break;

      case Blueprint.TileType.WALL:
        newTile = null;
        break;

      default:
        newTile = null;
        break;
    }

    if(tiles[x, y] != null) {
      if(tiles[x, y].GetComponentInChildren<Camera>(true)) {
        cameraManager.UnregisterCamera(tiles[x, y].GetComponentInChildren<Camera>(true));
      }
    }

    Destroy(tiles[x, y]);
    tiles[x, y] = newTile;
  }
}