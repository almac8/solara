using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Probe : Unit {
  [SerializeField] private ModuleMatrix moduleMatrix;

  [SerializeField] private TMP_Text powerText;
  [SerializeField] private TMP_Text dataText;

  private void FixedUpdate() {
    //  powerText.text = "Power: " + powerModule.GetStatusString();
    //  dataText.text = "Data: " + dataStorageModule.GetStatusString();
  }

  private void OnMouseDown() {
    ShowUI();
  }

  public void ToggleSolarPanel() {
    //  solarPanelModule.ToggleIsDeployed();
  }

  public void ToggleTopographyScan() {
    //  topographyScannerModule.ToggleScanning();
  }

  public void ToggleDrone() {
    //  droneDockModule.ToggleDocked();
  }
}