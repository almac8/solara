using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
  [SerializeField] private GameObject UI;

  private void OnMouseDown() {
    ShowUI();
  }

  protected void ShowUI() {
    UI.SetActive(true);
  }

  protected void HideUI() {
    UI.SetActive(false);
  }
}