using UnityEngine;

public class SelectionManager : MonoBehaviour {
  public Unit SelectedUnit { get; private set; }
  public Resource SelectedResource { get; private set; }

  public static void Select(Selectable selected) {
    SelectionManager self = GameManager.Instance.SelectionManager;

    if(selected is Unit) {
      if(self.SelectedUnit != null) self.SelectedUnit.Deselect();
      Unit selectedAsUnit = selected as Unit;
      if(selectedAsUnit.isSelectable) self.SelectedUnit = selectedAsUnit;
    } else if(selected is Resource) {
      if(self.SelectedUnit != null) {
        if(self.SelectedResource != null) self.SelectedResource.Deselect();
        self.SelectedResource = selected as Resource;
      }
    }
  }

  public void DeselectAll() {
    DeselectUnit();
    DeselectResource();
  }

  public void DeselectUnit() {
    SelectedUnit?.Deselect();
    SelectedUnit = null;
  }

  public void DeselectResource() {
    SelectedResource?.Deselect();
    SelectedResource = null;
  }
}