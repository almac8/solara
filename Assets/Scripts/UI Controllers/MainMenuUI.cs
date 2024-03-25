using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour {
  private UserInterface uiController;

  private void OnEnable() {
    uiController = transform.parent.parent.gameObject.GetComponent<UserInterface>();

    VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

    Button startButton = rootVisualElement.Q<Button>("start");
    startButton.clicked += ActivateStartUI;

    Button loadButton = rootVisualElement.Q<Button>("load");
    loadButton.clicked += ActivateLoadUI;

    Button settingsButton = rootVisualElement.Q<Button>("settings");
    settingsButton.clicked += ActivateSettingsUI;

    Button quitButton = rootVisualElement.Q<Button>("quit");
    quitButton.clicked += () => Application.Quit();
  }

  private void ActivateStartUI() {
    uiController.SetState(UserInterface.UIState.START);
  }

  private void ActivateLoadUI() {
    uiController.SetState(UserInterface.UIState.LOAD);
  }

  private void ActivateSettingsUI() {
    uiController.SetState(UserInterface.UIState.SETTINGS);
  }
}