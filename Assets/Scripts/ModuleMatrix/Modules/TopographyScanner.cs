using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopographyScanner : Module {
  public float ScanCompletion { get; private set; }

  public float powerRequired;
  public float scanRate;
  public float completeScanDataSize;
  public PowerStorage powerStorage;
  public DataStorage dataStorage;

  private void Start() {
    Title = "Topography Scanner";
    Description = "\"MapMaster 5000 - Uncovering the Universe's Dirty Secrets.\" Bringing you the topographical dirt on every celestial body, one scan at a time.";
    Activator = new ModuleActivator(false, "Disable Topography Scanner", "Enable Topography Scanner");
    powerStorage = gameObject.GetComponent<PowerStorage>();
    dataStorage = gameObject.GetComponent<DataStorage>();
  }
  
  public override void RunStep(float deltaTime) {
    if(Activator.IsActive) {
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