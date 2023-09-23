using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {
  [SerializeField] private float rotationSpeed;

  private void Update() {
    transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
  }
}