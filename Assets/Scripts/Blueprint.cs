using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour {
  [SerializeField] private Construction construction;
  [SerializeField] private GameObject bpBlankReference;
  [SerializeField] private GameObject bpFloorReference;
  [SerializeField] private GameObject bpFloorCornerReference;
  [SerializeField] private GameObject bpCameraCornerReference;
  [SerializeField] private GameObject bpWallReference;
  
  [SerializeField] private int numTilesWidth;
  [SerializeField] private int numTilesHeight;

  private GameObject[,] tiles;

  public enum TileType {
    BLANK,
    FLOOR,
    FLOOR_CORNER,
    CAMERA_CORNER,
    WALL
  }

  private TileType currentlySelectedTile;
  private Quaternion currentlySelectedTileRotation;

  private void Start() {
    tiles = new GameObject[numTilesWidth, numTilesHeight];
    currentlySelectedTile = TileType.BLANK;
    currentlySelectedTileRotation = bpBlankReference.transform.rotation;
    
    for(int x = 0; x < numTilesWidth; x++) {
      for(int y = 0; y < numTilesHeight; y++) {
        InitializeTile(x, y);
      }
    }
  }

  private void Update() {
    if(Input.GetKeyDown(KeyCode.O)) {
      int currentlySelectedInt = (int) currentlySelectedTile;
      currentlySelectedInt--;

      if(currentlySelectedInt < 0) {
        currentlySelectedInt = 4;
      }

      currentlySelectedTile = (TileType) currentlySelectedInt;
    }

    if(Input.GetKeyDown(KeyCode.P)) {
      int currentlySelectedInt = (int) currentlySelectedTile;
      currentlySelectedInt++;

      if(currentlySelectedInt > 4) {
        currentlySelectedInt = 0;
      }

      currentlySelectedTile = (TileType) currentlySelectedInt;
    }

    if(Input.GetKeyDown(KeyCode.R)) {
      Quaternion rotation = Quaternion.Euler(0f, 0f, 90f);
      currentlySelectedTileRotation = currentlySelectedTileRotation * rotation;
    }
  }

  public void TileClicked(int x, int y) {
    SetTile(x, y, currentlySelectedTile);
  }

  private void InitializeTile(int x, int y) {
    Vector3 offset = new Vector3(x - numTilesWidth / 2, y - numTilesHeight / 2, 0);
    tiles[x, y] = GameObject.Instantiate(bpBlankReference, offset, currentlySelectedTileRotation, transform);
    SetTile(x, y, TileType.BLANK);
  }

  private void SetTile(int x, int y, TileType tile) {
    GameObject newTile;

    switch (tile) {
      case TileType.BLANK:
        newTile = GameObject.Instantiate(bpBlankReference, tiles[x, y].transform.position, currentlySelectedTileRotation, transform);
        break;

      case TileType.FLOOR:
        newTile = GameObject.Instantiate(bpFloorReference, tiles[x, y].transform.position, currentlySelectedTileRotation, transform);
        break;
      
      case TileType.FLOOR_CORNER:
        newTile = GameObject.Instantiate(bpFloorCornerReference, tiles[x, y].transform.position, currentlySelectedTileRotation, transform);
        break;

      case TileType.CAMERA_CORNER:
        newTile = GameObject.Instantiate(bpCameraCornerReference, tiles[x, y].transform.position, currentlySelectedTileRotation, transform);
        break;

      case TileType.WALL:
        newTile = GameObject.Instantiate(bpWallReference, tiles[x, y].transform.position, currentlySelectedTileRotation, transform);
        break;

      default:
        newTile = GameObject.Instantiate(bpBlankReference, tiles[x, y].transform.position, currentlySelectedTileRotation, transform);
        break;
    }

    BlueprintTile tileScript = newTile.GetComponent<BlueprintTile>();
    tileScript.SetIndex(x, y);
    Destroy(tiles[x, y]);
    tiles[x, y] = newTile;
    
    Quaternion constructionTileRotation = Quaternion.Euler(0f, currentlySelectedTileRotation.eulerAngles.z, 0f);
    construction.SetTile(x, y, currentlySelectedTile, constructionTileRotation);
  }
}