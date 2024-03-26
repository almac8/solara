using UnityEngine;
using UnityEngine.UIElements;

public class NewGameUIPC : MonoBehaviour {
  private MainMenuUI mainMenuUI;

  private void OnEnable() {
    mainMenuUI = transform.parent.parent.gameObject.GetComponent<MainMenuUI>();
    
    VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

    Button startButton = rootVisualElement.Q<Button>("start");
    startButton.clicked += StarNewGame;

    Button backButton = rootVisualElement.Q<Button>("back");
    backButton.clicked += ActivateMainMenuUI;
  }

  private void StarNewGame() {
    GameManager.Instance.SetGameState(GameManager.GameState.PLAY);
  }

  private void ActivateMainMenuUI() {
    mainMenuUI.SetState(MainMenuUI.UIState.MAIN_MENU);
  }
}