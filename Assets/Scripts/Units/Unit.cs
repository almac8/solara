using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ModuleMatrix))]
public class Unit : Selectable {
  public bool IsSelectable { get; private set; }

  private void Start() {
    IsSelectable = true;
  }

  public ModuleMatrix GetModuleMatrix() {
    return gameObject.GetComponent<ModuleMatrix>();
  }

  protected override void Selected() {
    GameManager.Instance.UIManager.SetUI(UIManager.UILayout.HUD);
  }

  //  protected virtual void Deselected() {}

  public void SetSelectable(bool canSelect) {
    IsSelectable = canSelect;
  }
}