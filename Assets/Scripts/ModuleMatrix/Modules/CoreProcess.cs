using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreProcess : Module {
  public float standbyPowerConsumption;
  public PowerStorage powerStorage;

  private void Start() {
    Title = "Core Process";
    Description = "\"Solara's CPU - Don't Let the Lights Go Out!\" Keep the energy flowing, or Solara might decide it's naptime.";
    powerStorage = gameObject.GetComponent<PowerStorage>();
  }

  public override void RunStep(float deltaTime) {
    if(!powerStorage.DrainCharge(standbyPowerConsumption * deltaTime)) {
      Debug.Log("Systems Shutdown");
    }
  }
}