using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Probe : MonoBehaviour {
  [SerializeField] private float coreStandbyPowerConsumption;

  [SerializeField] private float powerModuleCharge;
  [SerializeField] private float powerModuleMaxCharge;

  [SerializeField] private bool solarPanelModuleIsDeployed;
  [SerializeField] private float solarPanelModuleRechargeRate;
  [SerializeField] private float solarPanelModuleRechargeEfficiency;

  [SerializeField] private bool topographyScanComponentIsSampling;
  [SerializeField] private float topographyScanComponentPowerRequired;
  
  [SerializeField] private GameObject probeUI;
  [SerializeField] private TMP_Text powerText;

  private float powerDelta;

  private void Update() {
    float powerCharged = 0.0f;
    float powerDisCharged = 0.0f;

    if(solarPanelModuleIsDeployed) {
      powerCharged += solarPanelModuleRechargeRate * solarPanelModuleRechargeEfficiency * Time.deltaTime;
    }

    powerDisCharged += coreStandbyPowerConsumption * Time.deltaTime;

    if(topographyScanComponentIsSampling) {
      float samplePowerRequirement = topographyScanComponentPowerRequired * Time.deltaTime;
      //  Calculate Data Requirement

      if(powerModuleCharge - samplePowerRequirement > 0f /* && dataStorageAvailable*/) {
        powerDisCharged += samplePowerRequirement;
        //  Increase Sample Data Storage Size
      }
    }

    powerDelta = powerCharged - powerDisCharged;
    powerModuleCharge = Mathf.Clamp(powerModuleCharge + powerDelta, 0.0f, powerModuleMaxCharge);
  }

  private void FixedUpdate() {
    powerText.text = "Power: " + powerModuleCharge.ToString() + "/" + powerModuleMaxCharge.ToString() + " (" + powerDelta.ToString() + ")";
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
    topographyScanComponentIsSampling = !topographyScanComponentIsSampling;
  }
}