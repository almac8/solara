using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SelectionManager {
  public static Unit SelectedUnit { get; private set; }
  public static Resource SelectedResource { get; private set; }


  public static void Select(Selectable selected) {
    if(selected is Unit) {
      SelectedUnit = selected as Unit;
    } else if(selected is Resource) {
      SelectedResource = selected as Resource;
    }
  }

  public static void DeselectAll() {
    SelectedUnit = null;
    SelectedResource = null;
  }
}