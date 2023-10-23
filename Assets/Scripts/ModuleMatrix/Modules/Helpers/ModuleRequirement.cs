using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleRequirement {
  public System.Type ModuleType { get; private set; }
  public string ModuleName { get; private set; }
  public Module AssociatedModule { get; private set; }

  public void SetRequiredModule<T>(string name) {
    ModuleType = typeof(T);
    ModuleName = name;
  }

  public void SetAssociatedModule(Module module) {
    AssociatedModule = module;
  }
}