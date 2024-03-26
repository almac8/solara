using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuUIPC : MonoBehaviour {
  private PauseMenuUI pauseMenuUI;

  private void OnEnable() {
    pauseMenuUI = transform.parent.parent.gameObject.GetComponent<PauseMenuUI>();

    VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

    Button resumeButton = rootVisualElement.Q<Button>("resume");
    resumeButton.clicked += DeactivatePauseMenuUI;

    Button quitButton = rootVisualElement.Q<Button>("quit");
    quitButton.clicked += () => Application.Quit();
  }

  private void DeactivatePauseMenuUI() {
    pauseMenuUI.Toggle();
  }
}