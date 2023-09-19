using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour {
  [SerializeField] private GameObject droneUI;
  [SerializeField] private float rotationSpeed;

  private bool isCollecting;

  private void Update() {
    if(isCollecting) {
      transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
    }
  }

  public void Collect(GameObject objectToCollect) {
    Vector3 collectionPoint = objectToCollect.transform.position;
    collectionPoint.y = transform.position.y;

    transform.position = collectionPoint;
    isCollecting = true;
  }

  private void ShowUI() {
    droneUI.SetActive(true);
  }

  public void HideUI() {
    droneUI.SetActive(false);
  }

  private void OnMouseDown() {
    ShowUI();
  }
}