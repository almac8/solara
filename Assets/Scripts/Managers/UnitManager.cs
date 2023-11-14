using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {
  public Unit CoreProcessUnit { get; private set; }

  public void RegisterCoreProcessUnit(Unit coreProcessor) {
    CoreProcessUnit = coreProcessor;
  }
}