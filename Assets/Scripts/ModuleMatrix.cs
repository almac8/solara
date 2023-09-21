using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleMatrix : MonoBehaviour {
  private PowerStorage powerStorage;
  private CoreProcess coreProcess;
  private TopographyScanner topographyScanner;
  private SolarPanel solarPanel;
  private DataStorage dataStorage;
  private DroneDock droneDock;

  private void Awake() {
    powerStorage = gameObject.AddComponent<PowerStorage>();
    powerStorage.charge = 100f;
    powerStorage.chargeCapacity = 100f;

    coreProcess = gameObject.AddComponent<CoreProcess>();
    coreProcess.standbyPowerConsumption = 0.001f;

    topographyScanner = gameObject.AddComponent<TopographyScanner>();
    topographyScanner.powerRequired = 2f;
    topographyScanner.scanRate = 0.01f;
    topographyScanner.completeScanDataSize = 5f;

    solarPanel = gameObject.AddComponent<SolarPanel>();
    solarPanel.rechargeRate = 1f;
    solarPanel.rechargeEfficiency = 1f;

    dataStorage = gameObject.AddComponent<DataStorage>();
    dataStorage.storageCapacity = 100f;

    droneDock = gameObject.AddComponent<DroneDock>();
  }

  private void Update() {
    float deltaTime = Time.deltaTime;

    coreProcess.RunStep(deltaTime);
    solarPanel.RunStep(deltaTime);
    topographyScanner.RunStep(deltaTime);
    powerStorage.RunStep(deltaTime);
    dataStorage.RunStep(deltaTime);
    droneDock.RunStep(deltaTime);
  }
  
  public string GetPowerStatus() {
    return powerStorage.GetStatusString();
  }

  public string GetDataStorageStatus() {
    return dataStorage.GetStatusString();
  }
}