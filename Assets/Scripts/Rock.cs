using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {
  [SerializeField] private GameObject rockUI;

  private void OnMouseDown() {
    ShowUI();
  }

  private void ShowUI() {
    rockUI.SetActive(true);
  }

  public void HideUI() {
    rockUI.SetActive(false);
  }
}