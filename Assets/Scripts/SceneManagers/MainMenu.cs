using UnityEngine;

public class MainMenu : MonoBehaviour {
  private void Awake() {
    Debug.Log("Main Menu");

    GameManager.Instance.SetGameState(GameManager.GameState.PLAY);
  }
}