using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarPanel : Module {
  private const int POWER_STORAGE_INDEX = 0;
  
  [SerializeField] private float rechargeRate;
  [SerializeField] private float rechargeEfficiency;

  private Animator animator;

  private void Start() {
    Title = "Solar Panel";
    Description = "\"Solar Slurper - Sipping Sunshine Since Forever.\" Absorbing sunlight to keep our systems running, because why not harness the power of a giant, flaming ball of gas?";
    Activator = new ModuleActivator(false, "Deactivate Solar Panel", "Activate Solar Panel");

    ModuleRequirement requirement = new ModuleRequirement();
    requirement.SetRequiredModule<PowerStorage>("Power Storage");
  }

  public override void RunStep(float deltaTime) {
    if(Activator.IsActive) {
      PowerStorage powerStorage = Requirements[POWER_STORAGE_INDEX].AssociatedModule as PowerStorage;
      float chargeSupplied = 0f;

      if(powerStorage != null) {
        chargeSupplied = rechargeRate * rechargeEfficiency * deltaTime;
        powerStorage.SupplyCharge(chargeSupplied);
      }
      
      animator.SetBool("is_deployed", Activator.IsActive);
    }
  }
}