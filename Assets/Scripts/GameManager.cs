using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
  public enum GameState {
    MAIN_MENU,
    PLAY,
    GAME_OVER
  };

  public static GameManager Instance { get; private set; }
  public GameState CurrentState { get; private set; }

  private void Awake() {
    if(Instance != null && Instance != this) {
      Destroy(this);
      return;
    }

    Instance = this;
    DontDestroyOnLoad(gameObject);

    SetGameState(GameState.MAIN_MENU);
  }

  public void SetGameState(GameState newGameState) {
    CurrentState = newGameState;

    switch (newGameState) {
      case GameState.MAIN_MENU:
        SceneManager.LoadScene("Main Menu");
        break;

      case GameState.PLAY:
        SceneManager.LoadScene("Play");
        break;

      default:
        SceneManager.LoadScene("Main Menu");
        break;
    }
  }
}