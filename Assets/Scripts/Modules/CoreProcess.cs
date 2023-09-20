using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreProcess : Module {
  public float standbyPowerConsumption;
  
  private PowerStorage powerStorage;

  private void Start() {
    powerStorage = gameObject.GetComponent<PowerStorage>();
  }

  private void RunStep(deltaTime) {
    if(!powerStorage.DrainCharge(standbyPowerConsumption * deltaTime)) {
      Debug.Log("Systems Shutdown");
    }
  }
}