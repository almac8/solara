using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleMatrixManipulator : Module {
  private void Awake() {
    Title = "Module Matrix Manipulator";
    Description = "Can edit a units Module Matrix";
    Activator = new ModuleActivator(false, "Close Module Matrix", "Module Matrix");
    Activator.Activated += ActivateModuleMatrix;
  }

  private void ActivateModuleMatrix() {
    GameManager.Instance.UIManager.SetUI(UIManager.UILayout.MODULE_MATRIX);
  }

  public override void RunStep(float deltaTime) {}
}