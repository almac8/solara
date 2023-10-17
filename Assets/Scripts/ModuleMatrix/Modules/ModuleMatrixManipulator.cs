using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleMatrixManipulator : Module {
  private UIManager uiManager;

  private void Awake() {
    Title = "Module Matrix Manipulator";
    Description = "Can edit a units Module Matrix";
    Activator = new ModuleActivator(false, "Close Module Matrix", "Module Matrix");
    Activator.Activated += ActivateModuleMatrix;
    uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
  }

  private void ActivateModuleMatrix() {
    uiManager.EnableModuleMatrixUI();
  }

  public override void RunStep(float deltaTime) {}
}