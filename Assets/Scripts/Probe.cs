using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Probe : MonoBehaviour {
  [SerializeField] private float processorModulePowerConsumption = 1.0f;
  [SerializeField] private float powerModuleMaxCharge = 100.0f;
  [SerializeField] private float powerModuleCharge = 50.0f;
  [SerializeField] private bool processorModuleIsProcessing = false;
  [SerializeField] private float solarPanelModuleRechargeRate = 0.5f;
  [SerializeField] private float solarPanelModuleRechargeEfficiency = 1.0f;
  [SerializeField] private TMP_Text selectedUnitText;

  private void Update() {
    if(powerModuleCharge < powerModuleMaxCharge) {
      powerModuleCharge += solarPanelModuleRechargeRate * solarPanelModuleRechargeEfficiency * Time.deltaTime;

      if(powerModuleCharge > powerModuleMaxCharge) {
        powerModuleCharge = powerModuleMaxCharge;
      }
    }

    if(powerModuleCharge > 0.0f) {
      if(processorModuleIsProcessing) {
        powerModuleCharge -= processorModulePowerConsumption * Time.deltaTime;
      }
    } else {
      powerModuleCharge = 0.0f;
    }
  }

  private void OnMouseDown() {
    selectedUnitText.text = "Probe";
  }
}