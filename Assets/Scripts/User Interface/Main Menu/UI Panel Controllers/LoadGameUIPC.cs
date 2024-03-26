using UnityEngine;
using UnityEngine.UIElements;

public class LoadGameUIPC : MonoBehaviour {
  private MainMenuUI mainMenuUI;

  private void OnEnable() {
    mainMenuUI = transform.parent.parent.gameObject.GetComponent<MainMenuUI>();
    
    VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

    Button backButton = rootVisualElement.Q<Button>("back");
    backButton.clicked += ActivateMainMenuUI;
  }

  private void ActivateMainMenuUI() {
    mainMenuUI.SetState(MainMenuUI.UIState.MAIN_MENU);
  }
}