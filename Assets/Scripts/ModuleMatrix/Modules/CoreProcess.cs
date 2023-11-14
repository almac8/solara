using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreProcess : Module {
  private const int POWER_STORAGE_INDEX = 0;
  [SerializeField] private float standbyPowerConsumption;

  private void Start() {
    Title = "Core Process";
    Description = "\"Solara's CPU - Don't Let the Lights Go Out!\" Keep the energy flowing, or Solara might decide it's naptime.";

    ModuleRequirement requirement = new ModuleRequirement();
    requirement.SetRequiredModule<PowerStorage>("Power Storage");
    Requirements.Add(requirement);

    GameManager.Instance.UnitManager.RegisterCoreProcessUnit(GetComponent<Unit>());
  }

  public override void RunStep(float deltaTime) {
    PowerStorage powerStorage = Requirements[POWER_STORAGE_INDEX].AssociatedModule as PowerStorage;

    if(powerStorage != null) {
      if(!powerStorage.DrainCharge(standbyPowerConsumption * deltaTime)) {
        Debug.Log("Systems Shutdown");
      }
    }
  }
}