using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour {
  private bool hovered = false;
  private Vector3 hoveredPosition;
  private Vector3 unhoveredPosition;
  private Hexmap hexmap;

  private void Awake() {
    unhoveredPosition = transform.position;
    hoveredPosition = unhoveredPosition;
    hoveredPosition.y = 0.1f;
    hexmap = transform.parent.GetComponent<Hexmap>();
  }

  private void OnMouseEnter() {
    hovered = true;
    hexmap.TileHovered(unhoveredPosition);
  }
  
  private void OnMouseExit() {
    hovered = false;
  }

  private void OnMouseDown() {
    hexmap.TileClicked(unhoveredPosition);
  }

  private void Update() {
    transform.position = hovered ? hoveredPosition : unhoveredPosition;
  }
}