using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Module : MonoBehaviour {
  public string Title { get; set; }
  public string Description { get; set; }
  public ModuleGauge Gauge { get; protected set; }

  public abstract void RunStep(float deltaTime);
}