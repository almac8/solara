using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUIPC : MonoBehaviour {
  private MainMenuUI mainMenuUI;

  private void OnEnable() {
    mainMenuUI = transform.parent.parent.gameObject.GetComponent<MainMenuUI>();

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
    mainMenuUI.SetState(MainMenuUI.UIState.START);
  }

  private void ActivateLoadUI() {
    mainMenuUI.SetState(MainMenuUI.UIState.LOAD);
  }

  private void ActivateSettingsUI() {
    mainMenuUI.SetState(MainMenuUI.UIState.SETTINGS);
  }
}