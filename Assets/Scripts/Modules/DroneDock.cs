using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDock : Module {
  public bool droneIsDeployed;
  public GameObject drone;
  public TopographyScanner topographyScanner;
  public Animator animator;

  private void Start() {
    Title = "Drone Dock";
    topographyScanner = gameObject.GetComponent<TopographyScanner>();
  }

  public override void RunStep(float deltaTime) {
    drone.SetActive(droneIsDeployed);
    animator.SetBool("is_deployed", droneIsDeployed);
  }
}