using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour {
  public enum UIState {
    MAIN_MENU,
    START,
    LOAD,
    SETTINGS
  };

  [SerializeField] private GameObject mainMenu;
  [SerializeField] private GameObject newGame;
  [SerializeField] private GameObject loadGame;
  [SerializeField] private GameObject settings;

  public UIState ActiveState { get; private set; }

  private void Awake() {
    mainMenu.SetActive(true);
    newGame.SetActive(false);
    loadGame.SetActive(false);
    settings.SetActive(false);
  }

  public void SetState(UIState newUIState) {
    ActiveState = newUIState;

    mainMenu.SetActive(false);
    newGame.SetActive(false);
    loadGame.SetActive(false);
    settings.SetActive(false);

    switch (newUIState) {
      case UIState.MAIN_MENU:
        mainMenu.SetActive(true);
        break;

      case UIState.START:
        newGame.SetActive(true);
        break;

      case UIState.LOAD:
        loadGame.SetActive(true);
        break;

      case UIState.SETTINGS:
        settings.SetActive(true);
        break;

      default:
        mainMenu.SetActive(true);
        break;
    }
  }
}