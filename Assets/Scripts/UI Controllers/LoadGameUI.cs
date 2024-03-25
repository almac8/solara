using UnityEngine;
using UnityEngine.UIElements;

public class LoadGameUI : MonoBehaviour {
  private UserInterface uiController;

  private void OnEnable() {
    Debug.Log("Load Game");
    uiController = transform.parent.parent.gameObject.GetComponent<UserInterface>();
    
    VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

    Button backButton = rootVisualElement.Q<Button>("back");
    backButton.clicked += ActivateMainMenuUI;
  }

  private void ActivateMainMenuUI() {
    uiController.SetState(UserInterface.UIState.MAIN_MENU);
  }
}