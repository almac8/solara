using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceMining : Module {
  private const int POWER_STORAGE_INDEX = 0;
  private const int PHYSICAL_STORAGE_INDEX = 1;

  private void Awake() {
    Title = "Surface Mining";
    Description = "Mines resources from the surface";
    Activator = new ModuleActivator(false, "Stop Mining", "Start Mining");

    ModuleRequirement powerRequirement = new ModuleRequirement();
    powerRequirement.SetRequiredModule<PowerStorage>("Power Storage");

    ModuleRequirement storageRequirement = new ModuleRequirement();
    storageRequirement.SetRequiredModule<PhysicalStorage>("Physical Storage");

    Requirements.Add(powerRequirement);
    Requirements.Add(storageRequirement);
  }

  public override void RunStep(float deltaTime) {
    if(!Activator.IsActive) return;
    
    PowerStorage power = Requirements[POWER_STORAGE_INDEX].AssociatedModule as PowerStorage;
    if(power == null) {
      Debug.Log("Surface Mining Module: Requires Power Module");
      Activator.Toggle();
      return;
    }

    if(!power.DrainCharge(0.1f)) {
      Debug.Log("Surface Mining Module: Power source is empty");
      Activator.Toggle();
      return;
    }

    float minedRegolith = 0.1f;
    Debug.Log("Mining");

    PhysicalStorage storage = Requirements[PHYSICAL_STORAGE_INDEX].AssociatedModule as PhysicalStorage;
    if(storage == null) {
      Debug.Log("Surface Mining Module: Requires Physical Storage Module");
      Activator.Toggle();
      return;
    }

    if(!storage.Add(minedRegolith)) {
      Debug.Log("Surface Mining Module: Physical Storage is full");
      Activator.Toggle();
      return;
    }
  }
}