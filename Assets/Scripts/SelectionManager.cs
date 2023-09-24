using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SelectionManager {
  private static GameObject selectedObject;

  public static void Select(GameObject gameObject) {
    selectedObject = gameObject;
    Debug.Log("Selection Manager: " + gameObject.name);
  }
}