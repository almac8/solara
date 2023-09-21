using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleMatrix : MonoBehaviour {
  [SerializeField] private PowerStorage powerStorage;
  [SerializeField] private CoreProcess coreProcess;
  [SerializeField] private TopographyScanner topographyScanner;
  [SerializeField] private SolarPanel solarPanel;

  private DataStorageModule dataStorageModule;
  private DroneDockModule droneDockModule;

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

    dataStorageModule = new DataStorageModule(100f);

    droneDockModule = new DroneDockModule();
    //  droneDockModule.SetTopographyScannerModule(topographyScannerModule);
  }

  private void Update() {
    coreProcess.RunStep(Time.deltaTime);
    solarPanel.RunStep(Time.deltaTime);
    topographyScanner.RunStep(Time.deltaTime);
    powerStorage.RunStep();

    dataStorageModule.Update();
  }
}