using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionPlanner : Module {
  [SerializeField] private Hexmap hexmap;

  private void Awake() {
    Title = "Construction Planner";
    Description = "Plan out your building before placing them";

    Activator = new ModuleActivator(false, "Close Construction", "Construction Planner");
    Activator.Activated += ActivateConstructionPlanner;
    Activator.Deactivated += DeactivateConstructionPlanner;
  }

  private void ActivateConstructionPlanner() {
    hexmap.gameObject.SetActive(true);
  }

  private void DeactivateConstructionPlanner() {
    hexmap.gameObject.SetActive(false);
  }
}