using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDock : Module {
  public GameObject drone;
  public TopographyScanner topographyScanner;
  public Animator animator;

  private void Start() {
    Title = "Drone Dock";
    Description = "\"Drone Deployment Depot - Your Personal Taskmasters.\" Because we know Solara can't be bothered to pick up space rocks herself.";
    topographyScanner = gameObject.GetComponent<TopographyScanner>();
    Activator = new ModuleActivator(false, "Dock Drone", "Deploy Drone");
  }

  public override void RunStep(float deltaTime) {
    drone.SetActive(Activator.IsActive);
    animator.SetBool("is_deployed", Activator.IsActive);
  }
}