using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Selectable : MonoBehaviour {
  public bool IsSelected {
    get;
    private set;
  }

  private void OnMouseDown() {
    Select();
  }

  private void Select() {
    IsSelected = true;
    SelectionManager.Select(gameObject);
    Selected();
  }

  private void Deselect() {
    IsSelected = false;
    Deselected();
  }

  protected virtual void Selected() {}
  protected virtual void Deselected() {}
}