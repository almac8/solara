using UnityEngine;
using UnityEngine.UIElements;

public class NewGameUI : MonoBehaviour {
  private void OnEnable() {
    VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

    Button startButton = rootVisualElement.Q<Button>("start");
    startButton.clicked += StarNewGame;
  }

  private void StarNewGame() {
    GameManager.Instance.SetGameState(GameManager.GameState.PLAY);
  }
}