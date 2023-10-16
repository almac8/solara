using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleMatrixManipulator : Module {
  [SerializeField] private GameObject moduleMatrixUI;

  private void Awake() {
    Title = "Module Matrix Manipulator";
    Description = "Can edit a units Module Matrix";
    Activator = new ModuleActivator(false, "Close Module Matrix", "Module Matrix");
    Activator.Activated += ActivateModuleMatrix;
  }

  private void ActivateModuleMatrix() {
    moduleMatrixUI.SetActive(true);
  }

  public override void RunStep(float deltaTime) {}
}