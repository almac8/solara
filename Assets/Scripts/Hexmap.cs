using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hexmap : MonoBehaviour {
  [SerializeField] private GameObject tile;
  [SerializeField] private Vector3 offset;
  [SerializeField] private int tileCount;

  public Vector3 HoveredTilePosition { get; private set; }

  public event Action TileHover;
  public event Action TileClick;

  private void Awake() {
    int numLines = tileCount * 2 - 1;
    Vector3 spawnpoint = transform.position;
    int c = tileCount;

    spawnpoint.z += ((numLines / 2) + 1) * offset.z;
    for(int z = 0; z < numLines; z++) {
      spawnpoint.z -= offset.z;
      spawnpoint.x = transform.position.x - (c - 1) * (offset.x / 2);

      for(int x = 0; x < c; x++) {
        Instantiate(tile, spawnpoint, tile.transform.rotation, transform);
        spawnpoint.x += offset.x;
      }

      if(z < numLines / 2) {
        c++;
      } else {
        c--;
      }
    }
  }

  public void TileHovered(Vector3 tilePosition) {
    HoveredTilePosition = tilePosition;
    TileHover?.Invoke();
  }

  public void TileClicked(Vector3 tilePosition) {
    TileClick?.Invoke();
  }
}