using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour {
  private VisualElement root;
  private Label unitLabel;
  private Label resourceLabel;

  private void Start() {
    root = GetComponent<UIDocument>().rootVisualElement;
    unitLabel = root.Q<Label>("unit_label");
    resourceLabel = root.Q<Label>("resource_label");
  }

  private void Update() {
    if(SelectionManager.SelectedUnit == null) {
      unitLabel.text = "";
      unitLabel.visible = false;
    } else {
      unitLabel.text = SelectionManager.SelectedUnit.gameObject.name;
      unitLabel.visible = true;
    }

    if(SelectionManager.SelectedResource == null) {
      resourceLabel.text = "";
      resourceLabel.visible = false;
    } else {
      resourceLabel.text = SelectionManager.SelectedResource.gameObject.name;
      resourceLabel.visible = true;
    }

    if(Input.GetButtonDown("Cancel")) {
      SelectionManager.DeselectAll();
    }
  }
}