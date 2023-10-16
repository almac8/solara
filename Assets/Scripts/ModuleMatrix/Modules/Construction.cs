using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : Module {
  [SerializeField] private ConstructionManager constructionManager;

  private void Awake() {
    Title = "Construction Module";
    Description = "It can build stuff";
    Activator = new ModuleActivator(false, "Stop Construction", "Start Construction");
    Activator.Activated += constructionManager.EnableConstructionMode;
    Activator.Deactivated += constructionManager.DisableConstructionMode;
  }

  public override void RunStep(float deltaTime) {}
}