using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteController : Module {
  [SerializeField] private float movementSpeed;

  public override void RunStep(float deltaTime) {
    Vector3 movement = Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
    movement *= movementSpeed * deltaTime;
    transform.Translate(movement);
  }
}