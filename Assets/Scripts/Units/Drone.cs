using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Unit {
  [SerializeField] private float rotationSpeed;
  [SerializeField] private float movementSpeed;

  [SerializeField] private float cargoStorageCapacity = 5f;
  [SerializeField] private float cargoStorage = 0f;
  [SerializeField] private float extractionRate = 0.5f;

  private enum DroneState {
    DOCKED,
    MOVING,
    COLLECTING,
    HOMING
  }

  [SerializeField] private DroneState state;
  private GameObject target;
  private Vector3 homingPoint;

  private void Start() {
    homingPoint = transform.position;
  }

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
        cargoStorage = Mathf.Clamp(cargoStorage + extractionRate * Time.deltaTime, 0, cargoStorageCapacity);
        if(cargoStorage == cargoStorageCapacity) state = DroneState.HOMING;
        break;

      case DroneState.HOMING:
        if(transform.position == homingPoint) {
          state = DroneState.DOCKED;
        } else {
          transform.eulerAngles = Vector3.zero;
          Vector3 direction = Vector3.Normalize(homingPoint - transform.position);
          transform.Translate(direction * Time.deltaTime * movementSpeed);
        }
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