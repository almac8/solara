using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreProcessModule {
  private float standbyPowerConsumption;

  private PowerModule powerModule;
  
  public CoreProcessModule(float standbyPowerConsumption) {
    this.standbyPowerConsumption = standbyPowerConsumption;
  }

  public void SetPowerModule(PowerModule powerModule) {
    this.powerModule = powerModule;
  }

  public void Update(float deltaTime) {
    if(!powerModule.DrainCharge(standbyPowerConsumption * deltaTime)) {
      Debug.Log("Systems Shutdown");
    }
  }
}