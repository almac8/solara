using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : Module {
  private GameObject target = null;
  private HoverMovement hoverMovement = null;

  public GameObject Target {
    get {
      return target;
    }

    private set {
      target = value;
    }
  }

  private void Awake() {
    hoverMovement = GetComponent<HoverMovement>();
    Activator = new ModuleActivator(false, "Cancel Targeting", "Set Target");
    Activator.Activated += ActivateTargetingMode;
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

  private void MoveToTarget() {
    if(target == null) return;

    Vector3 targetPosition = target.transform.position;
    targetPosition.y = transform.position.y;

    if(Vector3.Distance(transform.position, targetPosition) < 0.01) return;

    Vector3 movement = targetPosition - transform.position;

    hoverMovement.ApplyMovement(movement);
  }
}