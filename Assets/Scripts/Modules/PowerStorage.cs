using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerStorage : Module {
  public float charge;
  public float chargeCapacity;
  
  private float powerCharged;
  private float powerDisCharged;
  private float chargeDelta;

  public void SupplyCharge(float chargeSupplied) {
    powerCharged += chargeSupplied;
  }

  public bool DrainCharge(float chargeToDrain) {
    if(charge - chargeToDrain < 0f) {
      return false;
    } else {
      powerDisCharged += chargeToDrain;
      return true;
    }
  }

  public override void RunStep(float deltaTime) {
    chargeDelta = powerCharged - powerDisCharged;
    charge = Mathf.Clamp(charge + chargeDelta, 0.0f, chargeCapacity);

    powerCharged = 0f;
    powerDisCharged = 0f;
  }

  public string GetStatusString() {
    return $"{ charge }/{ chargeCapacity } ({ chargeDelta })";
  }
}