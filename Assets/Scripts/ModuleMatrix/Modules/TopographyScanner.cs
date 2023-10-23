using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopographyScanner : Module {
  private const int POWER_STORAGE_INDEX = 0;
  private const int DATA_STORAGE_INDEX = 1;
  
  [SerializeField] private float scanRate;
  [SerializeField] private float powerRequired;
  [SerializeField] private float completeScanDataSize;

  public float ScanCompletion { get; private set; }

  private void Start() {
    Title = "Topography Scanner";
    Description = "\"MapMaster 5000 - Uncovering the Universe's Dirty Secrets.\" Bringing you the topographical dirt on every celestial body, one scan at a time.";
    Activator = new ModuleActivator(false, "Disable Topography Scanner", "Enable Topography Scanner");

    ModuleRequirement powerStorageRequirement = new ModuleRequirement();
    powerStorageRequirement.SetRequiredModule<PowerStorage>("Power Storage");

    ModuleRequirement dataStorageRequirement = new ModuleRequirement();
    dataStorageRequirement.SetRequiredModule<DataStorage>("Data Storaage");

    Requirements.Add(powerStorageRequirement);
    Requirements.Add(dataStorageRequirement);
  }
  
  public override void RunStep(float deltaTime) {
    if(Activator.IsActive) {
      PowerStorage powerStorage = Requirements[POWER_STORAGE_INDEX].AssociatedModule as PowerStorage;
      DataStorage dataStorage = Requirements[DATA_STORAGE_INDEX].AssociatedModule as DataStorage;

      if(powerStorage != null && dataStorage != null) {
        float samplePowerRequirement = powerRequired * deltaTime;

        if(powerStorage.DrainCharge(samplePowerRequirement)) {
          float scanSampleCompletion = scanRate * Time.deltaTime;
          float sampleDataRequirement = scanSampleCompletion * completeScanDataSize;

          if(dataStorage.WriteData(sampleDataRequirement)) {
            ScanCompletion = Mathf.Clamp(ScanCompletion + scanSampleCompletion, 0f, 1f);
            if(ScanCompletion == 1f) Activator.Toggle();
          } else {
            powerStorage.SupplyCharge(samplePowerRequirement);
          }
        }
      }
    }
  }
}