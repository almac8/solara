using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
  private void OnMouseDown() {
    Debug.Log("Unit Selected: " + gameObject.name);
  }
}