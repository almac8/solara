using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleMatrix : MonoBehaviour {
  private PowerModule powerModule;
  private SolarPanelModule solarPanelModule;
  private DataStorageModule dataStorageModule;
  private TopographyScannerModule topographyScannerModule;
  private DroneDockModule droneDockModule;

  private void Awake() {
    powerModule = new PowerModule(100f);

    CoreProcess coreProcess = gameObject.AddComponent<CoreProcess>();
    coreProcess.standbyPowerConsumption = 0.001f;
    coreProcess.SetPowerModule(powerModule);
    
    solarPanelModule = new SolarPanelModule(1f);
    solarPanelModule.SetPowerModule(powerModule);

    dataStorageModule = new DataStorageModule(100f);

    topographyScannerModule = new TopographyScannerModule(2f, 0.01f, 5f);
    topographyScannerModule.SetPowerModule(powerModule);
    topographyScannerModule.SetDataStorageModule(dataStorageModule);

    droneDockModule = new DroneDockModule();
    droneDockModule.SetTopographyScannerModule(topographyScannerModule);
  }

  private void Update() {
    Debug.Log(powerModule.GetStatusString());
    //  coreProcessModule.Update(Time.deltaTime);
    solarPanelModule.Update(Time.deltaTime);
    topographyScannerModule.Update(Time.deltaTime);
    powerModule.Update();
    dataStorageModule.Update();
  }
}