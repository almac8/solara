using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Probe : MonoBehaviour {
  private PowerModule powerModule;
  private SolarPanelModule solarPanelModule;

  [SerializeField] private float coreStandbyPowerConsumption;

  [SerializeField] private float dataStorageUsed;
  [SerializeField] private float dataStorageCapacity;

  [SerializeField] private bool topographyScanComponentIsScanning;
  [SerializeField] private float topographyScanComponentPowerRequired;
  [SerializeField] private float topographyScanComponentScanCompletion;
  [SerializeField] private float topographyScanComponentScanRate;
  [SerializeField] private float topographyScanComponentCompleteScanDataSize;

  [SerializeField] private bool droneIsDeployed;
  [SerializeField] private GameObject drone;
  
  [SerializeField] private GameObject probeUI;
  [SerializeField] private TMP_Text powerText;
  [SerializeField] private TMP_Text dataText;

  private float dataDelta;

  private void Awake() {
    powerModule = new PowerModule(100);
    solarPanelModule = new SolarPanelModule(1);
  }

  private void Update() {
    float dataLoaded = 0.0f;
    float dataProcessed = 0.0f;

    if(solarPanelModule.IsDeployed) {
      float chargeSupplied = solarPanelModule.GetCharge(Time.deltaTime);
      powerModule.SupplyCharge(chargeSupplied);
    }

    if(!powerModule.DrainCharge(coreStandbyPowerConsumption * Time.deltaTime)) {
      Debug.Log("Systems Shutdown");
    }

    if(topographyScanComponentIsScanning) {
      float samplePowerRequirement = topographyScanComponentPowerRequired * Time.deltaTime;
      float scanSampleCompletion = topographyScanComponentScanRate * Time.deltaTime;
      float sampleDataRequirement = scanSampleCompletion * topographyScanComponentCompleteScanDataSize;

      if(powerModule.DrainCharge(samplePowerRequirement) && dataStorageUsed + sampleDataRequirement < dataStorageCapacity) {
        dataLoaded += sampleDataRequirement;

        topographyScanComponentScanCompletion = Mathf.Clamp(topographyScanComponentScanCompletion + scanSampleCompletion, 0f, 1f);
        if(topographyScanComponentScanCompletion >= 1f) ToggleTopographyScan();
      }
    }

    dataDelta = dataLoaded - dataProcessed;

    powerModule.Update();
    dataStorageUsed = Mathf.Clamp(dataStorageUsed + dataDelta, 0.0f, dataStorageCapacity);
  }

  private void FixedUpdate() {
    powerText.text = "Power: " + powerModule.GetStatusString();
    dataText.text = "Data: " + dataStorageUsed.ToString() + "/" + dataStorageCapacity.ToString() + " (" + dataDelta.ToString() + ")";
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
    topographyScanComponentIsScanning = !topographyScanComponentIsScanning;
  }

  public void ToggleDrone() {
    if(topographyScanComponentScanCompletion == 1f) {
      droneIsDeployed = !droneIsDeployed;
      drone.SetActive(droneIsDeployed);
    }
  }
}