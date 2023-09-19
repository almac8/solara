using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Probe : MonoBehaviour {
  [SerializeField] private float coreStandbyPowerConsumption;

  private PowerModule powerModule;
  private SolarPanelModule solarPanelModule;
  private DataStorageModule dataStorageModule;
  private TopographyScannerModule topographyScannerModule;
  
  [SerializeField] private bool droneIsDeployed;
  [SerializeField] private GameObject drone;
  
  [SerializeField] private GameObject probeUI;
  [SerializeField] private TMP_Text powerText;
  [SerializeField] private TMP_Text dataText;

  private void Awake() {
    powerModule = new PowerModule(100f);
    solarPanelModule = new SolarPanelModule(1f);
    dataStorageModule = new DataStorageModule(100f);

    topographyScannerModule = new TopographyScannerModule(2f, 0.01f, 20f);
    topographyScannerModule.SetPowerModule(powerModule);
    topographyScannerModule.SetDataStorageModule(dataStorageModule);
  }

  private void Update() {
    if(solarPanelModule.IsDeployed) {
      float chargeSupplied = solarPanelModule.GetCharge(Time.deltaTime);
      powerModule.SupplyCharge(chargeSupplied);
    }

    if(!powerModule.DrainCharge(coreStandbyPowerConsumption * Time.deltaTime)) {
      Debug.Log("Systems Shutdown");
    }
    
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

  private void ShowUI() {
    probeUI.SetActive(true);
  }

  public void HideUI() {
    probeUI.SetActive(false);
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