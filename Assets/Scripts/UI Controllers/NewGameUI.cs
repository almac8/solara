using UnityEngine;
using UnityEngine.UIElements;

public class NewGameUI : MonoBehaviour {
  private UserInterface uiController;

  private void OnEnable() {
    uiController = transform.parent.parent.gameObject.GetComponent<UserInterface>();
    
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
    uiController.SetState(UserInterface.UIState.MAIN_MENU);
  }
}