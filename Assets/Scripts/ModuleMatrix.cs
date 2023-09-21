using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleMatrix : MonoBehaviour {
  [SerializeField] private CoreProcess coreProcess;

  private SolarPanelModule solarPanelModule;
  private DataStorageModule dataStorageModule;
  private TopographyScannerModule topographyScannerModule;
  private DroneDockModule droneDockModule;

  private void Awake() {
    PowerStorage powerStorage = gameObject.AddComponent<PowerStorage>();
    //  powerModule = new PowerModule(100f);

    coreProcess = gameObject.AddComponent<CoreProcess>();
    coreProcess.standbyPowerConsumption = 0.001f;
    
    solarPanelModule = new SolarPanelModule(1f);
    //  solarPanelModule.SetPowerModule(powerModule);

    dataStorageModule = new DataStorageModule(100f);

    topographyScannerModule = new TopographyScannerModule(2f, 0.01f, 5f);
    //  topographyScannerModule.SetPowerModule(powerModule);
    topographyScannerModule.SetDataStorageModule(dataStorageModule);

    droneDockModule = new DroneDockModule();
    droneDockModule.SetTopographyScannerModule(topographyScannerModule);
  }

  private void Update() {
    coreProcess.RunStep(Time.deltaTime);

    solarPanelModule.Update(Time.deltaTime);
    topographyScannerModule.Update(Time.deltaTime);
    //  powerModule.Update();
    dataStorageModule.Update();
  }
}