using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarPanelModule {
  public bool IsDeployed { get; private set; }

  private float rechargeRate;
  private float rechargeEfficiency;

  private PowerModule powerModule;

  public SolarPanelModule(float rechargeRate) {
    this.rechargeRate = rechargeRate;
    rechargeEfficiency = 1f;
  }

  public void Update(float deltaTime) {
    if(IsDeployed) {
      float chargeSupplied = GetCharge(deltaTime);
      powerModule.SupplyCharge(chargeSupplied);
    }
  }

  public void SetPowerModule(PowerModule powerModule) {
    this.powerModule = powerModule;
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