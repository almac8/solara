using UnityEngine;

public class PlaySM : MonoBehaviour {
  private PauseMenuUI pauseMenuUI;

  private void Awake() {
    pauseMenuUI = GameObject.Find("User Interface").GetComponent<PauseMenuUI>();
  }
  
  private void Update() {
    if(Input.GetButtonDown("Cancel")) TogglePaused();
  }

  private void TogglePaused() {
    pauseMenuUI.Toggle();
  }
}