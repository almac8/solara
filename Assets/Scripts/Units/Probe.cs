using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Probe : Unit {
  private CoreProcessModule coreProcessModule;
  private PowerModule powerModule;
  private SolarPanelModule solarPanelModule;
  private DataStorageModule dataStorageModule;
  private TopographyScannerModule topographyScannerModule;
  
  [SerializeField] private bool droneIsDeployed;
  [SerializeField] private GameObject drone;
  
  [SerializeField] private TMP_Text powerText;
  [SerializeField] private TMP_Text dataText;

  private void Awake() {
    powerModule = new PowerModule(100f);

    coreProcessModule = new CoreProcessModule(1f);
    coreProcessModule.SetPowerModule(powerModule);
    
    solarPanelModule = new SolarPanelModule(1f);
    solarPanelModule.SetPowerModule(powerModule);

    dataStorageModule = new DataStorageModule(100f);

    topographyScannerModule = new TopographyScannerModule(2f, 0.01f, 20f);
    topographyScannerModule.SetPowerModule(powerModule);
    topographyScannerModule.SetDataStorageModule(dataStorageModule);
  }

  private void Update() {
    coreProcessModule.Update(Time.deltaTime);
    solarPanelModule.Update(Time.deltaTime);
    topographyScannerModule.Update(Time.deltaTime);
    powerModule.Update();
    dataStorageModule.Update();
  }

  private void FixedUpdate() {
    powerText.text = "Power: " + powerModule.GetStatusString();
    dataText.text = "Data: " + dataStorageModule.GetStatusString();
  }

  private void OnMouseDown() {
    ShowUI();
  }

  public void ToggleSolarPanel() {
    solarPanelModule.ToggleIsDeployed();
  }

  public void ToggleTopographyScan() {
    topographyScannerModule.ToggleScanning();
  }

  public void ToggleDrone() {
    if(topographyScannerModule.ScanCompletion == 1f) {
      droneIsDeployed = !droneIsDeployed;
      drone.SetActive(droneIsDeployed);
    }
  }
}