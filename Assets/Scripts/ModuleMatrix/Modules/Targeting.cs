using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : Module {
  [SerializeField] private GameObject target = null;

  private HoverMovement hoverMovement = null;

  private void Start() {
    hoverMovement = GetComponent<HoverMovement>();
  }

  public override void RunStep(float deltaTime) {
    if(target == null) return;

    Vector3 targetPosition = target.transform.position;
    targetPosition.y = transform.position.y;

    if(Vector3.Distance(transform.position, targetPosition) < 0.01) return;

    Vector3 movement = targetPosition - transform.position;

    hoverMovement.ApplyMovement(movement);
  }
}