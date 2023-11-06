using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerStorage : Module {
  [SerializeField] private float chargeCapacity = 50;

  private float powerCharged;
  private float powerDisCharged;
  private float chargeDelta;

  public void Awake() {
    Title = "Power Storage";
    Description = "\"Power Stash - Because Even the Stars Need Batteries.\" Storing power for those moments when the universe forgets to provide it.";
    Gauge = new ModuleGauge(chargeCapacity, chargeCapacity, "Power");
  }

  public void SupplyCharge(float chargeSupplied) {
    powerCharged += chargeSupplied;
  }

  public bool DrainCharge(float chargeToDrain) {
    if(Gauge.Value - chargeToDrain < 0f) {
      return false;
    } else {
      powerDisCharged += chargeToDrain;
      return true;
    }
  }

  public override void RunStep(float deltaTime) {
    chargeDelta = powerCharged - powerDisCharged;
    Gauge.Value = Mathf.Clamp(Gauge.Value + chargeDelta, 0.0f, Gauge.MaxValue);
    Gauge.Title = $"Power: { Mathf.Round(Gauge.Value) }/{ Gauge.MaxValue } ({ chargeDelta })";

    powerCharged = 0f;
    powerDisCharged = 0f;
  }
}