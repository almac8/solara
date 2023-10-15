using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : Module {
  private Targeting targeting = null;
  private HoverMovement hoverMovement = null;
  private Vector3 homePosition = Vector3.zero;

  private void Awake() {
    Title = "Homing Module";
    Description = "Takes us Home";
    Activator = new ModuleActivator(false, "Cancel Homing", "Go Home");
    targeting = GetComponent<Targeting>();
    homePosition = transform.position;
    hoverMovement = GetComponent<HoverMovement>();
  }

  public override void RunStep(float deltaTime) {
    if(Activator.IsActive){
      targeting.SetTarget(transform.parent.gameObject);
    }
  }
}