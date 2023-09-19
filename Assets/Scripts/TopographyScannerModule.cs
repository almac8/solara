using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopographyScannerModule {
  public bool IsScanning { get; private set; }
  public float ScanCompletion { get; private set; }
  
  private float powerRequired;
  private float scanRate;
  private float completeScanDataSize;

  private PowerModule powerModule;
  private DataStorageModule dataStorageModule;

  public TopographyScannerModule(float powerRequired, float scanRate, float completeScanDataSize) {
    this.powerRequired = powerRequired;
    this.scanRate = scanRate;
    this.completeScanDataSize = completeScanDataSize;
  }

  public void ToggleScanning() {
    IsScanning = !IsScanning;
  }

  public void SetPowerModule(PowerModule powerModule) {
    this.powerModule = powerModule;
  }

  public void SetDataStorageModule(DataStorageModule dataStorageModule) {
    this.dataStorageModule = dataStorageModule;
  }

  public void Update(float deltaTime) {
    if(IsScanning) {
      float samplePowerRequirement = powerRequired * deltaTime;

      if(powerModule.DrainCharge(samplePowerRequirement)) {
        float scanSampleCompletion = scanRate * Time.deltaTime;
        float sampleDataRequirement = scanSampleCompletion * completeScanDataSize;

        if(dataStorageModule.WriteData(sampleDataRequirement)) {
          ScanCompletion = Mathf.Clamp(ScanCompletion + scanSampleCompletion, 0f, 1f);
          if(ScanCompletion == 1f) ToggleScanning();
        } else {
          powerModule.SupplyCharge(samplePowerRequirement);
        }
      }
    }
  }
}