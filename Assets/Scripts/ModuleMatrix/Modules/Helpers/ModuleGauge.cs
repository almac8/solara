using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleGauge {
  public float Value { get; set; }
  public float MaxValue { get; }
  public string Title { get; set; }

  public ModuleGauge(float initialValue, float maxValue, string title) {
    Value = initialValue;
    MaxValue = maxValue;
    Title = title;
  }
}