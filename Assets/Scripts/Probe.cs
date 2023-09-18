using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Probe : MonoBehaviour {
  [SerializeField] private float powerModuleCharge = 50.0f;
  [SerializeField] private float powerModuleMaxCharge = 100.0f;

  [SerializeField] private float solarPanelModuleRechargeRate = 0.5f;
  [SerializeField] private float solarPanelModuleRechargeEfficiency = 1.0f;

  [SerializeField] private float processorModulePowerConsumption = 1.0f;
  [SerializeField] private bool processorModuleIsProcessing = false;
  
  [SerializeField] private GameObject probeUI;
  [SerializeField] private TMP_Text powerText;

  private float powerDelta;

  private void Update() {
    float powerCharged = 0.0f;
    float powerDisCharged = 0.0f;

    powerCharged = solarPanelModuleRechargeRate * solarPanelModuleRechargeEfficiency * Time.deltaTime;

    if(processorModuleIsProcessing) {
      powerDisCharged = processorModulePowerConsumption * Time.deltaTime;
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
}