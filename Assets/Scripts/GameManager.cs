using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
  private void Awake() {
    MapGenerator mapGenerator = new MapGenerator(128, 32);
    Debug.Log(mapGenerator.ToString());
  }
}