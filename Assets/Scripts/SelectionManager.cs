using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SelectionManager {
  private static Unit selectedUnit;
  private static Resource selectedResource;

  public static void Select(Selectable selected) {
    if(selected is Unit) {
      selectedUnit = selected as Unit;
      Debug.Log("Unit Selected");
    } else if(selected is Resource) {
      selectedResource = selected as Resource;
      Debug.Log("Resource Selected");
    }
  }
}