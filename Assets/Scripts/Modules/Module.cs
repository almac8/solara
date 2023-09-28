using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Module : MonoBehaviour {
  public string Title { get; set; }

  public abstract void RunStep(float deltaTime);
}