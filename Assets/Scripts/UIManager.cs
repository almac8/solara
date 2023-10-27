using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
  [SerializeField] private GameObject hud;
  [SerializeField] private GameObject moduleMatrixUI;
  [SerializeField] private GameObject constructionUI;

  public void EnableHUD() {
    hud.SetActive(true);
  }

  public void EnableModuleMatrixUI() {
    moduleMatrixUI.SetActive(true);
  }

  public void EnableConstructionUI() {
    constructionUI.SetActive(true);
  }

  public void DisableConstructionUI() {
    constructionUI.SetActive(false);
  }
}