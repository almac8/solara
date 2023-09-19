using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Unit {
  [SerializeField] private float rotationSpeed;
  [SerializeField] private float movementSpeed;

  private enum DroneState {
    DOCKED,
    MOVING,
    COLLECTING
  }

  private DroneState state;
  private GameObject target;

  private void Update() {
    switch(state) {
      case DroneState.DOCKED:
        break;

      case DroneState.MOVING:
        Vector3 collectionPoint = target.transform.position;
        collectionPoint.y = transform.position.y;

        if(transform.position == collectionPoint) {
          state = DroneState.COLLECTING;
        } else {
          Vector3 direction = Vector3.Normalize(collectionPoint - transform.position);
          transform.Translate(direction * Time.deltaTime * movementSpeed);
        }
        
        break;

      case DroneState.COLLECTING:
        transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
        break;
    }
  }

  public void Collect(GameObject objectToCollect) {
    target = objectToCollect;
    state = DroneState.MOVING;
  }

  private void OnMouseDown() {
    ShowUI();
  }
}