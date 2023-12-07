using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ModuleMatrix))]
public class Unit : Selectable {
  public bool isSelectable;

  private void Awake() {
    isSelectable = true;
  }

  public ModuleMatrix GetModuleMatrix() {
    return gameObject.GetComponent<ModuleMatrix>();
  }

  protected override void Selected() {
    if(isSelectable) {
      GameManager.Instance.UIManager.SetUI(UIManager.UILayout.HUD);
    } else {
      Deselect();
    }
  }
}