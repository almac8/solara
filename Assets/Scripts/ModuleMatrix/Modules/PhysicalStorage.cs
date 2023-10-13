using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalStorage : Module {
  [SerializeField] private float storageCapacity;
  
  private void Start() {
    Title = "Physical Storage";
    Description = "Stores Physical Stuff";
    Gauge = new ModuleGauge(0, storageCapacity, "Inventory");
  }

  public override void RunStep(float deltaTime) {}
}