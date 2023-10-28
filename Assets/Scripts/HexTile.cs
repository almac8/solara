using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour {
  private void OnMouseEnter() {
    transform.Translate(Vector3.up * 0.1f);
  }
  
  private void OnMouseExit() {
    transform.Translate(Vector3.up * -0.1f);
  }
}