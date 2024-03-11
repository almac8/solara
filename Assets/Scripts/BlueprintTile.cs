using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintTile : MonoBehaviour {
  public int XIndex { get; private set; }
  public int YIndex { get; private set; }
  
  private Blueprint blueprint;

  private void Start() {
    blueprint = transform.parent.gameObject.GetComponent<Blueprint>();
  }

  private void OnMouseDown() {
    blueprint.TileClicked(XIndex, YIndex);
  }

  public void SetIndex(int x, int y) {
    XIndex = x;
    YIndex = y;
  }
}