using UnityEngine;

public class UIManager : MonoBehaviour {
  [SerializeField] private GameObject hud;
  [SerializeField] private GameObject moduleMatrixUI;
  [SerializeField] private GameObject constructionUI;

  public enum UILayout {
    NONE,
    HUD,
    MODULE_MATRIX,
    CONSTRUCTION
  }

  private UILayout activeLayout;

  public void SetUI(UILayout newLayout) {
    DisableUI();
    activeLayout = newLayout;

    switch (newLayout) {
      case UILayout.HUD:
        hud.SetActive(true);
        break;

      case UILayout.MODULE_MATRIX:
        hud.SetActive(true);
        break;

      case UILayout.CONSTRUCTION:
        hud.SetActive(true);
        break;

      default:
        break;
    }

    //  Activated GameObject
  }

  public void DisableUI() {
    activeLayout = UILayout.NONE;

    hud.SetActive(false);
    moduleMatrixUI.SetActive(false);
    constructionUI.SetActive(false);
  }
}