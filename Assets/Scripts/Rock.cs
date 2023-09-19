using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {
  [SerializeField] private GameObject rockUI;
  [SerializeField] private Drone drone;

  private void OnMouseDown() {
    ShowUI();
  }

  public void CollectWithDrone() {
    drone.Collect(gameObject);
  }

  private void ShowUI() {
    rockUI.SetActive(true);
  }

  public void HideUI() {
    rockUI.SetActive(false);
  }
}