using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : Module {
  private const int TARGETING_INDEX = 0;

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
      Targeting targeting = Requirements[TARGETING_INDEX].AssociatedModule as Targeting;
      if(targeting != null) targeting.SetTarget(homeDock);
    }
  }
}