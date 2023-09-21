using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDock : Module {
  public bool droneIsDeployed;
  public GameObject drone;

  private TopographyScanner topographyScanner;

  private void Start() {
    topographyScanner = gameObject.GetComponent<TopographyScanner>();
  }

  public void RunStep() {
    drone.SetActive(droneIsDeployed);
  }
}