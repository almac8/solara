using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour {
  public enum UIState {
    MAIN_MENU,
    START
  };

  [SerializeField] private GameObject mainMenu;
  [SerializeField] private GameObject newGame;

  public UIState ActiveState { get; private set; }

  private void Awake() {
    mainMenu.SetActive(true);
    newGame.SetActive(false);
  }

  public void SetState(UIState newUIState) {
    ActiveState = newUIState;

    mainMenu.SetActive(false);
    newGame.SetActive(false);

    switch (newUIState) {
      case UIState.MAIN_MENU:
        mainMenu.SetActive(true);
        break;

      case UIState.START:
        newGame.SetActive(true);
        break;

      default:
        mainMenu.SetActive(true);
        break;
    }
  }
}