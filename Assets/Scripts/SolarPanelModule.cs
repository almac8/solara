using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarPanelModule {
  public bool IsDeployed { get; private set; }

  private float rechargeRate;
  private float rechargeEfficiency;

  public SolarPanelModule(float rechargeRate) {
    this.rechargeRate = rechargeRate;
    rechargeEfficiency = 1f;
  }

  public float GetCharge(float deltaTime) {
    if(IsDeployed) {
      return rechargeRate * rechargeEfficiency * deltaTime;
    } else {
      return 0f;
    }
  }

  public void ToggleIsDeployed() {
    IsDeployed = !IsDeployed;
  }
}
