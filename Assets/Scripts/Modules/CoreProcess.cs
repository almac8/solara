using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreProcess : Module {
  public float standbyPowerConsumption;
  public PowerStorage powerStorage;

  private void Start() {
    powerStorage = gameObject.GetComponent<PowerStorage>();
  }

  public override void RunStep(float deltaTime) {
    if(!powerStorage.DrainCharge(standbyPowerConsumption * deltaTime)) {
      Debug.Log("Systems Shutdown");
    }
  }
}