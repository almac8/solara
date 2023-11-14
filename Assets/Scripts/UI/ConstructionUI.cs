using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ConstructionUI : MonoBehaviour {
  [SerializeField] private List<GameObject> constructionOptions = new List<GameObject>();

  private void OnEnable() {
    VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

    Button closeButton = rootVisualElement.Q<Button>("close");
    closeButton.clicked += GameManager.Instance.UIManager.DisableUI;

    VisualElement constructionOptionsButtons = rootVisualElement.Q<VisualElement>("construction_options");

    foreach (GameObject option in constructionOptions) {
      Button optionButton = new Button();
      optionButton.text = option.name;
      optionButton.clicked += () => {
        GameManager.Instance.SelectionManager.SelectedUnit.gameObject.GetComponent<ConstructionPlanner>().SetConstructionGhost(option);
      };

      constructionOptionsButtons.Add(optionButton);
    }
  }
}