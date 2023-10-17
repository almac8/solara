using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
  [SerializeField] private GameObject hud;
  [SerializeField] private GameObject moduleMatrixUI;

  public void EnableHUD() {
    hud.SetActive(true);
  }

  public void EnableModuleMatrixUI() {
    moduleMatrixUI.SetActive(true);
  }
}