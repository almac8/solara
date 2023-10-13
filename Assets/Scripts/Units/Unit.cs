using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ModuleMatrix))]
public class Unit : Selectable {
  [SerializeField] private HUD hud;

  public ModuleMatrix GetModuleMatrix() {
    return gameObject.GetComponent<ModuleMatrix>();
  }

  protected override void Selected() {
    hud.gameObject.SetActive(true);
  }

  //  protected virtual void Deselected() {}
}