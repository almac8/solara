using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarPanel : Module {
  public float rechargeRate;
  public float rechargeEfficiency;
  public PowerStorage powerStorage;
  public Animator animator;

  private void Start() {
    Title = "Solar Panel";
    Description = "\"Solar Slurper - Sipping Sunshine Since Forever.\" Absorbing sunlight to keep our systems running, because why not harness the power of a giant, flaming ball of gas?";
    powerStorage = gameObject.GetComponent<PowerStorage>();
    Activator = new ModuleActivator(false, "Deactivate Solar Panel", "Activate Solar Panel");
  }

  public override void RunStep(float deltaTime) {
    float chargeSupplied = Activator.IsActive ? rechargeRate * rechargeEfficiency * deltaTime : 0f;
    powerStorage.SupplyCharge(chargeSupplied);

    animator.SetBool("is_deployed", Activator.IsActive);
  }
}