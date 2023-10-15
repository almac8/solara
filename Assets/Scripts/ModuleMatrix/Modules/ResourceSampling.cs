using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSampling : Module {
  [SerializeField] private float sampleSize;
  [SerializeField] private float samplingRate;

  private Targeting targeting;

  private void Awake() {
    Title = "Resource Sampling";
    Description = "Collects samples of resources";
    Activator = new ModuleActivator(false, "Cancel Sample", "Collect Sample");
    Gauge = new ModuleGauge(0f, sampleSize, "Sample");
    targeting = GetComponent<Targeting>();
  }

  public override void RunStep(float deltaTime) {
    if(Activator.IsActive && targeting.IsLockedOn) {
      Gauge.Value += samplingRate * deltaTime;

      if(Gauge.Value > sampleSize) {
        Gauge.Value = sampleSize;
        Activator.Toggle();
        //  Store the sample in physical sorage
      }
    }
  }
}