using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarPanel : Module {
  public bool isDeployed;
  public float rechargeRate;
  public float rechargeEfficiency;
  public PowerStorage powerStorage;
  public Animator animator;

  private void Start() {
    Title = "Solar Panel";
    powerStorage = gameObject.GetComponent<PowerStorage>();
  }

  public override void RunStep(float deltaTime) {
    float chargeSupplied = isDeployed ? rechargeRate * rechargeEfficiency * deltaTime : 0f;
    powerStorage.SupplyCharge(chargeSupplied);

    animator.SetBool("is_deployed", isDeployed);
  }
}