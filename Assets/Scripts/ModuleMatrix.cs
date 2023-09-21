using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleMatrix : MonoBehaviour {
  [SerializeField] private PowerStorage powerStorage;
  [SerializeField] private CoreProcess coreProcess;
  [SerializeField] private TopographyScanner topographyScanner;
  [SerializeField] private SolarPanel solarPanel;
  [SerializeField] private DataStorage dataStorage;
  [SerializeField] private DroneDock droneDock;

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
    coreProcess.RunStep(Time.deltaTime);
    solarPanel.RunStep(Time.deltaTime);
    topographyScanner.RunStep(Time.deltaTime);
    powerStorage.RunStep();
    dataStorage.RunStep();
    droneDock.RunStep();
  }
}