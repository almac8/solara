using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ConstructionUI : MonoBehaviour {
  [SerializeField] private List<GameObject> constructionOptions = new List<GameObject>();

  private void OnEnable() {
    VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
    UIManager uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
    ConstructionManager constructionManager = GameObject.FindWithTag("ConstructionManager").GetComponent<ConstructionManager>();

    Button closeButton = rootVisualElement.Q<Button>("close");
    closeButton.clicked += uiManager.DisableConstructionUI;

    VisualElement constructionOptionsButtons = rootVisualElement.Q<VisualElement>("construction_options");

    foreach (GameObject option in constructionOptions) {
      Button optionButton = new Button();
      optionButton.text = option.name;
      optionButton.clicked += () => constructionManager.SetConstructionGhost(option);

      constructionOptionsButtons.Add(optionButton);
    }
  }
}