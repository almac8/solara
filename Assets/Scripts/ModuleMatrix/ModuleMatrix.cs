using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleMatrix : MonoBehaviour {
  public enum ModuleType {
    POWER_STORAGE,
    CORE_PROCESS,
    TOPOGRAPHY_SCANNER,
    SOLAR_PANEL,
    DATA_STORAGE,
    DRONE_DOCK,
    KNOWLEDGE_DATABASE,
    DATA_ANALIZER,
    PHYSICAL_STORAGE
  }

  public List<Module> modules;

  private void Start() {
    modules = new List<Module>();
    GetComponents<Module>(modules);
  }

  public Module AddModule(ModuleType type) {
    Module newModule = null;

    switch (type) {
      case ModuleType.POWER_STORAGE:
        newModule = gameObject.AddComponent<PowerStorage>();
        break;
      
      case ModuleType.CORE_PROCESS:
        newModule = gameObject.AddComponent<CoreProcess>();
        break;

      case ModuleType.TOPOGRAPHY_SCANNER:
        newModule = gameObject.AddComponent<TopographyScanner>();
        break;

      case ModuleType.SOLAR_PANEL:
        newModule = gameObject.AddComponent<SolarPanel>();
        break;

      case ModuleType.DATA_STORAGE:
        newModule = gameObject.AddComponent<DataStorage>();
        break;

      case ModuleType.DRONE_DOCK:
        newModule = gameObject.AddComponent<DroneDock>();
        break;

      case ModuleType.KNOWLEDGE_DATABASE:
        newModule = gameObject.AddComponent<KnowledgeDatabase>();
        break;

      case ModuleType.DATA_ANALIZER:
        newModule = gameObject.AddComponent<DataAnalizer>();
        break;

      case ModuleType.PHYSICAL_STORAGE:
        newModule = gameObject.AddComponent<PhysicalStorage>();
        break;
    }

    modules.Add(newModule);
    return newModule;
  }

  public void RemoveModule(Module moduleToRemove) {
    modules.Remove(moduleToRemove);
  }

  private void Update() {
    float deltaTime = Time.deltaTime;
    foreach (Module module in modules) {
      module.RunStep(deltaTime);
    }
  }

  public List<ModuleGauge> GetGauges() {
    List<ModuleGauge> gauges = new List<ModuleGauge>();
    
    foreach (Module module in modules) {
      ModuleGauge gauge = module.Gauge;
      if(gauge != null) {
        gauges.Add(gauge);
      }
    }

    return gauges;
  }

  public List<ModuleActivator> GetActivators() {
    List<ModuleActivator> activators = new List<ModuleActivator>();

    foreach (Module module in modules) {
      ModuleActivator activator = module.Activator;
      if(activator != null) {
        activators.Add(activator);
      }
    }

    return activators;
  }
}