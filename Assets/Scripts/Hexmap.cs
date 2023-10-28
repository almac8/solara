using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexmap : MonoBehaviour {
  [SerializeField] private GameObject tile;
  [SerializeField] private Vector3 offset;
  [SerializeField] private int tileCount;

  private void Awake() {
    Vector3 finalOffset = Vector3.zero;

    for(int x = 0; x < tileCount; x++) {
      finalOffset.x = offset.x * x;

      for(int y = 0; y < tileCount; y++) {
        finalOffset.z = offset.z * y;

        if(y % 2 != 0) {
          finalOffset.x += offset.x / 2;
        } else {
          finalOffset.x -= offset.x / 2;
        }

        Instantiate(tile, finalOffset, tile.transform.rotation);
      }
    }
  }
}