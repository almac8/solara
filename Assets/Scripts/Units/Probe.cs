using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Probe : Unit {
  [SerializeField] private ModuleMatrix moduleMatrix;

  private PowerStorage powerStorage;
  private DataStorage dataStorage;
  private SolarPanel solarPanel;
  private TopographyScanner topographyScanner;
  private DroneDock droneDock;

  [SerializeField] private TMP_Text powerText;
  [SerializeField] private TMP_Text dataText;

  private void Start() {
    powerStorage = moduleMatrix.AddModule(ModuleMatrix.ModuleType.POWER_STORAGE) as PowerStorage;
    powerStorage.charge = 100f;
    powerStorage.chargeCapacity = 100f;

    CoreProcess coreProcess = moduleMatrix.AddModule(ModuleMatrix.ModuleType.CORE_PROCESS) as CoreProcess;
    coreProcess.standbyPowerConsumption = 0.001f;

    topographyScanner = moduleMatrix.AddModule(ModuleMatrix.ModuleType.TOPOGRAPHY_SCANNER) as TopographyScanner;
    topographyScanner.powerRequired = 2f;
    topographyScanner.scanRate = 0.01f;
    topographyScanner.completeScanDataSize = 5f;

    solarPanel = moduleMatrix.AddModule(ModuleMatrix.ModuleType.SOLAR_PANEL) as SolarPanel;
    solarPanel.rechargeRate = 1f;
    solarPanel.rechargeEfficiency = 1f;

    dataStorage = moduleMatrix.AddModule(ModuleMatrix.ModuleType.DATA_STORAGE) as DataStorage;
    dataStorage.storageCapacity = 100f;

    droneDock = moduleMatrix.AddModule(ModuleMatrix.ModuleType.DRONE_DOCK) as DroneDock;

    moduleMatrix.AddModule(ModuleMatrix.ModuleType.KNOWLEDGE_DATABASE);
  }

  private void FixedUpdate() {
    powerText.text = "Power: " + powerStorage.GetStatusString();
    dataText.text = "Data: " + dataStorage.GetStatusString();
  }

  public void ToggleSolarPanel() {
    solarPanel.isDeployed = !solarPanel.isDeployed;
  }

  public void ToggleTopographyScan() {
    topographyScanner.ToggleScanning();
  }

  public void ToggleDrone() {
    droneDock.droneIsDeployed = !droneDock.droneIsDeployed;
  }
}