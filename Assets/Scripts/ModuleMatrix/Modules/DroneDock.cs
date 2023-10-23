using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDock : Module {
  public GameObject drone;
  public Animator animator;
  
  private void Start() {
    Title = "Drone Dock";
    Description = "\"Drone Deployment Depot - Your Personal Taskmasters.\" Because we know Solara can't be bothered to pick up space rocks herself.";
    Activator = new ModuleActivator(false, "Dock Drone", "Deploy Drone");

    Activator.Deactivated += () => {
      Vector3 dockingPosition = transform.position;
      dockingPosition.y = drone.transform.position.y;

      if(Vector3.Distance(dockingPosition, drone.transform.position) > 0.01f) {
        Activator.Toggle();
      }
    };
  }

  public override void RunStep(float deltaTime) {
    drone.SetActive(Activator.IsActive);
    animator.SetBool("is_deployed", Activator.IsActive);
  }
}