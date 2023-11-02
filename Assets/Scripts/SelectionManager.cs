using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SelectionManager {
  public static Unit SelectedUnit { get; private set; }
  public static Resource SelectedResource { get; private set; }


  public static void Select(Selectable selected) {
    if(selected is Unit) {
      if(SelectedUnit != null) SelectedUnit.Deselect();
      Unit selectedAsUnit = selected as Unit;
      if(selectedAsUnit.IsSelectable) SelectedUnit = selectedAsUnit;
    } else if(selected is Resource) {
      if(SelectedUnit != null) {
        if(SelectedResource != null) SelectedResource.Deselect();
        SelectedResource = selected as Resource;
      }
    }
  }

  public static void DeselectAll() {
    DeselectUnit();
    DeselectResource();
  }

  public static void DeselectUnit() {
    SelectedUnit?.Deselect();
    SelectedUnit = null;
  }

  public static void DeselectResource() {
    SelectedResource?.Deselect();
    SelectedResource = null;
  }
}