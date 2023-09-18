using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Probe : MonoBehaviour {
  [SerializeField] private float coreStandbyPowerConsumption;

  [SerializeField] private float powerModuleCharge;
  [SerializeField] private float powerModuleChargeCapacity;

  [SerializeField] private bool solarPanelModuleIsDeployed;
  [SerializeField] private float solarPanelModuleRechargeRate;
  [SerializeField] private float solarPanelModuleRechargeEfficiency;

  [SerializeField] private float dataStorageUsed;
  [SerializeField] private float dataStorageCapacity;

  [SerializeField] private bool topographyScanComponentIsScanning;
  [SerializeField] private float topographyScanComponentPowerRequired;
  [SerializeField] private float topographyScanComponentScanCompletion;
  [SerializeField] private float topographyScanComponentScanRate;
  [SerializeField] private float topographyScanComponentCompleteScanDataSize;
  
  [SerializeField] private GameObject probeUI;
  [SerializeField] private TMP_Text powerText;
  [SerializeField] private TMP_Text dataText;

  private float powerDelta;
  private float dataDelta;

  private void Update() {
    float powerCharged = 0.0f;
    float powerDisCharged = 0.0f;
    float dataLoaded = 0.0f;
    float dataProcessed = 0.0f;

    if(solarPanelModuleIsDeployed) {
      powerCharged += solarPanelModuleRechargeRate * solarPanelModuleRechargeEfficiency * Time.deltaTime;
    }

    powerDisCharged += coreStandbyPowerConsumption * Time.deltaTime;

    if(topographyScanComponentIsScanning) {
      float samplePowerRequirement = topographyScanComponentPowerRequired * Time.deltaTime;
      float scanSampleCompletion = topographyScanComponentScanRate * Time.deltaTime;
      float sampleDataRequirement = scanSampleCompletion * topographyScanComponentCompleteScanDataSize;

      if(powerModuleCharge - samplePowerRequirement > 0f && dataStorageUsed + sampleDataRequirement < dataStorageCapacity) {
        powerDisCharged += samplePowerRequirement;
        dataLoaded += sampleDataRequirement;

        topographyScanComponentScanCompletion = Mathf.Clamp(topographyScanComponentScanCompletion + scanSampleCompletion, 0f, 1f);
        if(topographyScanComponentScanCompletion >= 1f) ToggleTopographyScan();
      }
    }

    powerDelta = powerCharged - powerDisCharged;
    dataDelta = dataLoaded - dataProcessed;

    powerModuleCharge = Mathf.Clamp(powerModuleCharge + powerDelta, 0.0f, powerModuleChargeCapacity);
    dataStorageUsed = Mathf.Clamp(dataStorageUsed + dataDelta, 0.0f, dataStorageCapacity);
  }

  private void FixedUpdate() {
    powerText.text = "Power: " + powerModuleCharge.ToString() + "/" + powerModuleChargeCapacity.ToString() + " (" + powerDelta.ToString() + ")";
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
    solarPanelModuleIsDeployed = !solarPanelModuleIsDeployed;
  }

  public void ToggleTopographyScan() {
    topographyScanComponentIsScanning = !topographyScanComponentIsScanning;
  }
}