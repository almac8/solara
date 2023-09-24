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
  [SerializeField] private GameObject homeDock;
  [SerializeField] private GameObject target;
  
  private void Update() {
    switch(state) {
      case DroneState.DOCKED:
        break;

      case DroneState.MOVING:
        Vector3 collectionPoint = target.transform.position;
        collectionPoint.y = transform.position.y;

        if(Vector3.Distance(transform.position, collectionPoint) < 0.01) {
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
        Vector3 homePosition = homeDock.transform.position;
        homePosition.y = 1;

        if(Vector3.Distance(transform.position, homePosition) < 0.01) {
          state = DroneState.DOCKED;
        } else {
          transform.eulerAngles = Vector3.zero;
          Vector3 direction = Vector3.Normalize(homePosition - transform.position);
          transform.Translate(direction * Time.deltaTime * movementSpeed);
        }
        break;
    }
  }

  public void Collect(GameObject objectToCollect) {
    KnowledgeDatabase knowledgeDatabase = homeDock.GetComponent<KnowledgeDatabase>();
    float flyingUnitTopographicalSafeDistance = knowledgeDatabase.localTopographyRadius;
    float distanceToTarget = Vector3.Distance(homeDock.transform.position, objectToCollect.transform.position);

    if(distanceToTarget < flyingUnitTopographicalSafeDistance) {
      target = objectToCollect;
      state = DroneState.MOVING;
    } else {
      Debug.Log("Requires local topography information");
    }
  }
}