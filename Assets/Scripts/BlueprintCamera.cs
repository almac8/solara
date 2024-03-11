using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintCamera : MonoBehaviour {
  [SerializeField] private float movementSpeed;

  [SerializeField] private float minZoom;
  [SerializeField] private float maxZoom;
  [SerializeField] private float zoomSpeed;

  private Camera camera;
  private float cameraSize;

  private void Awake() {
    camera = GetComponent<Camera>();
  }

  private void Update() {
    Vector3 movement = Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f));
    movement *= movementSpeed * Time.deltaTime;
    transform.Translate(movement);

    float mouseScroll = Input.mouseScrollDelta.y;
    cameraSize = Mathf.Clamp(cameraSize + mouseScroll * zoomSpeed * Time.deltaTime, minZoom, maxZoom);
    camera.orthographicSize = cameraSize;
  }
}