using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : Module {
  private const int HOVER_MOVEMENT_INDEX = 0;

  private GameObject target = null;
  public bool IsLockedOn { get; private set; }
  
  public GameObject Target {
    get {
      return target;
    }

    private set {
      target = value;
    }
  }

  private void Awake() {
    Title = "Targeting";
    Description = "Locks on to a target";
    Activator = new ModuleActivator(false, "Cancel Targeting", "Set Target");
    Activator.Activated += ActivateTargetingMode;
    
    ModuleRequirement requirement = new ModuleRequirement();
    requirement.SetRequiredModule<HoverMovement>("Hover Movement");
    
    Requirements.Add(requirement);
  }

  private void ActivateTargetingMode() {
    SelectionManager.DeselectResource();
  }

  private void DeactivateTargetingMode() {
    Activator.Toggle();
  }

  public override void RunStep(float deltaTime) {
    if(Activator.IsActive) {
      if(SelectionManager.SelectedResource != null) {
        target = SelectionManager.SelectedResource.gameObject;
        DeactivateTargetingMode();
      }
    } else {
      MoveToTarget();
    }
  }

  public void SetTarget(GameObject targetObject) {
    target = targetObject;
  }

  private void MoveToTarget() {
    if(target == null) return;
    
    HoverMovement hoverMovement = Requirements[HOVER_MOVEMENT_INDEX].AssociatedModule as HoverMovement;

    if(hoverMovement != null) {
      Vector3 targetPosition = target.transform.position;
      targetPosition.y = transform.position.y;

      if(Vector3.Distance(transform.position, targetPosition) < 0.01) {
        IsLockedOn = true;
        return;
      } else {
        IsLockedOn = false;
      }

      Vector3 movement = targetPosition - transform.position;

      hoverMovement.ApplyMovement(movement);
    }
  }
}