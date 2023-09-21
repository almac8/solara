using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopographyScanner : Module {
  public bool IsScanning { get; private set; }
  public float ScanCompletion { get; private set; }

  public float powerRequired;
  public float scanRate;
  public float completeScanDataSize;
  
  private PowerStorage powerStorage;
  private DataStorageModule dataStorageModule;

  private void Start() {
    powerStorage = gameObject.GetComponent<PowerStorage>();
    //  dataStorageModule = 
  }

  public void ToggleScanning() {
    IsScanning = !IsScanning;
  }

  public void RunStep(float deltaTime) {
    if(IsScanning) {
      float samplePowerRequirement = powerRequired * deltaTime;

      if(powerStorage.DrainCharge(samplePowerRequirement)) {
        float scanSampleCompletion = scanRate * Time.deltaTime;
        float sampleDataRequirement = scanSampleCompletion * completeScanDataSize;

        if(dataStorageModule.WriteData(sampleDataRequirement)) {
          ScanCompletion = Mathf.Clamp(ScanCompletion + scanSampleCompletion, 0f, 1f);
          if(ScanCompletion == 1f) ToggleScanning();
        } else {
          powerStorage.SupplyCharge(samplePowerRequirement);
        }
      }
    }
  }
}