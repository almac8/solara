using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : Module {
  private GameObject homeDock = null;

  private void Awake() {
    Title = "Homing Module";
    Description = "Takes us Home";
    Activator = new ModuleActivator(false, "Cancel Homing", "Go Home");
    
    ModuleRequirement requirement = new ModuleRequirement();
    requirement.SetRequiredModule<Targeting>("Targeting");
    Requirements.Add(requirement);

    homeDock = transform.parent.gameObject;
  }

  public override void RunStep(float deltaTime) {
    if(Activator.IsActive){
      Requirements[TARGETING_INDEX].SetTarget(homeDock);
    }
  }
}