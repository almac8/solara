using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexmap : MonoBehaviour {
  [SerializeField] private GameObject tile;
  [SerializeField] private Vector3 offset;
  [SerializeField] private int tileCount;

  private void Awake() {
    int numLines = tileCount * 2 - 1;
    Vector3 spawnpoint = transform.position;
    int c = tileCount;

    spawnpoint.z += ((numLines / 2) + 1) * offset.z;
    for(int z = 0; z < numLines; z++) {
      spawnpoint.z -= offset.z;
      spawnpoint.x = transform.position.x - (c - 1) * (offset.x / 2);

      for(int x = 0; x < c; x++) {
        Instantiate(tile, spawnpoint, tile.transform.rotation);
        spawnpoint.x += offset.x;
      }

      if(z < numLines / 2) {
        c++;
      } else {
        c--;
      }
    }
  }
}