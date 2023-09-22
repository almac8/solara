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
    DATA_ANALIZER
  }

  private List<Module> modules;

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
    }

    modules.Add(newModule);
    return newModule;
  }

  private void Update() {
    float deltaTime = Time.deltaTime;
    foreach (Module module in modules) {
      module.RunStep(deltaTime);
    }
  }
}