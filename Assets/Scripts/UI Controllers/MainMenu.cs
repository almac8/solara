using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour {
  private UserInterface uiController;

  private void OnEnable() {
    uiController = transform.parent.parent.gameObject.GetComponent<UserInterface>();

    VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

    Button startButton = rootVisualElement.Q<Button>("start");
    startButton.clicked += ActivateStartUI;
  }

  private void ActivateStartUI() {
    uiController.SetState(UserInterface.UIState.START);
  }
}