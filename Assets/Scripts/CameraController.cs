using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
  [SerializeField] private Camera mainCamera;
  [SerializeField] private Transform nearViewpoint;
  [SerializeField] private Transform mediumViewpoint;
  [SerializeField] private Transform farViewpoint;
  [SerializeField] private float viewpointDistance;
  [SerializeField] private float scrollSensitivity;

  private void Update() {
    viewpointDistance = Mathf.Clamp(viewpointDistance - Input.mouseScrollDelta.y * scrollSensitivity, 0f, 2f);
    
    if(viewpointDistance <= 1f) {
      Vector3 newCameraPosition = Vector3.Lerp(nearViewpoint.position, mediumViewpoint.position, viewpointDistance);
      mainCamera.transform.position = newCameraPosition;
      mainCamera.transform.LookAt(transform.position);
    } else {
      Vector3 newCameraPosition = Vector3.Lerp(mediumViewpoint.position, farViewpoint.position, viewpointDistance - 1f);
      Quaternion newCameraRotation = Quaternion.Lerp(mediumViewpoint.rotation, farViewpoint.rotation, viewpointDistance - 1f);
      mainCamera.transform.position = newCameraPosition;
      mainCamera.transform.rotation = newCameraRotation;
    }
  }
}