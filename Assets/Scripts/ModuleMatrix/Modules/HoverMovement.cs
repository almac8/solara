using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverMovement : Module {
  [SerializeField] private float movementSpeed;

  private Vector3 movement = Vector3.zero;

  public override void RunStep(float deltaTime) {
    transform.Translate(Vector3.Normalize(movement) * movementSpeed * deltaTime);
    movement = Vector3.zero;
  }

  public void ApplyMovement(Vector3 newMovement) {
    movement += newMovement;
  }
}