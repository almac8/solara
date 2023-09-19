using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerModule {
  private float charge;
  private float _chargeCapacity;
  
  private float powerCharged;
  private float powerDisCharged;
  private float chargeDelta;

  public PowerModule(float chargeCapacity) {
    _chargeCapacity = chargeCapacity;
    charge = _chargeCapacity;
  }
  
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

  public void Update() {
    chargeDelta = powerCharged - powerDisCharged;
    charge = Mathf.Clamp(charge + chargeDelta, 0.0f, _chargeCapacity);

    powerCharged = 0f;
    powerDisCharged = 0f;
  }

  public string GetStatusString() {
    return $"{ charge }/{ _chargeCapacity } ({ chargeDelta })";
  }
}