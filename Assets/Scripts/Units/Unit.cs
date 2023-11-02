using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ModuleMatrix))]
public class Unit : Selectable {
  private UIManager uiManager;

  public bool IsSelectable { get; private set; }

  private void Start() {
    uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
    IsSelectable = true;
  }

  public ModuleMatrix GetModuleMatrix() {
    return gameObject.GetComponent<ModuleMatrix>();
  }

  protected override void Selected() {
    uiManager.EnableHUD();
  }

  //  protected virtual void Deselected() {}

  public void SetSelectable(bool canSelect) {
    IsSelectable = canSelect;
  }
}