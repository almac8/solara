using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSampling : Module {
  private const int TARGETING_INDEX = 0;
  
  [SerializeField] private float sampleSize;
  [SerializeField] private float samplingRate;

  private void Awake() {
    Title = "Resource Sampling";
    Description = "Collects samples of resources";
    Activator = new ModuleActivator(false, "Cancel Sample", "Collect Sample");
    Gauge = new ModuleGauge(0f, sampleSize, "Sample");

    ModuleRequirement requirement = new ModuleRequirement();
    requirement.SetRequiredModule<Targeting>("Targeting");
  }

  public override void RunStep(float deltaTime) {
    if(Activator.IsActive) {
      Targeting targeting = Requirements[TARGETING_INDEX].AssociatedModule as Targeting;

      if(targeting != null && targeting.IsLockedOn) {
        Gauge.Value += samplingRate * deltaTime;

        if(Gauge.Value > sampleSize) {
          Gauge.Value = sampleSize;
          Activator.Toggle();
          //  Store the sample in physical sorage
        }
      }
    }
  }
}