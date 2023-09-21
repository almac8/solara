using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopographyScanner : Module {
  public bool isScanning;
  public float ScanCompletion { get; private set; }

  public float powerRequired;
  public float scanRate;
  public float completeScanDataSize;
  public PowerStorage powerStorage;
  public DataStorage dataStorage;

  private void Start() {
    powerStorage = gameObject.GetComponent<PowerStorage>();
    dataStorage = gameObject.GetComponent<DataStorage>();
  }

  public void ToggleScanning() {
    isScanning = !isScanning;
  }

  public override void RunStep(float deltaTime) {
    if(isScanning) {
      float samplePowerRequirement = powerRequired * deltaTime;

      if(powerStorage.DrainCharge(samplePowerRequirement)) {
        float scanSampleCompletion = scanRate * Time.deltaTime;
        float sampleDataRequirement = scanSampleCompletion * completeScanDataSize;

        if(dataStorage.WriteData(sampleDataRequirement)) {
          ScanCompletion = Mathf.Clamp(ScanCompletion + scanSampleCompletion, 0f, 1f);
          if(ScanCompletion == 1f) ToggleScanning();
        } else {
          powerStorage.SupplyCharge(samplePowerRequirement);
        }
      }
    }
  }
}