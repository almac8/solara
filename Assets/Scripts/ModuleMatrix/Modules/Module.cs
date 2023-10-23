using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Module : MonoBehaviour {
  public string Title { get; set; }
  public string Description { get; set; }
  public ModuleGauge Gauge { get; protected set; }
  public ModuleActivator Activator { get; protected set; }

  private List<ModuleRequirement> requirements = new List<ModuleRequirement>();
  public List<ModuleRequirement> Requirements {
    get {
      return requirements;
    }

    set {
      requirements = value;
    }
  }

  public abstract void RunStep(float deltaTime);
}