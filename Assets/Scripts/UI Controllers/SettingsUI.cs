using UnityEngine;
using UnityEngine.UIElements;

public class SettingsUI : MonoBehaviour {
  private UserInterface uiController;

  private void OnEnable() {
    Debug.Log("Settings");
    uiController = transform.parent.parent.gameObject.GetComponent<UserInterface>();
    
    VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

    Button backButton = rootVisualElement.Q<Button>("back");
    backButton.clicked += ActivateMainMenuUI;
  }

  private void ActivateMainMenuUI() {
    uiController.SetState(UserInterface.UIState.MAIN_MENU);
  }
}