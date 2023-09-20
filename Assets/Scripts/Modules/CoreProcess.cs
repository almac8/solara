using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreProcess : Module {
  public float standbyPowerConsumption;
  private PowerModule powerModule;

  private void Update() {
    if(!powerModule.DrainCharge(standbyPowerConsumption * Time.deltaTime)) {
      Debug.Log("Systems Shutdown");
    }
  }

  public void SetPowerModule(PowerModule powerModule) {
    this.powerModule = powerModule;
  }
}